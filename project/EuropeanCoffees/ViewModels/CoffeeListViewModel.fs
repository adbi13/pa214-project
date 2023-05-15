namespace EuropeanCoffees.ViewModels

open Deedle
open EuropeanCoffees.Models.FilterModel
open EuropeanCoffees.Models
open System.Collections.ObjectModel

type CoffeeListViewModel(dataset) =
    inherit ViewModelBase(dataset)

    let mutable actualDataset = dataset.Coffees

    let coffees = new ObservableCollection<Coffee>()

    member this.Coffees = coffees

    member this.UpdateCoffees() =
        coffees.Clear()

        actualDataset.Rows.ValuesAll
        |> Seq.map (fun row ->
            { Name = row.GetAs<string>("Name"); Processing = row.GetAs<string>("Processing"); Price = row.GetAs<decimal>("Price") }
        )
        |> Seq.iter (fun coffee -> coffees.Add(coffee))

    member this.HandleFilterUpdate(filterMap: Map<string,string>) =
        actualDataset <- filterDataset this.Dataset.Coffees filterMap
        this.UpdateCoffees()

