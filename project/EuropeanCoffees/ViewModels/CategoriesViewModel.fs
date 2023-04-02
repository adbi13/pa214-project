namespace EuropeanCoffees.ViewModels

type CategoriesViewModel(dataset) =
    inherit ViewModelBase(dataset)

    let filter = FilterViewModel(dataset)
    let tasteWheel = TasteWheelViewModel(dataset)

    do
        filter.OptionsUpdated.Add(tasteWheel.HandleFilterUpdate)
    
    member this.Filter = filter
    member this.TasteWheel = tasteWheel
