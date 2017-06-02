using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiebaNet.Segmenter;
using System.IO;

namespace Lexical_PageRank
{
    class LexRank_BagOfWord
    {
        string folderFullName;
        public int totalines;
        public List<string> ListofSentences;
        public Dictionary<string,List<int>> DicofWords;
        public List<Aux_SentenceInfo> ListofSentenceInfo;

        public LexRank_BagOfWord(string TextsDirectortName)
        {
            folderFullName = TextsDirectortName;
            ListofSentences = new List<string>();
            DicofWords = new Dictionary<string, List<int>>();
            ListofSentenceInfo = new List<Aux_SentenceInfo>();
            totalines = 0;

            GetAllSentenceInfo(GetTotalFilesName());
            GetAllWordInfo(GetTotalFilesName());
        }
        private void GetAllWordInfo(List<string> ListofFileInfo)
        {
            int linenum = 0;

            foreach (var path in ListofFileInfo)
            {
                StreamReader sr = new StreamReader(path, Encoding.UTF8 );
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (var item in GetIndependentWord(line))
                    {
                        if (DicofWords.ContainsKey(item))
                            DicofWords[item].Add(linenum);
                        else
                        {
                            List<int> t = new List<int>();
                            DicofWords.Add(item, t);
                            DicofWords[item].Add(linenum);
                        }
                    }
                    linenum++;
                }
            }
        }
        private void GetAllSentenceInfo(List<string> ListofFileInfo)
        {
            int linenum = 0;

            foreach (var path in ListofFileInfo)
            {
                StreamReader sr = new StreamReader(path, Encoding.UTF8);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    totalines++;
                    ListofSentences.Add(line);
                    Aux_SentenceInfo temp = new Aux_SentenceInfo(linenum++);

                    foreach (var item in GetIndependentWord(line))
                    {
                        bool judge = true;
                        foreach (var p in temp.ListofWordsInfo)
                        {
                            if (p.word == item)
                            {
                                p.count++;
                                judge = false;
                                break;
                            }
                        }
                        if (judge)
                        {
                            Aux_WordsInfo st = new Aux_WordsInfo(item);
                            temp.ListofWordsInfo.Add(st);
                        }
                    }
                    ListofSentenceInfo.Add(temp);
                }
            }

        }
        private List<string> GetIndependentWord(string line)
        {
            var segmenter = new JiebaSegmenter();
            var segments = segmenter.Cut(line);

            List<string> temp = segments.ToList();
            return temp;
        }
        private List<string> GetTotalFilesName()
        {
            DirectoryInfo TheFolder = new DirectoryInfo(folderFullName);
            List<string> ListofFileInfo = new List<string>();
            foreach (FileInfo NextFile in TheFolder.GetFiles())
                ListofFileInfo.Add(folderFullName + NextFile.Name);

            return ListofFileInfo;
        }
        public void ShowWordInfo()
        {
            foreach (var item in DicofWords)
            {
                Console.WriteLine(item.Key);
                foreach (var p in item.Value)
                    Console.Write(p + " ");
                Console.WriteLine();

            }
        }
        public void ShowSentenceInfo()
        {
            foreach (var item in ListofSentenceInfo)
            {
                Console.WriteLine(item.LineNumber);
                foreach(var p in item.ListofWordsInfo)
                    Console.Write(p.word + " ");
                Console.WriteLine();
            }
        }
    }
}
