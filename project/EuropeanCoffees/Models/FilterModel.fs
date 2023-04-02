namespace EuropeanCoffees.Models

open Deedle
open EuropeanCoffees.DataSource

module FilterModel =
    let filterDataset (dataset: Frame<int, string>) (filterMap: Map<string, string>) =
        let mutable filteredDataset = dataset
        for (name, value) in Map.toSeq filterMap do
            filteredDataset <- Frame.filterRowsBy name value filteredDataset
        filteredDataset