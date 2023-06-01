namespace EuropeanCoffees.DataSource

module Cleaning =
    let correctVariety variety =
        match variety with
        | "catuaì" | "catuaí" -> "catuai"
        | "mundo novo, red catuaí" -> "mundo novo, red catuai"
        | "sl 34" -> "sl34"
        | "acaiá" -> "acaia"
        | "yellow borubon" -> "yellow bourbon"
        | "yellow catuaí" -> "yellow catuai"
        | "geisha enano" -> "geisha"
        | "green tip pacamara" | "bronze tip pacamara" -> "pacarama"

        // heirlooms
        // | "heirlooms ’74-219 & ’74-212" -> "74219, 74212"
        | "heirlooms ’74-219 & ’74-212" | "74110" | "74158"
        | "74212" | "74219" | "74122" | "74142"
        | "gibirinna 74110, 74112" | "dega" | "wolisho"
        | "daga" | "krume 74142" | "krume 74122"
        | "serto 74112" | "etiopes" -> "heirloom"
        | _ -> variety

    let correctVarietyAbbr (variety: string) =
        match variety.ToLower() with
        | "sl34" -> "SL34"
        | "sl28" -> "SL28"
        | "2sl" -> "2SL"
        | _ -> variety

    let cleanVarieties (rawInput: string) =
        rawInput.Split(", ")
        |> Seq.map (fun variety -> correctVariety (variety.ToLower()))
        |> Seq.map System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase
        |> Seq.map correctVarietyAbbr
        |> Seq.reduce (fun a b -> $"{a}, {b}")

    let correctProfile profile =
        match profile with
        | "almonds" -> "almond"
        | "berries" -> "berry"
        | "blackberries" -> "blackberry"
        | "blueberries" -> "blueberry"
        | "caramell" -> "caramel"
        | "citrus" -> "citrus fruit"
        | "classic chocolate and milk" -> "milk chocolate"
        | "cranberries" -> "cranberry"
        | "dried fruits" -> "dried fruit"
        | "figs" -> "fig"
        | "florals" | "flowers" | "floral" -> "flower"
        | "fruity" -> "fruit"
        | "gooseberries" -> "gooseberry"
        | "white jasmine" | "jazmin" -> "jasmine"
        | "macadamia" -> "macadamia nut"
        | "marcipan" -> "marzipan"
        | "nectarine melon" -> "nectarine, melon"
        | "nutty" -> "nuts"
        | "passionfruit" -> "passion fruit"
        | "peach,honey" -> "peach, honey"
        | "peaches" -> "peach"
        | "pralines" -> "praline"
        | "raisind" -> "raisins"
        | "red apples" -> "red apple"
        | "red berries" -> "red berry"
        | "roses" -> "rose"
        | "stone fruits" -> "stone fruit"
        | "strawberries" -> "strawberry"
        | "sugar cane" -> "sugarcane"
        | "tropical fruits" -> "tropical fruit"
        | "white florals" -> "white flower"
        | "yellow fruits" | "yellow mature fruit" -> "yellow fruit"
        | _ -> profile

    let cleanProfiles (rawInput: string)=
        rawInput.Split(", ")
        |> Seq.map (fun profile -> correctProfile (profile.ToLower()))
        |> Seq.reduce (fun a b -> $"{a}, {b}")

    let correctProcessing processing =
        match processing with
        | "natural" | "Black Diamond Natural" -> "Natural"
        | "Fully Washed"-> "Washed"
        | "Yellow Honey" | "Black Honey" -> "Honey"

        | "Anaerobic Natural" | "Anaerobic Honey" | "Carbonic Maceration"
        | "96h fermented, washed" | "777 Fermantation"
        | "Double Fermentation Natural" | "Washed with double fermentation"
        | "Natural Anaerobic Fermentation" | "Double Diamond Natural Anaerobic"
        | "48-hour cherry maceration" | "Natural & Fermentation"
        | "Natural, Wet Fermentation 72 hours" | "Fully washed, Wet Fermentation 72 hours"
        | "Natural, 48 hours maceration" -> "Fermentation"

        | _ -> processing
