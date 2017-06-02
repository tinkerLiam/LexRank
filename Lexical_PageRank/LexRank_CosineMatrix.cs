using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lexical_PageRank
{
    class LexRank_CosineMatrix
    {
        LexRank_BagOfWord Data;
        int size;
        public double[][] CosineMatrix;

        public int MatrixSize
        {
            get { return size; }
        }
        public LexRank_CosineMatrix(LexRank_BagOfWord bag)
        {
            Data = bag;

            size = Data.ListofSentenceInfo.Count;

            CosineMatrix = new double[size][];
            for (int i = 0; i < size; i++)
                CosineMatrix[i] = new double[size];

            StiffCosineMatrix();
        }
        private void StiffCosineMatrix()
        {
            for (int i = 0; i < Data.ListofSentenceInfo.Count; i++)
                for (int j = 0; j < Data.ListofSentenceInfo.Count; j++)
                {
                    CosineMatrix[i][j] = Cosine(i, j);
                }
        }
        private double Cosine(int aline, int bline)
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

            foreach (var item in DicofTemp)
            {
                bool judge = false;
                foreach (var item1 in Data.ListofSentenceInfo[aline].ListofWordsInfo)
                {
                    if (item1.word == item.Key)
                    {
                        foreach (var item2 in Data.ListofSentenceInfo[bline].ListofWordsInfo)
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

                if (judge)
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
        private double tfidf(int linenumber, string word)
        {
            double tf = 0;
            double idf = 0;
            Aux_SentenceInfo temp = Data.ListofSentenceInfo[linenumber];

            foreach (var item in temp.ListofWordsInfo)
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
        public void PrintCosineMatrix()
        {
            string path = @"C:\Users\Lian\Desktop\CosineMatrix.txt";
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            for (int i = 0; i < Data.ListofSentenceInfo.Count; i++)
            {
                for (int j = 0; j < Data.ListofSentenceInfo.Count; j++)
                    sw.Write(CosineMatrix[i][j].ToString("N10") + " ");
                sw.WriteLine();
            }
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }
        public void ShowCosineMatrix()
        {
            for (int i = 0; i < Data.ListofSentenceInfo.Count; i++)
            {
                for (int j = 0; j < Data.ListofSentenceInfo.Count; j++)
                    Console.Write(CosineMatrix[i][j] + " ");
                Console.WriteLine();
            }
        }
    }
}
