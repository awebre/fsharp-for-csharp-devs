type Student(name, gpa) =
    //private fields
    let mutable _name = name
    let mutable _gpa = gpa
    //private function
    let calculateLetterGrade gpa = 
        match gpa with
        | a when 3.3m < a -> "A"
        | b when 2.3m < b && b <= 3.3m -> "B"
        | c when 1.7m < c && c <= 2.3m -> "C"
        | d when 1.0m < d && d <= 1.7m -> "D"
        | _ -> "F"
    //public "property"
    member this.Name 
        with get() = _name
        and set(n) = _name <- n
    member this.Gpa
        with get() = _gpa
        and set(g) = _gpa <- g
    member this.LetterGrade
        with get() = calculateLetterGrade _gpa
    member this.PrintGrade() =
        printfn "%s has a %s" name this.LetterGrade