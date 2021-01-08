open System;

module Customers = 
    type CustomerId = CustomerId of int

    type Customer = 
        { Id: CustomerId 
          FirstName: string 
          MiddleName: string option 
          LastName: string }

    let chargeCustomer (customerId:CustomerId) =
        printfn "Charging customer %A" customerId
open Customers

module Shipping = 
    type Days = Days of int with
        static member op_Explicit x =
            match x with Days d -> decimal d //"hack" that allows us to cast to decimal directly
    type ShippingMethod = 
        | Rush of Days
        | Regular
        | NoRush

    let getShippingCost method =
        match method with
        | Rush days -> 10.0m - (decimal days)
        | Regular -> 2.99m
        | NoRush -> 0.00m
open Shipping

module Taxes =
    type TaxRate = TaxRate of decimal with
        static member op_Explicit x =
            match x with TaxRate r -> decimal r
    let createTaxRate rate =
        match rate with
        | r when r <= 0m || r >= 1m -> None
        | _ -> Some (TaxRate rate)
    
    type Tax = 
        { LocalRate: TaxRate 
          StateRate: TaxRate }
    let totalTaxRate tax =
        decimal tax.LocalRate + decimal tax.StateRate
    let applyTax tax amount =
        Math.Round (totalTaxRate tax * amount, 2); //WOAH! That's C#'s Math.Round function!
open Taxes

module Orders =
    type OrderId = OrderId of int
    type Order = 
        { Id: OrderId
          Customer: Customer
          Subtotal: decimal
          Shipping: ShippingMethod
          Tax: Tax }
    let getTotal order =
        order.Subtotal + getShippingCost order.Shipping |> applyTax order.Tax
    let completeOrder (orderId:OrderId) =
        printfn "Order complete: %A" orderId