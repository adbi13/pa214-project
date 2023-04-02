namespace EuropeanCoffees.Models

open Deedle
open EuropeanCoffees.DataSource

module CountsModel =
    let columnCounts (dataset: Dataset) column =
        dataset.Coffees
        |> Frame.getCol column
        |> Series.values
        |> Seq.map (fun (row: string) -> row.Split(", "))
        |> Seq.concat
        |> Seq.countBy id
        |> Seq.toArray
        |> Array.unzip
