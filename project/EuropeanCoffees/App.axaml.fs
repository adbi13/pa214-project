namespace EuropeanCoffees

open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Markup.Xaml
open EuropeanCoffees.ViewModels
open EuropeanCoffees.Views
open EuropeanCoffees.DataSource

type App() =
    inherit Application()

    override this.Initialize() =
            AvaloniaXamlLoader.Load(this)

    override this.OnFrameworkInitializationCompleted() =


        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktop ->
            let dataset = new Dataset()
            desktop.MainWindow <- MainWindow(DataContext = MainWindowViewModel(dataset))
        | _ -> ()

        base.OnFrameworkInitializationCompleted()
