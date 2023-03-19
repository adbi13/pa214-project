module WebScraper

open System
open System.IO

open FSharp.Data

let jizbaLuhacovice =
    let parseCoffeePage (url: string) =
        // let description = HtmlDocument.Load(url).CssSelect(".detail-parameters").Head
        // description.Descendants["td"]
        // |> Seq.take 1
        // |> Seq.iter (fun info -> printf $"{info.DirectInnerText()} ")
        let page = HtmlDocument.Load(url)
        // let country = 

        let info =
            page.CssSelect(".detail-parameters td")
            |> Seq.map (fun info -> info.DirectInnerText().Trim())
            |> Seq.toArray
        try
            $"Jizba;Luhacovice;Czech Republic;{info[6]};{info[7]};{info[9]};{info[10]};{info[13]};{info[14]};{info[0]}"
        with
        | _ -> ""
        
        

    let url = "https://www.jizba.com/kava/"
    let page = HtmlDocument.Load(url)

    let output = new StreamWriter "jizba.csv"

    page.CssSelect ".name"
    |> Seq.map (fun coffee -> "https://www.jizba.com" + coffee.AttributeValue "href")
    // |> Seq.iter (fun coffee -> printfn $"{coffee}")
    // |> Seq.take 1
    |> Seq.map parseCoffeePage
    |> Seq.iter output.WriteLine

    output.Close()
