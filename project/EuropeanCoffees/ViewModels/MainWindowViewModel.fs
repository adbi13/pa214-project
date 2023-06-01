namespace EuropeanCoffees.ViewModels

type MainWindowViewModel(dataset) =
    inherit ViewModelBase(dataset)

    member this.Overview = OverviewViewModel(dataset)
    member this.Categories = CategoriesViewModel(dataset)
    member this.Compare = CompareViewModel(dataset)

    // member this.CountryColumnChart = ColumnViewModel(dataset, "Country")
    // member this.VarietyColumnChart = ColumnViewModel(dataset, "Variety")
    // member this.RoasteryCountryColumnChart = ColumnViewModel(dataset, "Roastery Country")
    // member this.ProcessingColumnChart = TasteWheelViewModel(dataset, "Processing", "Natural")
    // member this.Filter = FilterViewModel(dataset)
