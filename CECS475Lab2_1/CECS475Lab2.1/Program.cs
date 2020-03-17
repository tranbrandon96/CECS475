// Name: Brandon Tran
// CECS 475: Phuong Nguyen
// Date: 1/29/2020
// Lab Assignment 2: IntegerSet

using System;

namespace CECS475Lab2_1
{
    class Program
    {
        /// <summary>
        /// 1. Create class IntegerSet. Each IntegerSet object can hold integers in the range 0-100. The set is represented by an array of bools.
        ///    Array element a[i] is true if integer i is in the set. Array element a[j] is false if integer j is not in the set. The parameterless
        ///    constructor initializes the array to the "empty set" (i.e., a set whose array representation contains all false values.
        /// </summary>
        public class IntegerSet
        {
            /// <summary>
            /// Set is represented by an array of bools within the range 0-100.
            /// </summary>
            private bool[] _integers;

            /// <summary>
            /// The default constructor initializes the array to the "empty set".
            /// </summary>
            public IntegerSet()
            {
                _integers = new bool[101];
            }

            /// <summary>
            /// Type conversion constructor is used to assign each value to the index of the object IntegerSet
            /// </summary>
            /// <param name="intArray"></param> Populate the object IntegerSet with the values from main.
            public IntegerSet(int[] intArray)
            {
                // Initialize the boolean array to the "empty set".
                _integers = new bool[101];

                // Create a for loop to check through the entire array
                for(int index = 0; index < intArray.Length; index++)
                {
                    // Store the value's from the array into a temporary value variable
                    int tempValue = intArray[index];

                    // Check if the number is valid from 0-100.
                    if((tempValue >= 0) && (tempValue <= 100))
                    {
                        // If the value is valid from 0-100, set the space of the boolean array to true because the value is there.
                        _integers[tempValue] = true;
                    }
                }
            }

            /// <summary>
            /// a. Method Union creates a third set that is the set-theoretic union of two existing sets (i.e., an element of the third set's array
            ///    is set to true if that element is true in either or both of the existing sets-otherwise, the element of the third set is set to false).
            /// </summary>
            /// <param name="rightSet"></param> Comparing the left and right set.
            /// <returns></returns> A union set that contains both numbers of both sets and shows duplicate only once.
            public IntegerSet Union(IntegerSet secondSet)
            {
                // Create a temporary set to return back to the main that acts as the Union Set
                IntegerSet tempSet = new IntegerSet();

                // Create a for loop to traverse through both sets to form a separate Union Set
                for (int index = 0; index <= 100; index++)
                {
                    // Condition statement to combine both sets while dealing with duplicates
                    if ((_integers[index] == true) || (secondSet._integers[index] == true))
                    {
                        tempSet._integers[index] = true;
                    }
                }

                // Return Temporary Set
                return tempSet;
            }

            /// <summary>
            /// b. Method Intersection creates a third set which is the set-theoretic intersection of two existing sets (i.e., an element of the third set's
            ///    array is set to false if that element is false in either or both of the existing sets - otherwise, the element of the third set is set to true).
            /// </summary>
            /// <param name="rightSet"></param> Comparing the left and right set
            /// <returns></returns> A intersection set that shows the repeating values from both left and right sets.
            public IntegerSet Intersection(IntegerSet secondSet)
            {
                // Create a temporary set to return back to the main that acts as the Intersection Set.
                IntegerSet tempSet = new IntegerSet();

                // Create a for loop to traverse through both sets to form a separate Intersection Set
                for (int index = 0; index <= 100; index++)
                {
                    // Condition statement to combine both sets to find commonality. 
                    if ((_integers[index] == true) && (secondSet._integers[index] == true))
                    {
                        tempSet._integers[index] = true;
                    }
                }

                // Return Intersection Set
                return tempSet;
            }


            /// <summary>
            /// c. Method InsertElement inserts a new integer k into a set (by setting a[k] to true).
            /// </summary>
            /// <param name="value"></param> Insert value into the set.
            public void InsertElement(int value)
            {
                _integers[value] = true;
            }

