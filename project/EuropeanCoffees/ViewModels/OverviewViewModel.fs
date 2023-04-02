namespace EuropeanCoffees.ViewModels

type OverviewViewModel(dataset) =
    inherit ViewModelBase(dataset)

    member this.CountryColumnChart = ColumnViewModel(dataset, "Country")
    member this.VarietyColumnChart = ColumnViewModel(dataset, "Variety")
    member this.RoasteryCountryColumnChart = ColumnViewModel(dataset, "Roastery Country")
    member this.ProcessingColumnChart = TasteWheelViewModel(dataset)
    member this.Filter = FilterViewModel(dataset)
