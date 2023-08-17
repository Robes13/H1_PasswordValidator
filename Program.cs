using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace PasswordValidator
    // Robert Pedersen H1 17-08-2023 Password Validator v1.0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Calling start method
            callStart();
        }
        #region Controller
        /// <summary>
        /// This method just calls the other methods, and calls the desired view/GUI methods. It basically runs the whole program.
        /// </summary>
        static void callStart()
        {
            do
            {
                Console.Clear();
                ExplainProgram();
                string password = ReadPassword();
                int result = TestingPassword(password);
                PasswordResult(result);
            }
            while (Console.ReadKey().Key == ConsoleKey.Y);
        }
        /// <summary>
        /// This tests if there is at least one upper - and lower case letter in the password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        static bool UpperAndLower(string password)
        {
            bool result = false;
            bool resultUpper = false;
            bool resultLower = false;
            for (int i = 0; i < password.Length; i++)
            {
                if (Char.IsUpper(password[i]))
                {
                    resultUpper = true;
                }
                if (Char.IsLower(password[i]))
                {
                    resultLower = true;
                }
            }
            if (resultUpper && resultLower)
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// This method tests if the password contains at least one number.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        static bool Number(string password)
        {
            bool result = false;
            for (int i = 0; i < password.Length; i++)
            {
                if (Char.IsNumber(password[i]))
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// This tests if there is at least one special character in the password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        static bool SpecialChar(string password)
        {
            bool result = false;
            for (int i = 0; i < password.Length; i++)
            {
                if (!Char.IsNumber(password[i]) && !Char.IsLetter(password[i]))
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// This just tests if the password is at least 12 long and not above 64.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        static bool PasswordLength(string password)
        {
            bool result = false;
            if (password.Length > 12 && password.Length < 64)
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// This method test if there is a sequence of numbers in a row, like 1234, or 6789 and so on. It will only test from numbers 1 to 9, and not numbers like 10 to 20.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        static bool PasswordSequenceNumbers(string password)
        {
            bool result = true;
            for (int i = 0; i < password.Length - 4; i++)
            {
                if (Char.IsNumber(password[i]) && Char.IsNumber(password[i + 1]) &&
                    Char.IsNumber(password[i + 2]) && Char.IsNumber(password[i + 3]))
                {
                    int numberOne = int.Parse(Convert.ToString(password[i]));
                    int numberTwo = int.Parse(Convert.ToString(password[i + 1]));
                    int numberThree = int.Parse(Convert.ToString(password[i + 2]));
                    int numberFour = int.Parse(Convert.ToString(password[i + 3]));
                    if (numberOne + 1 == numberTwo && numberTwo + 1 == numberThree && numberThree + 1 == numberFour)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// This method tests if there is a sequence of the same kind of letter, number or special character 4 times in a row. Like KKKK or 1111.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        static bool PasswordSequenceIsSame(string password)
        {
            bool result = true;
            for (int i = 0; i < password.Length - 4; i++)
            {
                if (password[i] == password[i + 1] && password[i + 1] == password[i + 2] && password[i + 2] == password[i + 3])
                {
                    result = false;
                }
            }
            return result;
        }
        /// <summary>
        /// This method is testing if there is a sequence of letters like ABCD or any other letters that fit somewhere in the alphabet.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        static bool AlphabetChecker(string password)
        {
            bool result = true;
            for (int i = 0; i < password.Length - 4; i++)
            {
                int asciiOne = (int)password[i];
                int asciiTwo = (int)password[i + 1];
                int asciiThree = (int)password[i + 2];
                int asciiFour = (int)password[i + 3];
                if (Char.IsLetter(password[i]) && Char.IsLetter(password[i + 1]) &&
                   Char.IsLetter(password[i + 2]) && Char.IsLetter(password[i + 3]))
                {
                    if (asciiOne + 1 == asciiTwo && asciiTwo + 1 == asciiThree && asciiThree + 1 == asciiFour)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
        /// <summary>
        ///  This method runs all the other methods. I could have done this method with just the return boolean values, but the flowchart I got from a classmate planned it this way, so I made it this way.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>If I is = 4 the passed word passed. But if at least 1 value is added to j, the password will still be passed, but will not be recommended.</returns>
        static int TestingPassword(string password)
        {
            int perfectPass = 0;
            int passed = 1;
            int notPassed = 2;
            int i = 0;
            int j = 0;
            if (UpperAndLower(password))
            {
                i++;
            }
            if (Number(password))
            {
                i++;
            }
            if (SpecialChar(password))
            {
                i++;
            }
            if (PasswordLength(password))
            {
                i++;
            }
            if (!PasswordSequenceNumbers(password))
            {
                j++;
            }
            if (!PasswordSequenceIsSame(password))
            {
                j++;
            }
            if (!AlphabetChecker(password))
            {
                j++;
            }
            if (i == 4 && j == 0)
            {
                return perfectPass;
            }
            else if (i == 4)
            {
                return passed;
            }
            else
            {
                return notPassed;
            }
        }
        #endregion
        #region View
        /// <summary>
        /// Explaining the program
        /// </summary>
        static void ExplainProgram()
        {
            Console.WriteLine("This program is a password validator!");
            Console.WriteLine("Password must be at least 12 characters long, and max 64 long.");
            Console.WriteLine("Password must have a mix of upper - and lowercase letters.");
            Console.WriteLine("There must be numbers in your password.");
            Console.WriteLine("Password must contain at least one special character.");
            Console.WriteLine("Please enter the password you want to validate: ");
        }
        /// <summary>
        /// This method prints out the result of the password strength. The int paramter is for deciding what to print.
        /// </summary>
        /// <param name="result"></param>
        static void PasswordResult(int result)
        {
            if (result == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Your password passed with flying colors!");
                Console.ResetColor();
                Console.WriteLine("Press y to try again or press any other key to exit program.");
            }
            else if (result == 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Your password passed, but has been downgraded.");
                Console.ResetColor();
                Console.WriteLine("Press y to try again or press any other key to exit program.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Your password didnt pass.");
                Console.ResetColor();
                Console.WriteLine("Press y to try again or press any other key to exit program.");
            }
        }
        #endregion
        #region Model
        /// <summary>
        ///  This method only reads password and returns it.
        /// </summary>
        /// <returns></returns>
        static string ReadPassword()
        {
            string password = Console.ReadLine();
            return password;
        }
        #endregion
    }
}
