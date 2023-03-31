namespace EuropeanCoffee.Visualizations

module Plots =
    let columnCounts (df: Frame<int,string>) column =
        df
        |> Frame.getCol column
        |> Series.values
        |> Seq.map (fun (row: string) -> row.Split(", "))
        |> Seq.concat
        |> Seq.countBy id
        // |> Seq.iter (fun (variety, count) -> printfn "%s %i" variety count)
        |> Chart.Column
        |> Chart.Show
