using System;
using WeCantSpell.Hunspell;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NHunspell;// doesn't work on linux or mac

namespace TextAnalysis
{
    public class Analysis
    {
        WordList dictionary;
        //Dictionary<string, int> translator;

        string dictionaryFile = "en_US.dic";

        public Analysis(string path1, string path2, bool spellcheck)
        {
            dictionary = WordList.CreateFromFiles(dictionaryFile);

            var text = OpenFile(path1);
            Console.Write(text);

            if (spellcheck)
            {
                text = Correct(text);// can be skipped to make it much faster! Or we could do it properly with nhunspell, but my mono on linux dosen't want to compile the available package
                Console.Write(text);
            }
            var dic1 = CountWords(text);

            var text2 = OpenFile(path2);
            Console.Write(text2);
            if (spellcheck)
            {
                text2 = Correct(text2);
                Console.Write(text2);
            }
            var dic2 = CountWords(text2);


            float similarity = Jaccard(dic1, dic2);

            Console.WriteLine("Jaccard index of " + path1 + " and " + path2 + " is " + similarity);
        }

        public float Jaccard(Dictionary<string, int> dictionary1, Dictionary<string, int> dictionary2)
        {
            var union = new Dictionary<string, int>();
            var difference = new Dictionary<string, int>();

            /*
            HashSet<string> hash1 = new HashSet<string>(keys1);
            hash1.UnionWith(keys2);


            foreach (string key in hash1)
            {
                dictionary1.TryGetValue(key, out int num1);
                dictionary1.TryGetValue(key, out int num2);

                union.Add(key, num1 + num2);
            }
            */

            // calculate union and difference
            foreach (string key in dictionary1.Keys)
            {
                dictionary1.TryGetValue(key, out int num1);

                if (dictionary2.ContainsKey(key))// add to union 
                {
                    dictionary2.TryGetValue(key, out int num2);

                    union.Add(key, num1 + num2);
                }
                else
                {
                    difference.Add(key, num1);//add to difference
                }
            }

            foreach (string key in dictionary2.Keys)
            {
                dictionary2.TryGetValue(key, out int num1);

                if (!dictionary1.ContainsKey(key))
                {
                    difference.Add(key, num1);// add to difernece
                }
            }

            int unionSum = 0;
            int differenceSum = 0;

            foreach (string key in union.Keys)
            {
                union.TryGetValue(key, out int sum);
                unionSum += sum;
            }

            foreach (string key in difference.Keys)
            {
                difference.TryGetValue(key, out int sum);
                differenceSum += sum;
            }

            Console.WriteLine("\n\n\nunion score: " + unionSum + " difference score: " + differenceSum);

            var jac = (float)unionSum / ((float)differenceSum + (float)unionSum);
            return jac;
        }

        public string Correct(string input)
        {
            Console.WriteLine("\npreforming spellcheck, this might take a while\n");
            var words = input.Split(' ');
            string output = null;

            for (int i = 0; i < words.Length; i++)
            {
                if (!dictionary.Check(words[i]))//if misspelled or unknown
                {
                    var suggestions = dictionary.Suggest(words[i]);
                    foreach (string str in suggestions)// get a suggestion, take the first one
                    {
                        //Console.WriteLine(str);
                        words[i] = str;
                        break;
                    }
                }
                output += words[i];// reassamble the sentence
                output += ' ';
            }
            return output;
        }

        public Dictionary<string, int> CountWords(string input)
        {
            //List<wordCount> uniqueWords = new List<wordCount>();
            var translator = new Dictionary<string, int>();

            var words = input.Split(' ');
            foreach (string word in words)
            {
                if (!translator.ContainsKey(word))
                {
                    translator.Add(word, 1);
                    //uniqueWords.Add(word);
                }
                else
                {
                    translator[word]++;//add one to the counter
                }
            }

            return translator;
        }

        public string OpenFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("Textfile not found: " + path);// if there is no file crash
            }

            string text = null;
            using (FileStream fs = File.OpenRead(path))// Open the stream and read it back.
            {
                byte[] b = new byte[fs.Length];
                fs.Read(b, 0, b.Length);
                text = Encoding.UTF8.GetString(b);
            }

            if (text.Length <= 0)
            {
                throw new Exception("text file empty, nothing to do. " + path);
            }

            return text;
        }
    }
}
