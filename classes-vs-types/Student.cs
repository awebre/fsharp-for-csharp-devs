public class Student {
    public Student(string name, decimal gpa)
    {
        Name = name;
        Gpa = gpa;        
    }

    public string Name { get; set; }
    public decimal Gpa { get; set; }
    public string LetterGrade => CalculateLetterGrade(Gpa);
    public void PrintGrade()
    { 
        Console.Log($"{Name} has a {LetterGrade}"); 
    }

    private string CalculateLetterGrade(decimal gpa)
    { //Note that we could achieve something similar to F#'s match using pattern matching 
        if (3.3 < gpa) 
        {
            return "A";
        } 
        else if (2.3 < gpa && gpa <= 3.3)
        {
            return "B";
        }
        else if (1.7 < gpa && gp <= 2.3)
        {
            return "C";
        }
        else if (1.0 < gpa && gpa <= 1.7)
        {
            return "D";
        }
        return "F";
    }
}