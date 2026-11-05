using System;

namespace ExaminationSystem;

public class TrueFalseQuestion : Question
{
    public bool CorrectAnswer { get; set; }

    public TrueFalseQuestion(string header, int marks, QuestionLevel level, bool correctAnswer)
        : base(header, marks, level)
    {
        CorrectAnswer = correctAnswer;
    }

    public override void Display()
    {
        Console.WriteLine(Header + " (True/False) - Marks: " + Marks);
    }

    public override bool CheckAnswer(string answer)
    {
        bool studentAnswer = Convert.ToBoolean(answer);

        if (studentAnswer == CorrectAnswer)
        {
            return true;
        }

        return false;
    }

}
