using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_PageRank
{
    class LexRank_TopicSentences
    {
        List<string> ListofSentences;
        double[] LexRankScores;
        SortedDictionary<double, List<int>> Dic;
        public int[] SortedSentences;

        public LexRank_TopicSentences(List<string> ListofLines,double[] ScoresInfo )
        {
            ListofSentences = ListofLines;
            LexRankScores = ScoresInfo;

            SortedAllSentences();
            DicToArray();
        }
        private void SortedAllSentences()
        {
            Dic = new SortedDictionary<double, List<int>>();
            
            for (int i = 0; i < ListofSentences.Count; i++)
            {
                if (Dic.ContainsKey(LexRankScores[i]))
                    Dic[LexRankScores[i]].Add(i);
                else
                {
                    List<int> temp = new List<int>();
                    temp.Add(i);
                    Dic.Add(LexRankScores[i], temp);
                }
            }


        }
        private void DicToArray()
        {
            SortedSentences = new int[ListofSentences.Count];

            int i = ListofSentences.Count - 1;
            foreach(var item in Dic)
            {
                foreach (var p in item.Value)
                    SortedSentences[i--] = p;
            }
        }
        public void ShowSentences()
        {
            for (int i = 0; i < ListofSentences.Count; i++)
                Console.WriteLine(SortedSentences[i]+"\t"+ListofSentences[SortedSentences[i]]);
        }
        public void ShowSentencesAndScores()
        {
            foreach (var item in Dic)
            {
                foreach(var p in item.Value)
                {
                    Console.WriteLine(item.Key + " " + SortedSentences[p]);
                }
            }
        }
    }
}
