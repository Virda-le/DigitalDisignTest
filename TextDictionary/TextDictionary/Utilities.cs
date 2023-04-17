using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDictionary
{
    public static class Utilities
    {
        static char[] separators = Enumerable.Range(0, 256)
                              .Select(c => (char)c)
                              .Where(c => char.IsWhiteSpace(c) ||
                              char.IsPunctuation(c)).ToArray();
        public static string EnterPath()
        {
            string? path = Console.ReadLine(); ;
            while (!File.Exists(path))
            {
                Console.Clear();
                Console.WriteLine("File doesn't exist. Please enter corret path to file:");
                path = Console.ReadLine();
            }
            return path;
        }
        //read text,separate words and count them
        public static Dictionary<string,int> GetDictionary(string path)
        {
            
            var dictionary = File.ReadAllText(path)
                .Split(separators, StringSplitOptions.RemoveEmptyEntries).AsParallel()
                .Aggregate(new Dictionary<string, int>(),
                (dic, w) =>
                {
                    if (dic.ContainsKey(w.ToLower()))
                        dic[w.ToLower()]++;
                    else
                        dic.Add(w.ToLower(), 1);
                    return dic;
                })
                .Where(word => word.Key.Length > 2) //cut words that less than 2 letters
                .OrderBy(pair => -pair.Value)
                .ToDictionary(g => g.Key,g =>g.Value);
            return dictionary;
        }
        //get directory name and file
        public static string GetNewPath(string path)
        {
            
            string newPath = Path.GetDirectoryName(path) + @"\" + Path.Combine()
                + Path.GetFileNameWithoutExtension(path) + "(dictionary).txt";
            return newPath;
        }
        //create new file with dictionary
        //save dictionary
        public  static async Task  CreateAndSaveFile(string path)
        {
            using (FileStream fs = File.Create(GetNewPath(path)))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (var pair in GetDictionary(path))
                        await sw.WriteLineAsync($"{pair.Key} - {pair.Value}");
                }
            }
            
        }        
    }
}
