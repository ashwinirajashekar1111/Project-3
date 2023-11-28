using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConAppProject_3

{
    public class Student
    {
        public string Name { get; set; }
        public string Class { get; set; }

        public override string ToString()
        {
            return $"{Name}\t{Class}";
        }

        public void PrintStudentData(List<Student> students)
        {
            Console.WriteLine("Name\tClass");
            Console.WriteLine("----------------");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string choice;
            string filePath = "C:\\Mphasis -B233\\Project-3\\student.txt";

            if (File.Exists(filePath))
            {
                bool isHeaderLine = true;
                Student data = new Student();
                List<Student> students = new List<Student>();

                try
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        
                        string schoolName = reader.ReadLine();
                        Console.WriteLine($"School Name: {schoolName}");

                        
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            if (isHeaderLine)
                            {
                                isHeaderLine = false;
                                continue;
                            }
                            string[] values = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            if (values.Length == 2)
                            {
                                string name = values[0];
                                string studentClass = values[1];

                                Student student = new Student
                                {
                                    Name = name,
                                    Class = studentClass
                                };

                                students.Add(student);
                            }
                            else
                            {
                                Console.WriteLine("Invalid data format in the file.");
                            }
                        }
                    }

                    
                    Console.WriteLine("Unsorted Student Data:");
                    data.PrintStudentData(students);

                    do
                    {
                        Console.WriteLine("Choose the operations to be done on student data");
                        Console.WriteLine("1. Sorting");
                        Console.WriteLine("2. Searching");

                        if (int.TryParse(Console.ReadLine(), out int op))
                        {
                            switch (op)
                            {
                                case 1:
                                    {
                                        students = students.OrderBy(s => s.Name, StringComparer.OrdinalIgnoreCase).ToList();
                                        Console.WriteLine("\nSorted Student Data by Name:");
                                        data.PrintStudentData(students);
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.WriteLine("Enter the name to be searched");
                                        string searchName = Console.ReadLine();
                                        Student foundStudent = students.Find(s => s.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

                                        if (foundStudent != null)
                                        {
                                            Console.WriteLine($"\nFound Student: {foundStudent}");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"\nStudent with name '{searchName}' not found.");
                                        }

                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("Invalid choice.");
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a numeric choice.");
                        }

                        Console.WriteLine("Do you want to continue? Press y/n");
                        choice = Console.ReadLine();
                    } while (choice.Equals("y", StringComparison.OrdinalIgnoreCase));
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("File not found.");
                Console.ReadKey();
            }
        }
    }
}



