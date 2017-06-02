using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_PageRank
{
    class LexRank_TFIDF
    {
        LexRank_BagOfWord Data;

        public LexRank_TFIDF(LexRank_BagOfWord DataInfo)
        {
            Data = DataInfo;
        }
        private double Cosine(int aline,int bline)
        {
            double fraction = 0;
            double denominator1 = 0;
            double denominator2 = 0;

            Dictionary<string, int> DicofTemp = new Dictionary<string, int>();

            foreach (var item in Data.ListofSentenceInfo[aline].ListofWordsInfo)
                if (!DicofTemp.ContainsKey(item.word))
                    DicofTemp.Add(item.word, 1);
            foreach (var item in Data.ListofSentenceInfo[bline].ListofWordsInfo)
                if (!DicofTemp.ContainsKey(item.word))
                    DicofTemp.Add(item.word, 1);

            foreach(var item in DicofTemp)
            {
                bool judge = false;
                foreach(var item1 in Data.ListofSentenceInfo[aline].ListofWordsInfo)
                {
                    if(item1.word==item.Key)
                    {
                        foreach(var item2 in Data.ListofSentenceInfo[bline].ListofWordsInfo)
                        {
                            if (item2.word == item.Key)
                            {
                                judge = true;
                                break;
                            }
                        }
                        break;
                    }
                }

                if(judge)
                {
                    double temp = tfidf(aline, item.Key);
                    temp *= tfidf(bline, item.Key);
                    fraction += temp;
                }

            }

            foreach (var item in Data.ListofSentenceInfo[aline].ListofWordsInfo)
                denominator1 += Math.Pow(tfidf(aline, item.word), 2);
            denominator1 = Math.Sqrt(denominator1);

            foreach (var item in Data.ListofSentenceInfo[bline].ListofWordsInfo)
                denominator2 += Math.Pow(tfidf(bline, item.word), 2);
            denominator2 = Math.Sqrt(denominator2);

            return fraction / (denominator1 * denominator2);
        }
        public double tfidf(int linenumber,string word)
        {
            double tf = 0;
            double idf = 0;
            Aux_SentenceInfo temp = Data.ListofSentenceInfo[linenumber];

            foreach(var item in temp.ListofWordsInfo)
               if (item.word == word)
               {
                    int count = 0;
                    foreach (var p in temp.ListofWordsInfo)
                        count += p.count;

                    tf = (double)item.count / (double)count;
                    break;
               }
            idf = Math.Log10((double)Data.totalines / (double)Data.DicofWords[word].Count);
            return tf * idf;
        }
    }
}
