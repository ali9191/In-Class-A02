// using statement is similar to the include
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Language.Review
{
    public class Program
    {
        // The following is a field - a field represents a piece of information that describes the class/object
        private static Random rnd = new Random();

        // This is a method. It is a special one because of its name - Main() - and thre should only be one of these in any given console application.
        public static void Main(string[] args)
        {
        }
    }
}

namespace CSharp.Language.Review.Entities
{
    public class Student
    {
        // Autoimplemented Properties
        public string Name { get; private set; }
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
    }
}