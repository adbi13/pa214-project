namespace EuropeanCoffees.Models

open Deedle
open EuropeanCoffees.DataSource

module CountsModel =
    let columnCounts column =
        // let plot = new Plot(width=600, height=400)
        let dataset = new Dataset()
        dataset.Coffees
        |> Frame.getCol column
        |> Series.values
        |> Seq.map (fun (row: string) -> row.Split(", "))
        |> Seq.concat
        |> Seq.countBy id
        // |> Seq.map snd
        // |> Seq.map float
        |> Seq.toArray
        |> Array.unzip
