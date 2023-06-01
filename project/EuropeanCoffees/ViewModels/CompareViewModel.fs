namespace EuropeanCoffees.ViewModels

type CompareViewModel(dataset) =
    inherit ViewModelBase(dataset)

    let filter1 = FilterViewModel(dataset)
    let tasteWheel1 = TasteWheelViewModel(dataset)
    let stats1 = StatsViewModel(dataset)
    let filter2 = FilterViewModel(dataset)
    let tasteWheel2 = TasteWheelViewModel(dataset)
    let stats2 = StatsViewModel(dataset)

    let coffeeList = CoffeeListViewModel(dataset)

    do
        filter1.OptionsUpdated.Add(tasteWheel1.HandleFilterUpdate)
        filter1.OptionsUpdated.Add(stats1.HandleFilterUpdate)
        filter1.OnUpdate()
        filter2.OptionsUpdated.Add(tasteWheel2.HandleFilterUpdate)
        filter2.OptionsUpdated.Add(stats2.HandleFilterUpdate)
        filter2.OnUpdate()
    
    member this.Filter1 = filter1
    member this.TasteWheel1 = tasteWheel1
    member this.Stats1 = stats1
    member this.Filter2 = filter2
    member this.TasteWheel2 = tasteWheel2
    member this.Stats2 = stats2
