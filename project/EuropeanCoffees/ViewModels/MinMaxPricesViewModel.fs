namespace EuropeanCoffees.ViewModels

open EuropeanCoffees.Models.MinMaxPricesModel
open LiveChartsCore
open LiveChartsCore.SkiaSharpView
open LiveChartsCore.SkiaSharpView.VisualElements
open LiveChartsCore.SkiaSharpView.Painting
open SkiaSharp

type MinMaxPricesViewModel(dataset, columnName) =
    inherit ViewModelBase(dataset)

    let mutable countries, minimums, maximums = priceMinMax dataset columnName

    let indexedMinimums = minimums |> Array.mapi (fun i value -> decimal i, value)
    let indexedMaximums = maximums |> Array.mapi (fun i value -> decimal i, value)


    member this.Series : ISeries array = 
        [|
            ScatterSeries<decimal>(
                Values = minimums,
                //Fill = null,
                TooltipLabelFormatter =
                    fun chartPoint -> $"Min price: €{chartPoint.PrimaryValue}"
            );
            ScatterSeries<decimal>(
                Values = maximums,
                //Fill = null,
                TooltipLabelFormatter =
                    fun chartPoint -> $"Max price: €{chartPoint.PrimaryValue}"
            );
        |]
    
    member this.XAxes : Axis array =
        [|
            Axis(
                Labels = countries,
                LabelsRotation = 125,
                // TextSize = 10,
                // MinStep = 1,
                ShowSeparatorLines = true
            );
        |]
    
    member this.YAxes : Axis array =
        [|
            Axis(
                MinLimit = 0
            );
        |]

// Doplnit nulu na Y

    member this.Title = LabelVisual(
        Text = $"{columnName} Min and Max Prices",
        TextSize = 25,
        Padding = new LiveChartsCore.Drawing.Padding(15),
        Paint = new SolidColorPaint(SKColors.DarkSlateGray)
    )