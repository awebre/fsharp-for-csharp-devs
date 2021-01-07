#r "nuget: Newtonsoft.Json" //this will import a nuget package!

open System //These are C# imports, we'll get to them later
open Newtonsoft.Json
//Immutability By Default
module ImmutableByDefault = 
    //The "let" expression allows us to bind values
    let num = 0

    //Bound values cannot be reassigned
    num = num + 1 //this statement is not an assignment, its a boolean
    let isNumPlusOne = num = num + 1

    //mutability is accomplished with a special key word
    let mutable mutableNum = 0
    mutableNum <- mutableNum + 1 //this is an assignment

    //since we are binding values and not declaring variables,
    //there is no concept of an undeclared variable.
    //let undeclared //uncoment this for a bad time

    //we can also bind to functions
    let multiplyBy2 x = x * 2

module NonNullableByDefault = 
    //By default, types defined in F# do not allow null
    //instead we use the option type to explicitly handle "non value, values"
    let someVal = Some 0
    match someVal with
    | None -> printfn "Nothing here"
    | Some i -> printfn "Something %i" i

    //However, the null keyword still exists, because types defined outside of F# can be null
    //This means when we interop with other .NET langauges we will want to check for these null cases
    let nullStr = JsonConvert.DeserializeObject<string>("null");
    let optionStr = match nullStr with
                    | null -> None
                    | _ -> Some nullStr

    //Similarly, we can pass null to methods written in other .NET languages
    let ParseDateTime (str: string) =
        let (success, res) = DateTime.TryParse(str, null, System.Globalization.DateTimeStyles.AssumeUniversal)
        if success then
            Some(res)
        else
            None

    //its important to keep in mind that certain "base" types are defined outside of F#
    //therefore, we should be careful to check for nulls against those
    let mutable str = "Str"
    str <- null //this is totally valid F#

    //But in general, if we stay in F#-land and do not use the null keyword,
    //there will be no nulls