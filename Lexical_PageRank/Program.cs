using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiebaNet.Segmenter;
using JiebaNet.Analyser;

namespace Lexical_PageRank
{
    class Program
    {
        static void Main(string[] args)
        {
            LexRank_BagOfWord BagOfWord = new LexRank_BagOfWord(@"C:\Users\Lian\Desktop\results\N07\");
            //BagOfWord.ShowSentenceInfo();
            LexRank_CosineMatrix CosineMatrix = new LexRank_CosineMatrix(BagOfWord);
            //CosineMatrix.PrintCosineMatrix();
            LexRank_PowerMethod PowerMethod = new LexRank_PowerMethod(CosineMatrix, 0.1);
            //PowerMethod.ShowDegreeInfo();
            //LexRank_Continuous Continuous = new LexRank_Continuous(CosineMatrix);
            //Continuous.ShowLexRankScores();
            LexRank_TopicSentences TopicSentences = new LexRank_TopicSentences(BagOfWord.ListofSentences, PowerMethod.LexRankScores);
            //TopicSentences.ShowSentencesAndScores();
            LexRank_PickTopicSentences PickTopicSentences = new LexRank_PickTopicSentences(BagOfWord.ListofSentences, CosineMatrix.CosineMatrix, TopicSentences.SortedSentences);
            //PickTopicSentences.ShowTopicThreePosition();
            //PickTopicSentences.ShowSentencesOrder();
            PickTopicSentences.PrintThreeSentences();
        }
    }
}
