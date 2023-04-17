namespace EuropeanCoffees.DataSource

open FSharp.Data
open System.IO

module Countries =
    let countriesCodesMap = 
        let countriesJson = JsonValue.Parse(File.ReadAllText("../../data/word-map-index.json"))
        let mutable map = Map.empty
        for country in countriesJson.GetProperty "lands" do
            let name = country.GetProperty("name").AsString()
            let shortName = country.GetProperty("shortName").AsString()
            map <- map.Add(name, shortName)
        map <- map.Add("Sumatra", "idn")
        map
