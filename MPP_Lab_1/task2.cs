using System;

namespace MPP_Lab_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int arrSize = 1000000;
            string[] words = new string[arrSize];
            int[] timesWordAppeared = new int[arrSize];
            string[] pages = new string[arrSize];
            string[] lines = System.IO.File.ReadAllLines(args[0]);
            int pageNum = 0;
            int wordCounter = 0;
            if (lines != null)
            {
                int linec = 0;
                lineLoop:
                if (linec % 45 == 0)
                {
                    pageNum++;
                }

                if (linec < lines.Length)
                {
                    string line = lines[linec];
                    string word = "";
                    int wordc = 0;
                    wordLoop:
                    if (wordc < line.Length)
                    {
                        char c = line[wordc];
                        if (c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z')
                        {
                            if (c >= 'A' && c <= 'Z')
                            {
                                word += (char) (32 + c);
                            }
                            else
                            {
                                word += c;
                            }
                        }
                        else
                        {
                            if (word.Length > 1)
                            {
                                int wordCheckc = arrSize - 1;
                                wordCheckLoop:
                                if (wordCheckc >= 0)
                                {
                                    if (words[wordCheckc] != null &&
                                        words[wordCheckc].Length == word.Length) // Check letters
                                    {
                                        int c1 = word.Length - 1;
                                        letterCheckLoop:
                                        if (c1 >= 0)
                                        {
                                            if (words[wordCheckc][c1] != word[c1]) goto wordIterationEnd;
                                            c1--;
                                            goto letterCheckLoop;
                                        }

                                        timesWordAppeared[wordCheckc]++;

                                        string lastPage = "";
                                        int lpc = pages[wordCheckc].Length - 1;
                                        lastPageLoop:
                                        if (lpc < 0 || pages[wordCheckc][lpc] == ' ')
                                        {
                                            if (lastPage != $"{pageNum}")
                                            {
                                                pages[wordCheckc] += $", {pageNum}";
                                            }
                                        }
                                        else
                                        {
                                            lastPage = pages[wordCheckc][lpc] + lastPage;
                                            lpc--;
                                            goto lastPageLoop;
                                        }

                                        wordCheckc = -2;
                                    }

                                    wordIterationEnd:
                                    wordCheckc--;
                                    goto wordCheckLoop;
                                }
                                else if (wordCheckc == -1)
                                {
                                    words[wordCounter] = word;
                                    pages[wordCounter] = $"{pageNum}";
                                    wordCounter++;
                                }

                                word = "";
                            }
                        }

                        wordc++;
                        goto wordLoop;
                    }

                    linec++;
                    goto lineLoop;
                }
            }

            //sorting by alphabetic order
            int i = 0;
            bubbleSortLoop:
            if (i < wordCounter)
            {
                int sortc = 0;
                string tmpWord;
                string tmpPage;
                int tmpAppeared;
                sortLoop:
                if (sortc < wordCounter - 1)
                {
                    bool nextLower = true;
                    int minLength = words[sortc].Length > words[sortc + 1].Length
                        ? words[sortc + 1].Length
                        : words[sortc].Length;
                    int charsc = 0;
                    compareLoop:
                    if (charsc < minLength)
                    {
                        if (words[sortc][charsc] != words[sortc + 1][charsc])
                        {
                            nextLower = words[sortc][charsc] > words[sortc + 1][charsc];
                            goto bubbleSort;
                        }

                        charsc++;
                        goto compareLoop;
                    }

                    bubbleSort:
                    if (nextLower)
                    {
                        tmpWord = words[sortc + 1];
                        tmpPage = pages[sortc + 1];
                        tmpAppeared = timesWordAppeared[sortc + 1];
                        words[sortc + 1] = words[sortc];
                        pages[sortc + 1] = pages[sortc];
                        timesWordAppeared[sortc + 1] = timesWordAppeared[sortc];
                        words[sortc] = tmpWord;
                        pages[sortc] = tmpPage;
                        timesWordAppeared[sortc] = tmpAppeared;
                    }

                    sortc++;
                    goto sortLoop;
                }

                i++;
                goto bubbleSortLoop;
            }

            int outc = 0;

            printLoop:
            if (outc < wordCounter)
            {
                if (timesWordAppeared[outc] <= 100)
                    Console.Out.WriteLine($"{words[outc]} - {pages[outc]}");
                outc++;
                goto printLoop;
            }
        }
    }
}