namespace EuropeanCoffee.DataSource

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

    let cleanVarieties (rawInput: string) =
        rawInput.Split(", ")
        |> Seq.map (fun variety -> correctVariety (variety.ToLower()))
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

