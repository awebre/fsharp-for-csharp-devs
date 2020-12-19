open System

type Address = 
    { Line1: string
      Line2: string 
      City: string
      State: string
      Zip: string }

type ShippingMethod = Ground | OverNight | NoRush

type Shipment =
    { Address: Address
      Method: ShippingMethod 
      Cost: decimal }

let getShippingMethodCost method =
    match method with
    | Ground -> 2.99m
    | OverNight -> 12.99m
    | NoRush -> 0.00m

let address = 
    { Line1 = "120 S Cypress St"; 
      Line2 = "Suite 1"; 
      City = "Hammond"; 
      State = "LA"; 
      Zip = "70042" }

let shipment = 
    { Address = address; 
      Method = Ground; 
      Cost = getShippingMethodCost Ground } //this could be better

printfn "Your shipment information:\n%A" shipment //the print formatting is really nice