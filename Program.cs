using System;
using System.Collections.Generic;

namespace NCR_test
{
    public class Program
    {
        static void Main(string[] args)
        {
            //running out of time to code up input validation
            //putting this high level try/catch to handle unexpected errors
            try
            {
                Console.WriteLine("Enter calculation (as per specification):");
                var mathString = Console.ReadLine();

                StringToNumber.Compute(mathString);
            }
            catch
            {
                Console.WriteLine(" 0 Unexpected error! Run the program again and adhere to the system specifications :)");
            }           
        }
    }

    public class StringToNumber
    {
        /// <summary>
        /// List with expected words.
        /// </summary>
        static List<string> numbersWords = new List<string>
        {
            "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten"
        };

        /// <summary>
        /// Dictionary to keep possible operators
        /// </summary>
        static Dictionary<string, char> operandAliases = new Dictionary<string, char>
        {
            {"add", '+' },
            {"plus", '+' },
            {"subtract", '-' },
            {"minus", '-' },
            {"less", '-' }
        };

        /// <summary>
        /// Prepares received string, sends it for calculation then displays result
        /// </summary>
        /// <param name="value"> User Input</param>
        public static void Compute(string value)
        {
            var lowerCaseInputString = value.ToLower();
            //split the string into a string array, array has both numbers and operators
            var incomingWords = lowerCaseInputString.Split(' ');

            //initialize lists to hold possible operators (operators), numeric words (words), and  numbers
            //numbers is the numeric rep on input words
            var numbers = new List<int>();
            var words = new List<string>();
            var operators = new List<char>();
            var sum = 0;

            //filter the input
            for (int i = 0; i < incomingWords.Length; i++)
            {
                //even number indexes contain numbers
                //odd number indexes contain operators
                if (i%2 == 0)
                {
                    words.Add(incomingWords[i]);
                    numbers.Add(numbersWords.IndexOf(incomingWords[i]));
                }
                else
                {
                    operators.Add(operandAliases[incomingWords[i]]);
                }
            }

            //send the lists we have for further calculation
            sum = CalculateRecursively(numbers, operators);
            Console.WriteLine($"Total: {sum}");
        }
        /// <summary>
        /// Recursive method to calculate sum using given numbers and operators, recursions halts when 
        /// there is two elements in the numbers list.
        /// </summary>
        /// <param name="numbers">integer values</param>
        /// <param name="operators">character values</param>
        /// <returns>final total</returns>
        public static int CalculateRecursively(List<int> numbers, List<char> operators)
        {
            var totalSum = 0;
            var newElelment = 0;
            var numbersCount = numbers.Count;
            var a = 0;
            var b = 0;

            if (numbersCount > 2) //recursion condition
            {
                a = numbers[0];
                b = numbers[1];

                //grab first two elements, add them or subtract them depending on the operator
                if (operators[0] == '+')
                {
                    newElelment = Add(a, b);
                }
                else
                {
                    newElelment = Subtract(a, b);
                }

                //remove elements that were involved in the previous calculation
                numbers.RemoveAt(0);
                numbers.RemoveAt(0);

                //insert the result of the previous calculation at the beginning of the list
                numbers.Insert(0,newElelment);

                //remove operator used in the previous calculation
                operators.RemoveAt(0);
                //Update numbersCount
                numbersCount = numbers.Count;

                //call the current method again
                CalculateRecursively(numbers, operators);
            }

            a = numbers[0];
            b = numbers[1];

            if (operators[0] == '+')
            {
                totalSum = Add(a, b);
            }
            else
            {
                totalSum = Subtract(a, b);
            }

            return totalSum;
        }

        /// <summary>
        ///Input Validation
        /// </summary>
        public static void ValidateUserInput()
        {
            //ran out of time
        }

        /// <summary>
        /// adds two integers
        /// </summary>
        /// <param name="a">int value</param>
        /// <param name="b">int value</param>
        /// <returns> sum of a and b</returns>
        public static int Add(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// subtracts two integers
        /// </summary>
        /// <param name="a">int value</param>
        /// <param name="b">int value</param>
        /// <returns>difference between a and b</returns>
        public static int Subtract(int a, int b)
        {
            return a - b;
        }
    }
}
