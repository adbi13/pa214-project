namespace EuropeanCoffees.ViewModels

open EuropeanCoffees.Models.CountsModel
open LiveChartsCore
open LiveChartsCore.SkiaSharpView

type ColumnViewModel(columnName) =
    inherit ViewModelBase()

    let names, counts = columnCounts columnName

    member this.Series : ISeries array = 
        [|
            ColumnSeries<int>(
                Values = counts,
                TooltipLabelFormatter =
                    fun chartPoint -> $"Count: {chartPoint.PrimaryValue}"
            );
        |]
    
    member this.XAxes : Axis array =
        [|
            Axis(
                Labels = names,
                LabelsRotation = 90,
                // TextSize = 10,
                MinStep = 1,
                ShowSeparatorLines = true
            );
        |]

    // let series = {
    //     LineSeries.Values = [| 1; 2; 3 |];
    //     Name = "test";
    // }

    // do
    //     series.Values <- [| 1; 2; 3 |]
    //     series.Name <- "test"

    // member this.Greeting = "Welcome to Avalonia!"
    // member this.Series = series


