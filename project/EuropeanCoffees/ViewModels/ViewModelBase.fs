namespace EuropeanCoffees.ViewModels

open EuropeanCoffees.DataSource
open ReactiveUI

type ViewModelBase(dataset: Dataset) =
    inherit ReactiveObject()

    member this.Dataset = dataset
