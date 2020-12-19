type Student(name, gpa) =
    //let thing //uncomment for a bad time
    let calculateLetterGrade gpa = 
        match gpa with
        | a when 3.3m < a -> "A"
        | b when 2.3m < b && b <= 3.3m -> "B"
        | c when 1.7m < c && c <= 2.3m -> "C"
        | d when 1.0m < d && d <= 1.7m -> "D"
        | _ -> "F"
    member this.Name =
        name
    member this.Gpa =
        gpa
    member this.LetterGrade() =
        calculateLetterGrade gpa
    member this.PrintGrade() =
        let letterGrade = this.LetterGrade()
        printfn "%s has a %s" name letterGrade