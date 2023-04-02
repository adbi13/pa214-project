namespace EuropeanCoffees.Models

open Deedle
open EuropeanCoffees.DataSource
open EuropeanCoffees.DataSource.Taste

module TasteWheelModel =
    let profileCountsWith (dataset: Frame<int, string>) =
        dataset
        |> Frame.getCol "Profile"
        |> Series.values
        |> Seq.map (fun (row: string) -> row.Split(", "))
        |> Seq.concat
        |> Seq.countBy id
        |> Seq.sortBy (fun (tuple: string * int) -> tasteMap[fst tuple])
        |> Seq.toArray

    let tasteCountsWith (dataset: Frame<int, string>) =
        dataset
        |> Frame.getCol "Tastes"
        |> Series.values
        |> Seq.concat
        |> Seq.countBy id
        |> Seq.sortBy (fun (tuple: string * int) -> fst tuple)
        |> Seq.toArray
        |> Array.unzip
