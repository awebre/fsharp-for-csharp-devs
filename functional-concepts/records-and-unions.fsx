open System

type Address = 
    { Line1: string
      Line2: string 
      City: string
      State: string
      Zip: string }

type RushOrder = OverNight | WithinDays of int
type ShippingMethod = Standard | Rush of RushOrder | NoRush

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

let address = 
    { Line1 = "120 S Cypress St"; 
      Line2 = "Suite 1"; 
      City = "Hammond"; 
      State = "LA"; 
      Zip = "70042" }

let shipment = 
    { Address = address; 
      Method = Standard; 
      Cost = getShippingMethodCost Standard } //this could be better

printfn "Your shipment information:\n%A" shipment //the print formatting is really nice