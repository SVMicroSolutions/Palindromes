using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palindromes
{
    public class Program
    {
        static void Main(string[] args)
        {
            string str1 = "", str2 = "oyotta", str3 = "cecarar";
            string str4 = "bbb", str5 = "babbb";

            Console.WriteLine("Word: {0}, IsPalindrome {1}", str1, str1.GeneratePalindrome());
            Console.WriteLine("Word: {0}, IsPalindrome {1}", str2, str2.GeneratePalindrome());
            Console.WriteLine("Word: {0}, IsPalindrome {1}", str3, str3.GeneratePalindrome());
            Console.WriteLine("Word: {0}, IsPalindrome {1}", str4, str4.GeneratePalindrome());
            Console.WriteLine("Word: {0}, IsPalindrome {1}", str5, str5.GeneratePalindrome());

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

    
    }


    public static class StringExtension
    {
        public static bool IsPalindrome(this string StrToCheck)
        {
            var lwr = StrToCheck.ToLower().Where(c => char.IsLetter(c));
            return lwr.SequenceEqual(lwr.Reverse());
        }

        public static string GeneratePalindrome(this string StrToCheck)
        {
            string invalidValue = "-1";
            // Return a palindrome if possible, otherwise return -1 

            // If the length of the string empty, or only whitespace this can't be a palindrome
            if (String.IsNullOrWhiteSpace(StrToCheck))
            {
                return invalidValue; 
            }

            //sort the string into a dictonary (java hashset) to make processing the string easier
            var dict = new Dictionary<char, int>();
            foreach (char c in StrToCheck.ToLower().ToCharArray())
            {
                if (char.IsLetter(c))
                {
                    if (dict.ContainsKey(c))
                    {
                        dict[c]++;
                    }
                    else
                    {
                        dict.Add(c, 1); 
                    }
                }  
            }

            string rearrangedString = String.Empty;
            int numberOfOdds = 0;
            //Build up the palindrome
            StringBuilder sb = new StringBuilder(); 
            foreach (var keyPair in dict)
            {
                bool isOdd = false;
                // If we have more than one odd set of characters it won't be possible to build a palindrome
                if (keyPair.Value % 2 != 0)
                {
                    isOdd = true;
                    numberOfOdds++;
                }

                if (numberOfOdds < 2)
                {
      
                    bool insertFront = true;
                    for (int i = 0; i < keyPair.Value; i++)
                    {
                        //If we have more values, insert in the front/or rear as needed. 
                        if (i < keyPair.Value - 1)
                        {
                            
                            if (insertFront == true)
                            {
                                sb.Insert(0, keyPair.Key);
                                insertFront = false;
                            }
                            else
                            {
                                sb.Append(keyPair.Key);
                                insertFront = true;
                            }
                        }
                        else
                        {
                            // this is the last value for our odd pair, put it in the
                            // middle
                            if (isOdd)
                            {
                                sb.Insert(sb.Length / 2, keyPair.Key);
                            }
                            else
                            {
                                // follow the normal pattern
                                if (insertFront == true)
                                {
                                    sb.Insert(0, keyPair.Key);
                                    insertFront = false;
                                }
                                else
                                {
                                    sb.Append(keyPair.Key);
                                    insertFront = true;
                                }
                            }
                        }
                        
                    }
                }
                else
                {
                    return invalidValue;
                     
                }

            } 
            return sb.ToString();
        }
    }
}
