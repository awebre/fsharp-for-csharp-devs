open System

//this is a function which returns a unit
//(think void from C#)
let printThing = printfn "Printing thing...\n\n"

//lets call it to prove the point
printThing

//similarly, the following are all functions that return a value
let str = "string!"
let num = 0
let date = DateTimeOffset.UtcNow

//and this is a function which prints the results of those functions!
printfn "Printing Values:\nstr: %s\nnum: %i\ndate: %A\n\n" str num date
