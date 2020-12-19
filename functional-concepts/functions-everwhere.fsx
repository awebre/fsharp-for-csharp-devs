open System

//This module could be in another file somewhere
module MyModule = 
    //"let" expressions allow us to assign a function to a variable
    //in this case the functions return type is unit
    //(think void from C#)
    let printThing = printfn "Thing!\n"

//lets call it just to be sure its a function
MyModule.printThing

//similarly, the following let expressions assign various data types
let str = "string!"
let num = 0
let date = DateTimeOffset.UtcNow //Holy DateTimeOffset, Batman!

//and this is a function which prints the results of those expressions (including the function from above)
let printValues = printfn "Printing Values:\nstr: %s\nnum: %i\ndate: %A\nprintThing:%A\n\n" str num date MyModule.printThing

printValues