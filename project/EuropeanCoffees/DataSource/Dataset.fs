namespace EuropeanCoffees.DataSource

open Deedle
open Cleaning
open Taste

type Dataset() =
    let df = Frame.ReadCsv("../../data/Coffees.csv")

    do
        let newVarieties = 
            df.GetColumn<string>("Variety") 
            |> Series.mapValues cleanVarieties
        df.ReplaceColumn("Variety", newVarieties)

        let newProfiles = 
            df.GetColumn<string>("Profile") 
            |> Series.mapValues cleanProfiles
        df.ReplaceColumn("Profile", newProfiles)

        let coffeeTastes =
            df.GetColumn<string>("Profile")
            |> Series.mapValues profilesToTastes
        df.AddColumn("Tastes", coffeeTastes)

        let newProcessings =
            df.GetColumn<string>("Processing")
            |> Series.mapValues correctProcessing
        df.ReplaceColumn("Processing", newProcessings)

    member this.Coffees = df
