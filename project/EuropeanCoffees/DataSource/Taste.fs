namespace EuropeanCoffees.DataSource

open Deedle

module Taste =
    let tastesDf = Frame.ReadCsv("../../data/ProfilesTastes.csv")

    let profiles = Series.values <| Frame.getCol "Profile" tastesDf
    let tastes = Series.values <| Frame.getCol "Taste" tastesDf

    let (tasteMap: Map<string, string>) = Map.ofSeq <| Seq.zip profiles tastes

    let profilesToTastes (profileString: string) =
        profileString.Split(", ")
        |> Array.map (fun profile -> tasteMap[profile])
        |> Array.distinct
