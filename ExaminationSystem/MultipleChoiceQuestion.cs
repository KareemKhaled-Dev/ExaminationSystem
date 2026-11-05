using System;
using System.Reflection.PortableExecutable;

namespace ExaminationSystem;

public class MultipleChoiceQuestion : Question
{
    public string[] Choices { get; set; }
    public bool[] CorrectChoices { get; set; }

    public MultipleChoiceQuestion(string header, int marks, QuestionLevel level, string[] choices, bool[] correctChoices)
        : base(header, marks, level)
    {
        Choices = choices;
        CorrectChoices = correctChoices;
    }

    public override void Display()
    {
        Console.WriteLine(Header + " (Multiple Choice) - Marks: " + Marks);
        for (int i = 0; i < Choices.Length; i++)
            Console.WriteLine((i + 1) + "- " + Choices[i]);
    }

    public override bool CheckAnswer(string answer)
    {
        bool[] selected = new bool[4];
        string[] parts = answer.Split(',');

        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i] != "1" && parts[i] != "2" &&
                parts[i] != "3" && parts[i] != "4")
            {
                return false;
            }

            int number = Convert.ToInt32(parts[i]);

            if (selected[number - 1] == true)
            {
                return false;
            }

            selected[number - 1] = true;
        }

        for (int i = 0; i < 4; i++)
        {
            if (selected[i] != CorrectChoices[i])
            {
                return false;
            }
        }

        return true;
    }
}
