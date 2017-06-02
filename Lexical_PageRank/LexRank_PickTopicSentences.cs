using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lexical_PageRank
{
    class LexRank_PickTopicSentences
    {
        List<string> ListofSentences;
        double[][] CosineMatrix;
        int[] SentencesOrder;
        int[] TopicThreePosition;
        List<string> ListofThree;

        public LexRank_PickTopicSentences(List<string> ListSentences,double[][] theCosineMatrix,int[] theSentencesOrder)
        {
            ListofSentences = ListSentences;
            CosineMatrix = theCosineMatrix;
            SentencesOrder = theSentencesOrder;

            TopicThreePosition = new int[3];
            ListofThree = new List<string>();

            SimilarityLimit(0.05);
        }
        private void SimilarityLimit(double threshold)
        {
            TopicThreePosition[0] = SentencesOrder[0];

            int order = 1;
            for (int i = order; i < SentencesOrder.Length; i++)
            {
                if (CosineMatrix[TopicThreePosition[0]][SentencesOrder[order]] < threshold)
                {
                    TopicThreePosition[1] = SentencesOrder[i];
                    order = i+1;
                    break;
                }
            }
            
            for (int i = order; i < SentencesOrder.Length; i++)
            {
                if (CosineMatrix[TopicThreePosition[0]][SentencesOrder[order]] < threshold&& CosineMatrix[TopicThreePosition[1]][SentencesOrder[order]] < threshold)
                {
                    TopicThreePosition[2] = SentencesOrder[i];
                    break;
                }

            }

            for (int i = 0; i < 3; i++)
                ListofThree.Add(ListofSentences[TopicThreePosition[i]]);
        }
        public void PrintThreeSentences()
        {
            string path = @"C:\Users\Lian\Desktop\ThreeSentences.txt";
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            for (int i = 0; i < 3; i++)
                sw.WriteLine(ListofThree[i]);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();

        }
        public void ShowSentencesOrder()
        {
            for (int i = 0; i < SentencesOrder.Length; i++)
                Console.WriteLine(SentencesOrder[i]);
        }
        public void ShowTopicThreePosition()
        {
            for (int i = 0; i < TopicThreePosition.Length; i++)
                Console.WriteLine(TopicThreePosition[i]);

        }
    }
}
