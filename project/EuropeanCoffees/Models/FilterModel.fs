namespace EuropeanCoffees.Models

open Deedle
open EuropeanCoffees.DataSource

module FilterModel =
    let filterDataset (dataset: Frame<int, string>) (filterMap: Map<string, string>) =
        let mutable filteredDataset = dataset.Clone()
        for (name, value) in Map.toSeq filterMap do
            filteredDataset <- filteredDataset
            |> Frame.filterRowValues (fun values -> Array.contains value (values.GetAs<string>(name).Split(", ")))
            // |> Frame.filterRowsBy name value
        filteredDataset