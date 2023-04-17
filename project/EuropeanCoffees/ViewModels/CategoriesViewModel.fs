namespace EuropeanCoffees.ViewModels

type CategoriesViewModel(dataset) =
    inherit ViewModelBase(dataset)

    let filter = FilterViewModel(dataset)
    let tasteWheel = TasteWheelViewModel(dataset)
    let stats = StatsViewModel(dataset)
    let map = MapViewModel(dataset, "Country")

    do
        filter.OptionsUpdated.Add(tasteWheel.HandleFilterUpdate)
        filter.OptionsUpdated.Add(stats.HandleFilterUpdate)
        filter.OptionsUpdated.Add(map.HandleFilterUpdate)
        filter.OnUpdate()
    
    member this.Filter = filter
    member this.TasteWheel = tasteWheel
    member this.Stats = stats
    member this.Map = map
