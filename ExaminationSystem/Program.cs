using System;
using System.Collections.Generic;

namespace ExaminationSystem;

public class Program
{
    static List<Question> questionBank = new List<Question>();

    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1- Doctor Mode");
            Console.WriteLine("2- Student Mode");
            Console.WriteLine("3- Exit");
            int choice = ReadInt("Choose: ", 1, 3);

            if (choice == 1) DoctorMode();
            else if (choice == 2) StudentMode();
            else return;
        }
    }

    static void DoctorMode()
    {
        int count = ReadInt("How many questions do you want to add? ", 1, int.MaxValue);
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine("Adding Question " + (i + 1) + ":");
            Console.WriteLine("Select Question Type: 1- True/False, 2- Choose One, 3- Multiple Choice");
            int type = ReadInt("", 1, 3);
            QuestionLevel level = ReadLevel("Enter Question Level (easy/medium/hard): ");
            string header = ReadText("Enter Question Header: ");
            int marks = ReadInt("Enter Question Marks: ", 1, int.MaxValue);

            if (type == 1)
            {
                bool correct = ReadBool("Enter Correct Answer (true/false): ");
                questionBank.Add(new TrueFalseQuestion(header, marks, level, correct));
            }
            else
            {
                string[] choices = new string[4];
                for (int j = 0; j < 4; j++)
                    choices[j] = ReadText("Enter choice " + (j + 1) + ": ");

                if (type == 2)
                {
                    int correct = ReadInt("Enter correct choice number (1-4): ", 1, 4);
                    questionBank.Add(new ChooseOneQuestion(header, marks, level, choices, correct));
                }
                else
                {
                    bool[] correct = ReadMultipleAnswers("Enter correct answers (comma separated, e.g. 1,3): ");
                    questionBank.Add(new MultipleChoiceQuestion(header, marks, level, choices, correct));
                }
            }
        }
    }

    static void StudentMode()
    {
        Console.WriteLine("Choose Exam Type: 1- Practical Exam, 2- Final Exam");
        int examType = ReadInt("", 1, 2);
        QuestionLevel level = ReadLevel("Enter Exam Level (easy/medium/hard): ");

        List<Question> available = new List<Question>();
        foreach (Question question in questionBank)
        {
            if (question.Level == level)
                available.Add(question);
        }

        int numberOfQuestions = available.Count;
        if (examType == 1 && available.Count > 0)
        {
            numberOfQuestions = available.Count / 2;
            if (numberOfQuestions == 0)
                numberOfQuestions = 1;
        }

        Console.WriteLine();
        Console.WriteLine("--- Exam Started ---");
        Console.WriteLine();

        if (numberOfQuestions == 0)
        {
            Console.WriteLine("No questions available for this exam.");
            return;
        }

        int score = 0;
        int totalMarks = 0;
        for (int i = 0; i < numberOfQuestions; i++)
        {

            Question question = available[i];

            question.Display(); // Polymorphism
            Console.Write("Your Answer: ");
            string answer = Console.ReadLine();

            if (question is TrueFalseQuestion)
            {
                while (answer.ToLower() != "true" && answer.ToLower() != "false")
                {
                    Console.Write("Invalid input! Enter True or False: ");
                    answer = Console.ReadLine();
                }
            }

            if (question.CheckAnswer(answer)) // Polymorphism
            {
                score += question.Marks;
            }

            totalMarks += question.Marks;
            Console.WriteLine();

        }
        Console.WriteLine("Your Result: " + score + " / " + totalMarks);
    }

    static int ReadInt(string message, int min, int max)
    {
        while (true)
        {
            Console.Write(message);

            string input = Console.ReadLine();
            bool valid = true;

            if (input == null || input == "")
            {
                valid = false;
            }
            else
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] < '0' || input[i] > '9')
                    {
                        valid = false;
                        break;
                    }
                }
            }

            if (valid == true)
            {

                {
                    int value = Convert.ToInt32(input);

                    if (value >= min && value <= max)
                    {
                        return value;
                    }
                }
            }

            Console.WriteLine("Invalid number! Please try again.");
        }
    }

    static string ReadText(string message)
    {
        while (true)
        {
            Console.Write(message);

            string value = Console.ReadLine();

            if (value != "" && value != null)
            {
                return value;
            }

            Console.WriteLine("Please enter a value.");
        }
    }

    static bool ReadBool(string message)
    {
        while (true)
        {
            Console.Write(message);

            string answer = Console.ReadLine();

            if (answer == "true" || answer == "True")
            {
                return Convert.ToBoolean(answer);
            }

            if (answer == "false" || answer == "False")
            {
                return Convert.ToBoolean(answer);
            }

            Console.WriteLine("Invalid input! Please enter true or false.");
        }
    }

    static QuestionLevel ReadLevel(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = (Console.ReadLine() ?? "").Trim().ToLower();
            if (input == "easy") return QuestionLevel.Easy;
            if (input == "medium") return QuestionLevel.Medium;
            if (input == "hard") return QuestionLevel.Hard;
            Console.WriteLine("Please enter easy, medium, or hard.");
        }
    }

    static bool[] ReadMultipleAnswers(string message)
    {
        while (true)
        {
            Console.Write(message);
            string answer = Console.ReadLine();

            string[] parts = answer.Split(',');
            bool[] selected = new bool[4];
            bool valid = true;

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] != "1" && parts[i] != "2" &&
                    parts[i] != "3" && parts[i] != "4")
                {
                    valid = false;
                    break;
                }

                int number = Convert.ToInt32(parts[i]);

                if (selected[number - 1] == true)
                {
                    valid = false;
                    break;
                }

                selected[number - 1] = true;
            }

            if (valid == true)
            {
                return selected;
            }

            Console.WriteLine("Invalid input! Enter unique numbers from 1 to 4, e.g. 1,3.");
        }
    }
}
