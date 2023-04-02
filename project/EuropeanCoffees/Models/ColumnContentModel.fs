namespace EuropeanCoffees.Models

open Deedle
open EuropeanCoffees.DataSource

module ColumnContentModel =
    let composedColumn (dataset: Dataset) column =
        dataset.Coffees
        |> Frame.getCol column
        |> Series.values
        |> Seq.map (fun (row: string) -> row.Split(", "))
        |> Seq.concat
        |> Seq.distinct
        |> Seq.toArray
    
    let basicColumn (dataset: Dataset) column =
        dataset.Coffees
        |> Frame.getCol column
        |> Series.values
        |> Seq.distinct
        |> Seq.toArray
