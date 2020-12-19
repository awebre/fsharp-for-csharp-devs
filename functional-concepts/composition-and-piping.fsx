open System

let calculateTax x = 
    x * 0.097m

let addTax x = 
    x + calculateTax x

let addShipping x = 
    x + 2.99m

//type definition necessary here, because C#
let round (x:decimal) =
    Math.Round(x, 2)

module Traditional = 
    let getTotal x = round(addShipping(addTax x))

module Compositional = 
    let getTotal = addTax >> addShipping >> round

let price = 15.99m
printfn "Traditional: $%M" (Traditional.getTotal price)
printfn "Compositional: $%M" (Compositional.getTotal price)
//...and now with piping!
printfn "With piping: $%M" (price |> Compositional.getTotal)
    