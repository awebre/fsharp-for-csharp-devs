open System
// TODO: steal some more domain modeling ideas from 
// https://viktorvan.github.io/fsharp/domain-modeling-with-fsharp/

type City = City of string
type State = State of string
type Zip = Zip of string

type Address = 
  private
    { Line1: string
      Line2: string option 
      City: City
      State: State
      Zip: Zip }
  module Address = 
    let create line1 line2 city state zip = 
      let line2Parsed = match line2 with
                        | null -> None
                        | str -> Some str;
      { Line1 = line1; Line2 = line2Parsed; City = City city; State = State state; Zip = Zip zip}

type RushOrder = private OverNight | WithinDays of int
type ShippingMethod = Standard | Rush of RushOrder | NoRush

let parseShippingMethod method =
    match method with
    | "Standard" -> Standard
    | "Overnight" -> Rush OverNight
    | withinDays when withinDays.EndsWith " Days" -> Rush (WithinDays ((withinDays.Split ' ').[0] |> int)) //there's probably a safer way to do this conversion
    | _ -> NoRush //a better way to do this would be to include an error case and return a result type (Ok/Error)

type Shipment =
    { Address: Address
      Method: ShippingMethod 
      Cost: decimal }

let getShippingMethodCost method =
  match method with
  | Standard -> 2.99m
  | Rush order -> 
    match order with
    | OverNight -> 12.99m
    | WithinDays days when 0 <= days && days <= 1 -> 12.99m
    | WithinDays days when 2 <= days && days <= 5 -> 5.99m
    | _ -> 3.99m
  | NoRush -> -1.99m

//simulated input
let line1Input = "120 S Cypress St"
let line2Input = "Suite 1"
let cityInput = "Hammond"
let stateInput = "Louisiana"
let zipInput = "70042"
let methodInput = "Standard"

let address = Address.create line1Input line2Input cityInput stateInput zipInput
let method = parseShippingMethod methodInput
let cost = getShippingMethodCost method

let shippingInfo = { Address = address; Method = method; Cost = cost}