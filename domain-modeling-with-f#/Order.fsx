open System;
open System.Text.RegularExpressions

module Contact =
    type EmailAddress = EmailAddress of string
    let createEmail email =
        if Regex.IsMatch(email, @"^\S+@\S+\.\S+$")
        then Some (EmailAddress email)
        else None
    let getValue = fun (EmailAddress email) -> email

    type ContactEmail = 
        | NoneSpecified
        | UnverifiedEmail of EmailAddress
        | VerifiedEmail of EmailAddress
    let createContactEmail email =
        match createEmail email with
        | Some emailAddress -> UnverifiedEmail emailAddress
        | None -> NoneSpecified
    let verifyEmail contactEmail =
        match contactEmail with
        | NoneSpecified -> contactEmail
        | UnverifiedEmail email -> 
            printfn "Verifiying email to %s" (getValue email)
            VerifiedEmail email
        | VerifiedEmail email -> contactEmail
open Contact

module Customers = 
    type CustomerId = CustomerId of int

    type Customer = 
        { Id: CustomerId 
          FirstName: string 
          MiddleName: string option 
          LastName: string
          ContactEmail: ContactEmail }

    //example implementation of the option type
    type MyOption<'T> =
        | Something of 'T
        | Nothing

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

    let addShippingCost method amount =
        let shippingCost = match method with
                           | Rush days -> 10.0m - (decimal days)
                           | Regular -> 2.99m
                           | NoRush -> 0.00m
        printfn "shipping: %M" shippingCost
        amount + shippingCost
open Shipping

module Taxes =
    type TaxRate = TaxRate of decimal with
            static member op_Explicit x =
                match x with TaxRate r -> decimal r
    let createTaxRate rate =
        //Note: this creation function doesn't prevent someone from creating
        //a tax rate improperly, this can either be done by convention
        //or it can be enforced by using tricks envolving modules
        match rate with
        | r when r <= 0m || r >= 1m -> None
        | _ -> Some (TaxRate rate)

    type Tax = 
        { LocalRate: TaxRate 
          StateRate: TaxRate }
    let totalTaxRate tax =
        decimal tax.LocalRate + decimal tax.StateRate
    let applyTax tax amount =
        totalTaxRate tax * amount + amount
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
        order.Subtotal |> applyTax order.Tax |> addShippingCost order.Shipping |> round
    let completeOrder (orderId:OrderId) =
        printfn "Order complete: %A" orderId
    let round (amount:decimal) =
        Math.Round(amount, 2) //WOAH! That's C#'s Math.Round function!
open Orders

//EXAMPLE USAGE
let exampleTotal =
    let contactInfo = createContactEmail "awebre@envoc.com"
    let austin = 
        { Id = CustomerId 98; 
          FirstName = "Austin"; 
          MiddleName = None; 
          LastName = "Webre"
          ContactEmail = contactInfo; }
    let rushOrder = Rush (Days 2)
    let localRate = createTaxRate 0.05m
    let stateRate = createTaxRate 0.045m

    match (localRate, stateRate) with
    | Some local, Some state -> 
        let tax = { LocalRate = local; StateRate = state; }
        let order = { Id = OrderId 99; Customer = austin; Subtotal = 12.99m; Shipping = rushOrder; Tax = tax;}
        getTotal order
    | _, _ -> 0.0m //Explcitly handle the case where local or state rate was invalid