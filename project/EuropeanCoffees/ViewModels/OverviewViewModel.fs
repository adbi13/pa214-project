namespace EuropeanCoffees.ViewModels

type OverviewViewModel(dataset) =
    inherit ViewModelBase(dataset)

    member this.CountryColumnChart = ColumnViewModel(dataset, "Country")
    member this.VarietyColumnChart = ColumnViewModel(dataset, "Variety")
    member this.RoasteryCountryColumnChart = ColumnViewModel(dataset, "Roastery Country")
    member this.RoasteryMap = MapViewModel(dataset, "Roastery Country")
    member this.Filter = FilterViewModel(dataset)
    member this.OriginMap = MapViewModel(dataset, "Country")
    member this.CountryPricesChart = MinMaxPricesViewModel(dataset, "Country")
    member this.RoasteryCountryPricesChart = MinMaxPricesViewModel(dataset, "Roastery Country")
