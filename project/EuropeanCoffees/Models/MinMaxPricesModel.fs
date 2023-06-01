namespace EuropeanCoffees.Models

open Deedle
open EuropeanCoffees.DataSource

module MinMaxPricesModel =
    let priceMinMax (dataset: Dataset) column =
        dataset.Coffees.Rows.Values
        |> Seq.map (fun row -> row.GetAs<string>(column), row.GetAs<decimal>("Price (EUR per kg)"))
        |> Seq.groupBy fst
        |> Seq.map (fun (country, countryPrice) -> country, Seq.map snd countryPrice)
        |> Seq.map (fun (country, prices) -> country, Seq.min prices, Seq.max prices)
        |> Seq.sortBy (fun (c, minimum, maximum) -> minimum)
        |> Seq.toArray
        |> Array.unzip3
