namespace EuropeanCoffees.ViewModels

open EuropeanCoffees.Models.FilterModel
open EuropeanCoffees.Models.TasteWheelModel
open EuropeanCoffees.Models.ColumnContentModel
open EuropeanCoffees.DataSource.Taste
open LiveChartsCore
open LiveChartsCore.SkiaSharpView
open LiveChartsCore.SkiaSharpView.Painting
open SkiaSharp
open System
open System.Collections.Specialized
open System.Collections.ObjectModel
open Deedle

type TasteWheelViewModel(dataset) =
    inherit ViewModelBase(dataset)

    let mutable actualDataset = dataset.Coffees

    let colorMap = Map [
        ("Nutty/Cocoa", SKColor(red=99uy, green=55uy, blue=44uy));
        ("Fruity", SKColor(red=236uy, green=78uy, blue=32uy));
        ("Floral", SKColor(red=1uy, green=111uy, blue=185uy));
        ("Sweet", SKColor(red=130uy, green=2uy, blue=99uy));
        ("Spices", SKColor(red=255uy, green=149uy, blue=5uy));
        ("Sour/Fermented", SKColor(red=107uy, green=212uy, blue=37uy));
    ]

    let mutable profileCountsMap =
        // profileCountsWith actualDataset
        // |> Array.groupBy (fun item -> tasteMap[fst item])
        // |> Array.map (fun (key, items) -> key, Array.sumBy snd items)
        composedColumn dataset "Profile"
        |> Array.groupBy (fun item -> tasteMap[item])
        |> Array.map (fun (key, items) -> key, items.Length)
        |> Array.sortBy fst
        |> Map.ofArray

    let rec getPositionWithinTaste (actualPosition: int) tastes taste =
        match tastes with
        | [] -> invalidArg "tastes" "Taste not in map"
        | head::_ when head = taste -> actualPosition
        | head::tail ->
            // printfn "%s %d %d" head profileCountsMap[head] actualPosition
            getPositionWithinTaste (actualPosition - profileCountsMap[head]) tail taste

    let getRandomColor name colorShift =
        let random = Random()
        let mutable h = 0f
        let mutable s = 0f
        let mutable l = 0f
        colorMap[tasteMap[name]].ToHsl(&h, &s, &l)
        let positionWithinTaste = getPositionWithinTaste colorShift (List.ofSeq profileCountsMap.Keys) tasteMap[name]

        SKColor.FromHsl(h + 5f, s - 5f, l - (0.5f * float32 positionWithinTaste), 255uy)
        
        
        // new SKColor(
        //     ~~~color.Red ||| (byte (random.Next(128))),
        //     ~~~color.Green ||| (byte (random.Next(128))),
        //     ~~~color.Blue ||| (byte (random.Next(128))),
        //     150uy)

    let mutable tastes : ISeries array =
        Map.toArray colorMap
        |> Array.sortBy fst
        |> Array.map (fun (name, _) -> PieSeries<int>(
            Values = ObservableCollection<int>([| 1 |]),
            Name = name,
            InnerRadius = 50,
            Fill = new SolidColorPaint(colorMap[name])
        ))
    let mutable profiles : ISeries array =
        composedColumn dataset "Profile"
        |> Array.sortBy (fun profile -> tasteMap[profile])
        |> Array.mapi (fun i name -> PieSeries<int>(
            Values = ObservableCollection<int>([| 1 |]),
            Name = name,
            InnerRadius = 130,
            Fill = new SolidColorPaint(getRandomColor name i)   
        ))

    member this.Tastes : ISeries array = tastes
    member this.Profiles : ISeries array = profiles

    member this.UpdateSeries (seriesArray: ISeries array) name value =
        let series = Array.find (fun (s: ISeries) -> s.Name = name) seriesArray
        let seriesValues: ObservableCollection<int> = series.Values :?> ObservableCollection<int>
        // seriesValues.Clear()
        seriesValues.Add(value)
    
    member this.ClearSeries (seriesArray: ISeries array) =
        for series in seriesArray do
            let seriesValues: ObservableCollection<int> = series.Values :?> ObservableCollection<int>
            seriesValues.Clear()
        
    member this.UpdateTastes() =
        this.ClearSeries tastes
        let counts =
            profileCountsWith actualDataset
            |> Array.groupBy (fun item -> tasteMap[fst item])
            |> Array.map (fun (key, items) -> key, Array.sumBy snd items)
        
        profileCountsMap <- Map.ofArray counts

        counts
        |> Array.iter (fun (name, count) -> this.UpdateSeries tastes name count)
    
    member this.UpdateProfiles() =
        this.ClearSeries profiles
        profileCountsWith actualDataset
        |> Array.iter (fun (name, count) -> this.UpdateSeries profiles name count)

    member this.HandleFilterUpdate(filterMap: Map<string,string>) =
        actualDataset <- filterDataset this.Dataset.Coffees filterMap
        this.UpdateTastes()
        this.UpdateProfiles()
