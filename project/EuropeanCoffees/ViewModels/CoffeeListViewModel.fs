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

        actualDataset.Rows.Values
        |> Seq.map (fun row -> {
                Country = row.GetAs<string>("Country");
                Name = row.GetAs<string>("Name");
                Processing = row.GetAs<string>("Processing");
                Roastery = row.GetAs<string>("Roastery");
                RoasteryCity = row.GetAs<string>("Roastery City");
                RoasteryCountry = row.GetAs<string>("Roastery Country");
                Roast = row.GetAs<string>("Roast");
                Variety = row.GetAs<string>("Variety");
                Profile = row.GetAs<string>("Profile");
             }
        )
        |> Seq.iter (fun coffee -> coffees.Add(coffee))

    member this.HandleFilterUpdate(filterMap: Map<string,string>) =
        actualDataset <- filterDataset this.Dataset.Coffees filterMap
        this.UpdateCoffees()
