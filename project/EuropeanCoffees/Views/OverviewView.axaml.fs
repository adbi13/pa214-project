namespace EuropeanCoffees.Views

open Avalonia
open Avalonia.Controls
open Avalonia.Markup.Xaml

type OverviewView () as this = 
    inherit UserControl ()

    do this.InitializeComponent()

    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)
