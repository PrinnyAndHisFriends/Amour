using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Utility
{
    public static class CSVReader
    {
        static char lineSplitKey = ';';
        static string path = "Data/";

        public static Dictionary<string, T> ReadCSVToDict<T>(string file, Func<string[], T> func)
        {
            Dictionary<string, T> dict = new Dictionary<string, T>();
            string[] lines = LoadFileAndFilter(file);

            foreach (var line in lines)
            {
                if (LineFilter(line))
                    continue;
                string[] keys = line.Split(lineSplitKey);
                if (keys.Length == 1 && keys[0] == "")
                    continue;
                if (keys.Length > 0)
                    dict[keys[0]] = func(keys);
            }
            return dict;
        }

        public static List<T> ReadCSVToList<T>(string file, Func<string[], T> func)
        {
            List<T> list = new List<T>();
            string[] lines = LoadFileAndFilter(file);

            foreach (var line in lines)
            {
                if (LineFilter(line))
                    continue;
                string[] keys = line.Split(lineSplitKey);
                if (keys.Length == 1 && keys[0] == "")
                    continue;
                if (keys.Length > 0)
                    list.Add(func(keys));
            }
            return list;
        }


        public static List<string[]> ReadCSVToTextList(string file)
        {
            List<string[]> list = new List<string[]>();
            string[] lines = LoadFileAndFilter(file);

            int lastPos = 0;
            for (int i=0;i<lines.Length;i++)
            {
                if (LineFilter(lines[i]))
                {
                    lastPos = i + 1;
                    continue;
                }
                if (lines[i].Equals(""))
                {
                    if (i - lastPos != 0)
                    {
                        string[] texts = new string[i - lastPos];
                        for (int j=0;j<texts.Length;j++)
                        {
                            texts[j] = lines[lastPos + j];
                        }
                        list.Add(texts);
                    }
                    lastPos = i + 1;
                }
            }
            return list;
        }

        private static string[] LoadFileAndFilter(string fileName)
        {
            TextAsset t = ResourceManager.Load(path + fileName) as TextAsset;
            if (t == null)
            {
                Debug.LogError("Empty");
            }
            return t.text.Replace("\r", "").Split('\n');
        }

        private static bool LineFilter(string line)
        {
            if (line.Contains("//"))
                return true;
            return false;
        }
    }
}