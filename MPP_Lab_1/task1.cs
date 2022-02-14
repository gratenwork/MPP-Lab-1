using System;

namespace MPP_Lab_1
{
    internal class Program
    {
        //Provide program with FULL path to a desired file you want to read in args[0]
        public static void Main(string[] args)
        {
            int arrSize = 10000;
            int numOfWords = 0;
            string[] words = new string[arrSize];
            int[] counts = new int[arrSize];
            string input = System.IO.File.ReadAllText(args[0]);
            string tmp = "";
            int wordCounter = 0;
            int inputPointer = 0;
            Console.Write("Num of words to print [0 = print all]: ");
            numOfWords = Int32.Parse(Console.ReadLine());
            if (input != null)
            {
                loop:
                if (input.Length > inputPointer)
                {
                    char inputChar = input[inputPointer];
                    if (inputChar >= 'A' && inputChar <= 'Z' || inputChar >= 'a' && inputChar <= 'z')
                    {
                        if (inputChar > 'A' && inputChar < 'Z')
                            tmp += (char) (32 + inputChar);
                        else tmp += inputChar; // Get word from text
                    }
                    else
                    {
                        if (tmp.Length > 3)
                        {
                            int c = arrSize - 1;
                            wordCheckLoop:
                            if (c >= 0)
                            {
                                if (words[c] != null && words[c].Length == tmp.Length) // Check letters
                                {
                                    int c1 = tmp.Length - 1;
                                    letterCheckLoop:
                                    if (c1 >= 0)
                                    {
                                        if (words[c][c1] != tmp[c1]) goto wordIterationEnd;
                                        c1--;
                                        goto letterCheckLoop;
                                    }

                                    counts[c]++;
                                    c = -2;
                                }

                                wordIterationEnd:
                                c--;
                                goto wordCheckLoop;
                            }
                            else if (c == -1)
                            {
                                words[wordCounter] = tmp;
                                counts[wordCounter]++;
                                wordCounter++;
                            }
                        }

                        tmp = "";
                    }

                    inputPointer++;
                    goto loop;
                }
                else
                {
                    //Bubble sort
                    int numTmp = 0;
                    int write = 0;
                    outerSortLoop:
                    if (write < arrSize)
                    {
                        int sort = 0;
                        innerSortLoop:
                        if (sort < arrSize - 1)
                        {
                            if (counts[sort] < counts[sort + 1])
                            {
                                tmp = words[sort + 1];
                                numTmp = counts[sort + 1];
                                words[sort + 1] = words[sort];
                                counts[sort + 1] = counts[sort];
                                words[sort] = tmp;
                                counts[sort] = numTmp;
                            }

                            sort++;
                            goto innerSortLoop;
                        }

                        write++;
                        goto outerSortLoop;
                    }

                    //Output
                    int c = 0;
                    wordCounter = wordCounter > numOfWords && numOfWords != 0 ? numOfWords : wordCounter;
                    outLoop:
                    if (c < wordCounter)
                    {
                        Console.WriteLine($"{words[c]} - {counts[c]}");
                        c++;
                        goto outLoop;
                    }
                }
            }
        }
    }
}