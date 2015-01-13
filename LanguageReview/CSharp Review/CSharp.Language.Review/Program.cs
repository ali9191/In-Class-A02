// using statement is similar to the include
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp.Language.Review.Entities; // allows our Program class to see the other classes

namespace CSharp.Language.Review
{
    public class Program
    {
        // The following is a field - a field represents a piece of information that describes the class/object
        private static Random rnd = new Random();

        // This is a method. It is a special one because of its name - Main() - and thre should only be one of these in any given console application.
        public static void Main(string[] args)
        {
            // DemoToString();
            Program app = new Program(args);

            app.AssignMarks(30, 80);

            foreach(Student person in app.Students)
            {
                System.Console.WriteLine("Name: " + person.Name);
                foreach (EarnedMark item in person.Marks)
                    System.Console.WriteLine("\t" + item);
            }
        }

        private List<Student> _students = new List<Student>();

        public List<Student> Students
        {
            get { return _students; }
            set { _students = value; }
        }

        public Program(string[] studentNames)
        {
            WeightedMark[] courseMarks = new WeightedMark[4];
            courseMarks[0] = new WeightedMark("Quiz 1", 20);
            courseMarks[1] = new WeightedMark("Quiz 2", 20);
            courseMarks[2] = new WeightedMark("Exercises", 25);
            courseMarks[3] = new WeightedMark("Lab", 35);
            int[] possibleMarks = new int[4] { 25, 50, 12, 35 };

            foreach(string name in studentNames)
            {
                EarnedMark[] marks = new EarnedMark[4];
                for (int i = 0; i < possibleMarks.Length; i++)
                    marks[i] = new EarnedMark(courseMarks[i], possibleMarks[i], 0);
                Students.Add(new Student(name, marks));
            }
        }

        public void AssignMarks(int min, int max)
        {
            foreach (Student person in Students)
                foreach (EarnedMark item in person.Marks)
                    item.Earned = (rnd.Next(min, max) / 100.0) * item.Possible;
        }

        public static void DemoToString()
        {
            // Make a WeightedMark object
            WeightedMark evaluation = new WeightedMark("Assessment 1", 15);
            Console.WriteLine("Here is my evaluation object: " + evaluation.ToString());
            // Make an EarnedMark object
            EarnedMark myAssessment = new EarnedMark(evaluation, 20, 18);
            Console.WriteLine("Here is myAssessment object: " + myAssessment.ToString());

            // remake the evaluation object as a new object
            evaluation = new EarnedMark("Quiz 3", 10, 50, 40);
            Console.WriteLine("Here is my new evalation object: " + evaluation.ToString());
        }
    }
}

namespace CSharp.Language.Review.Entities
{
    public class Student
    {
        // Autoimplemented Properties
        public string Name { get; private set; }
        public EarnedMark[] Marks { get; private set; }

        public Student(string name, EarnedMark[] marks)
        {
            // TODO: Probably should do some validation, but leave that 'till later.....
            Name = name;
            Marks = marks;
        }
    }

    public class WeightedMark
    {
        // Properties
        public int Weight { get; private set; }
        public string Name { get; private set; }

        // Constructors
        public WeightedMark(string name, int weight)
        {
            // The sole job of the constructor is to put meaningful values into the fields/properties of the object.
            // This puts the object into a "known state".
            if (weight <= 0 || weight > 100)
                throw new Exception("Invalid weight: must be greater than or equal to zero and less than or equal to 100");
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name cannot be empty for the weighted item");
            Weight = weight;
            Name = name;
        }
    } // end of WeightedMark class

    public class EarnedMark : WeightedMark
    {
        public int Possible { get; private set; } // this is an auto-implemented property
        private double _Earned; // this is a field
        // The following is an explicitly implemented property
        public double Earned
        {
            get { return _Earned; } // specify what the getter does
            // specify what the setter does
            set
            {
                if (value < 0 || value > Possible)
                    throw new Exception("Invalid earned mark assigned");
                _Earned = value;
            }
        }

        public double Percent
        { get { return (Earned / Possible) * 100; } }

        public double WeightedPercent
        {
            get
            {
                return Percent * Weight / 100;
            }
        }

        // Constructors
        public EarnedMark(WeightedMark markableItem, int possible, double earned)
            : this(markableItem.Name, markableItem.Weight, possible, earned)
        {
        }

        public EarnedMark(string name, int weight, int possible, double earned)
            : base(name, weight)
        {
            if (possible <= 0)
                throw new Exception("Invalid possible marks");
            Possible = possible;
            Earned = earned;
        }

        // Methods
        public override string ToString()
        {
            // This overridden method is an example of polymorphism
            return string.Format("{0} ({1})\t - {2}% ({3}/{4}) \t- Weighted Mark {5}%",
                Name,
                Weight,
                Percent,
                Earned,
                Possible,
                WeightedPercent);
        }
    }
}