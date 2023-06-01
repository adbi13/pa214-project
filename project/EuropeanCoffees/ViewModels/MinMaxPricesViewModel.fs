namespace EuropeanCoffees.ViewModels

open EuropeanCoffees.Models.MinMaxPricesModel
open LiveChartsCore
open LiveChartsCore.SkiaSharpView
open LiveChartsCore.SkiaSharpView.VisualElements
open LiveChartsCore.SkiaSharpView.Painting
open SkiaSharp

type MinMaxPricesViewModel(dataset, columnName) =
    inherit ViewModelBase(dataset)

    let countries, minimums, maximums = priceMinMax dataset columnName

    member this.Series : ISeries array = 
        [|
            LineSeries<decimal>(
                Values = minimums,
                Fill = null,
                TooltipLabelFormatter =
                    fun chartPoint -> $"Min price: €{chartPoint.PrimaryValue}"
            );
            LineSeries<decimal>(
                Values = maximums,
                Fill = null,
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

// Doplnit nulu na Y

    member this.Title = LabelVisual(
        Text = $"{columnName} Min and Max Prices",
        TextSize = 25,
        Padding = new LiveChartsCore.Drawing.Padding(15),
        Paint = new SolidColorPaint(SKColors.DarkSlateGray)
    )