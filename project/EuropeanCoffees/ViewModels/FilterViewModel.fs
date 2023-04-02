namespace EuropeanCoffees.ViewModels

open EuropeanCoffees.Models.ColumnContentModel
open LiveChartsCore
open LiveChartsCore.SkiaSharpView

type FilterViewModel(dataset) =
    inherit ViewModelBase(dataset)

    // let coffees = columnCounts columnName
    let optionsUpdated = new Event<FilterViewModel>()
    
    [<CLIEvent>]
    member this.OptionsUpdated = optionsUpdated.Publish
    member this.OnUpdate = optionsUpdated.Trigger(this)

    member this.Countries = basicColumn dataset "Country"
    member this.SelectedCountry = this.Countries[0]

    member this.Varieties = composedColumn dataset "Variety"
    member this.SelectedVariety = this.Varieties[0]

    member this.Processings = basicColumn dataset "Processing"
    member this.SelectedProcessing = this.Processings[0]

    member this.Roasts = basicColumn dataset "Roast"
    member this.SelectedRoast = this.Roasts[0]

    



