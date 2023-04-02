namespace EuropeanCoffees.ViewModels

open EuropeanCoffees.Models.ColumnContentModel
open Avalonia

type FilterViewModel(dataset) as this =
    inherit ViewModelBase(dataset)

    let mutable filterMap = Map.empty

    let optionsUpdated = new Event<Map<string,string>>()

    // let mutable selectedCountry = "any"
    // let mutable selectedVariety = "any"
    // let mutable selectedProcessing = "any"
    // let mutable selectedRoast = "any"

    [<CLIEvent>]
    member this.OptionsUpdated = optionsUpdated.Publish
    member this.OnUpdate() =
        this.UpdateFilterMap()
        optionsUpdated.Trigger(filterMap)

    member this.FilterMap = filterMap

    member this.Countries =
        basicColumn dataset "Country"
        |> Array.insertAt 0 "any"
    member val SelectedCountry = "any" with get, set

    member this.Varieties =
        composedColumn dataset "Variety"
        |> Array.insertAt 0 "any"
    member val SelectedVariety = "any" with get, set

    member this.Processings =
        basicColumn dataset "Processing"
        |> Array.insertAt 0 "any"
    member val SelectedProcessing = "any" with get, set

    member this.Roasts =
        composedColumn dataset "Roast"
        |> Array.insertAt 0 "any"
    member val SelectedRoast = "any" with get, set

    member private this.UpdateFilterMap() =
        let newMapList =
            List.filter (fun item -> (snd item) <> "any") [
                ("Country", this.SelectedCountry);
                ("Variety", this.SelectedVariety);
                ("Processing", this.SelectedProcessing);
                ("Roast", this.SelectedRoast);
            ]
        filterMap <- Map newMapList