            /// <summary>
            /// d. Method DeleteElement deletes integer m (by setting a[m] to false).
            /// </summary>
            /// <param name="value"></param> Delete value from the set.
            public void DeleteElement(int value)
            {
                _integers[value] = false;
            }

            /// <summary>
            /// e. Method ToString returns a string containing a set as a list of numbers separated by spaces. Include only those elements that are present in the set.
            ///    Use --- to represent an empty set.
            /// </summary>
            /// <returns></returns> 
            override public string ToString()
            {
                // Initial Declaration Variables
                string values = "";
                bool temp;

                // Traverse through entire set
                for (int index = 0; index <= 100; index++)
                {
                    // Check if the value is valid from 0 to 100
                    if((index >= 0) && (index <= 100))
                    {
                        temp = _integers[index];
                    }
                    // If not, dont print out.
                    else
                    {
                        temp = false;
                    }

                    // If the value is valid, print out the value.
                    if (temp == true)
                    {
                        values = values + " " + index;

                    }
                }
                return values;

            }

            /// <summary>
            /// f. Method IsEqualTo determines whether two sets are equal.
            /// </summary>
            /// <param name="rightSet"></param>
            /// <returns> 1. Return True if both set has the same index values
            ///           2. Return False if both set has different index values</returns>
            public bool IsEqualTo(IntegerSet rightSet)
            {
                // Traverse through both sets
                for (int index = 0; index <= 100; index++)
                {
                    // If the size is the same, return true.
                    if (_integers[index] == rightSet._integers[index])
                    {
                        return true;
                    }
                }

                // Otherwise, return false;
                return false;

            }
            /// <summary>
            /// Prompt the user to enter values into a set. Once the values are entered into the set, the index of the boolean array is set to true.
            /// </summary>
            public void InputSet()
            {
                // Initial Variable Declarations
                string inputArray;
                int value;

                // Prompt the user to enter a value into the set.
                Console.WriteLine("Enter a value to insert in the set: (Enter -1 to stop)");
                do
                {
                    inputArray = Console.ReadLine();
                    // C# Syntax to TypeCast
                    value = Convert.ToInt32(inputArray);
                    if ((value >= 0) && (value <= 100))
                    {
                        _integers[value] = true;
                    }

                } while (value != -1);
            }
        }


        static void Main(string[] args)
        {
            // initialize two sets
            Console.WriteLine("Input Set A");
            IntegerSet set1 = new IntegerSet();
            set1.InputSet();
            Console.WriteLine("\nInput Set B");
            IntegerSet set2 = new IntegerSet();
            set2.InputSet();
          
            IntegerSet union = set1.Union(set2);
            IntegerSet intersection = set1.Intersection(set2);

            // prepare output
            Console.WriteLine("\nSet A contains elements:");
            Console.WriteLine(set1.ToString());
            Console.WriteLine("\nSet B contains elements:");
            Console.WriteLine(set2.ToString());
            Console.WriteLine(
            "\nUnion of Set A and Set B contains elements:");
            Console.WriteLine(union.ToString());
            Console.WriteLine(
            "\nIntersection of Set A and Set B contains elements:");
            Console.WriteLine(intersection.ToString());

            // test whether two sets are equal
            if (set1.IsEqualTo(set2))
                Console.WriteLine("\nSet A is equal to set B");
            else
                Console.WriteLine("\nSet A is not equal to set B");

            // test insert and delete
            Console.WriteLine("\nInserting 77 into set A...");
            set1.InsertElement(77);
            Console.WriteLine("\nSet A now contains elements:");
            Console.WriteLine(set1.ToString());

            Console.WriteLine("\nDeleting 77 from set A...");
            set1.DeleteElement(77);
            Console.WriteLine("\nSet A now contains elements:");
            Console.WriteLine(set1.ToString());

            // test constructor
            int[] intArray = { 25, 67, 2, 9, 99, 105, 45, -5, 100, 1 };
            IntegerSet set3 = new IntegerSet(intArray);

            Console.WriteLine("\nNew Set contains elements:");
            Console.WriteLine(set3.ToString());
         // end Main
    }
    }
}
