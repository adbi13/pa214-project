namespace EuropeanCoffees.ViewModels

open ReactiveUI
open EuropeanCoffees.Models.FilterModel
open EuropeanCoffees.Models.TasteWheelModel
open EuropeanCoffees.Models.ColumnContentModel
open EuropeanCoffees.DataSource.Countries
open LiveChartsCore
open LiveChartsCore.SkiaSharpView
open SkiaSharp
open System
open System.Collections.Specialized
open System.Collections.ObjectModel
open Deedle
open LiveChartsCore.SkiaSharpView.Drawing.Geometries

type MapViewModel(dataset, column) as this =
    inherit ViewModelBase(dataset)

    let mutable actualDataset = dataset.Coffees

    let mutable series = [| HeatLandSeries(Lands = [| |]) |]

    do
        this.UpdateCountries()
    // member this.Lands = countries

    member this.Series =
        // series[0].Lands |> Seq.iter (fun name-> printfn "%s %f" name.Name name.Value)
        series

    member this.UpdateCountries() =
        let countries =
            actualDataset
            |> Frame.getCol column
            |> Series.values
            |> Seq.countBy id
            |> Seq.map (fun (country, count) -> new HeatLand(
                Value = count,
                Name = countriesCodesMap[country]
            ))
            |> Seq.map (fun item -> item :> Geo.IWeigthedMapLand)
            |> Seq.toArray
        series <- [| HeatLandSeries(Lands = countries) |]
        this.RaisePropertyChanged("Series")

    member this.HandleFilterUpdate(filterMap: Map<string,string>) =
        actualDataset <- filterDataset this.Dataset.Coffees filterMap
        this.UpdateCountries()
