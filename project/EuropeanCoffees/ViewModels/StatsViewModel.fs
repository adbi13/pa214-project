namespace EuropeanCoffees.ViewModels

open ReactiveUI
open EuropeanCoffees.Models.FilterModel
open EuropeanCoffees.Models.ColumnContentModel
open Avalonia
open Deedle

type StatsViewModel(dataset) =
    inherit ViewModelBase(dataset)

    let mutable actualDataset = dataset.Coffees

    let mutable avgPrice = 0f
    let mutable avgScore = 0f
    let mutable minAltitude = 0
    let mutable maxAltitude = 0

    member this.AvgPrice
        with get() = if avgPrice = 0f then "-" else sprintf "€%.2f" avgPrice

    member this.AvgScore
        with get() = if avgScore = 0f then "-" else sprintf "%.2f" avgScore

    member this.MinAltitude
        with get() = if minAltitude = 0 then "-" else sprintf "%d" minAltitude
    
    member this.MaxAltitude
        with get() = if maxAltitude = 0 then "-" else sprintf "%d" maxAltitude
    
    member this.UpdatePrice() =
        try
            avgPrice <- actualDataset
            |> Frame.getCol "Price (EUR per kg)"
            |> Series.values
            |> Seq.map float32
            |> Seq.average
        with
        | :? System.ArgumentException -> avgPrice <- 0f
        this.RaisePropertyChanged("AvgPrice")
        
    member this.UpdateScore() =
        try
            avgScore <- actualDataset
            |> Frame.getCol "Score"
            |> Series.values
            |> Seq.map float32
            |> Seq.average
        with
        | :? System.ArgumentException -> avgScore <- 0f
        this.RaisePropertyChanged("AvgScore")
    
    member this.UpdateMinAltitude() =
        try
            minAltitude <- actualDataset
            |> Frame.getCol "Altitude min"
            |> Series.values
            |> Seq.min
        with
        | :? System.ArgumentException -> minAltitude <- 0
        this.RaisePropertyChanged("MinAltitude")

    member this.UpdateMaxAltitude() =
        try
            maxAltitude <- actualDataset
            |> Frame.getCol "Altitude max"
            |> Series.values
            |> Seq.max
        with
        | :? System.ArgumentException -> maxAltitude <- 0
        this.RaisePropertyChanged("MaxAltitude")


    member this.HandleFilterUpdate(filterMap: Map<string,string>) =
        actualDataset <- filterDataset this.Dataset.Coffees filterMap
        this.UpdatePrice()
        this.UpdateScore()
        this.UpdateMinAltitude()
        this.UpdateMaxAltitude()
