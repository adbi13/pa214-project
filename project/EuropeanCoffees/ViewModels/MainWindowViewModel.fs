namespace EuropeanCoffees.ViewModels

type MainWindowViewModel() =
    inherit ViewModelBase()

    member this.CountryColumnChart = ColumnViewModel("Country")
    member this.VarietyColumnChart = ColumnViewModel("Variety")
    member this.RoasteryCountryColumnChart = ColumnViewModel("Roastery Country")
    member this.ProcessingColumnChart = TasteWheelViewModel("Processing", "Natural")
