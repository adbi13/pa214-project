namespace EuropeanCoffees.ViewModels

open EuropeanCoffees.Models.TasteWheelModel
open EuropeanCoffees.DataSource.Taste
open LiveChartsCore
open LiveChartsCore.SkiaSharpView
open LiveChartsCore.SkiaSharpView.Painting
open SkiaSharp
open System

type TasteWheelViewModel(columnName, columnValue) =
    inherit ViewModelBase()

    let colorMap = Map [
        ("Nutty/Cocoa", SKColor(red=99uy, green=55uy, blue=44uy));
        ("Fruity", SKColor(red=236uy, green=78uy, blue=32uy));
        ("Floral", SKColor(red=1uy, green=111uy, blue=185uy));
        ("Sweet", SKColor(red=130uy, green=2uy, blue=99uy));
        ("Spices", SKColor(red=255uy, green=149uy, blue=5uy));
        ("Sour/Fermented", SKColor(red=107uy, green=212uy, blue=37uy));
    ]

    let getRandomColor name colorShift =
        let random = Random()
        let mutable h = 0f
        let mutable s = 0f
        let mutable l = 0f
        colorMap[tasteMap[name]].ToHsl(&h, &s, &l)
        SKColor.FromHsl(h + float32 colorShift, s, l, 255uy)
        
        
        // new SKColor(
        //     ~~~color.Red ||| (byte (random.Next(128))),
        //     ~~~color.Green ||| (byte (random.Next(128))),
        //     ~~~color.Blue ||| (byte (random.Next(128))),
        //     150uy)

    let names, counts = tasteCountsWith columnName columnValue

    member this.Tastes : ISeries array = 
        // counts
        // |> Array.zip names
        profileCountsWith columnName columnValue
        |> Array.groupBy (fun item -> tasteMap[fst item])
        |> Array.map (fun (key, items) -> key, Array.sumBy snd items)
        |> Array.map (fun (name, count) -> PieSeries<int>(
            Values = [| count |],
            Name = name,
            Fill = new SolidColorPaint(colorMap[name])
        ))
    
    member this.Profiles : ISeries array =
        profileCountsWith columnName columnValue
        |> Array.mapi (fun i (name, count) -> PieSeries<int>(
            Values = [| count |],
            Name = name,
            InnerRadius = 130,
            Fill = new SolidColorPaint(getRandomColor name ((i * 3) % 30))
                
        ))
