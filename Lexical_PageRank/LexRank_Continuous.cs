using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Lexical_PageRank
{
    class LexRank_Continuous
    {
        LexRank_CosineMatrix Data;
        double[][] StochasticMatrix;
        public double[] LexRankScores;

        public LexRank_Continuous(LexRank_CosineMatrix CosineMatrixInfo)
        {
            Data = CosineMatrixInfo;

            StochasticMatrix = new double[Data.MatrixSize][];
            for (int i = 0; i < Data.MatrixSize; i++)
            {
                StochasticMatrix[i] = new double[Data.MatrixSize];
            }

            StiffStochasticMatrix();
            LexRankScores = GetLexRankScores();
            Normalization();
        }
        private double[] GetLexRankScores()
        {
            var x = new DenseVector(Data.MatrixSize);
            for (int i = 0; i < Data.MatrixSize; i++)
                x[i] = 1;

            var A = new DenseMatrix(Data.MatrixSize);
            for (int i = 0; i < Data.MatrixSize; i++)
                for (int j = 0; j < Data.MatrixSize; j++)
                    A[i, j] = StochasticMatrix[i][j];

            for (int i = 0; i < 0; i++)
                A *= A;

            var B = A.Transpose();
            var y = B * x;

            double[] resultscores = y.ToArray();

            return resultscores;
        }
        private void StiffStochasticMatrix()
        {
            for (int i = 0; i < Data.MatrixSize; i++)
            {
                double count = 0;

                for (int j = 0; j < Data.MatrixSize; j++)
                {
                    count += Data.CosineMatrix[i][j];
                }
                for (int j=0;j<Data.MatrixSize;j++)
                {
                    StochasticMatrix[i][j] = Data.CosineMatrix[i][j] / count;
                }
            }

        }
        private void Normalization()
        {
            double max = 0;
            for (int i = 0; i < LexRankScores.Length; i++)
            {
                if (max < LexRankScores[i])
                    max = LexRankScores[i];
            }

            for (int i = 0; i < LexRankScores.Length; i++)
            {
                LexRankScores[i] = LexRankScores[i] / max;
            }
        }
        public void PrintLexRankScores()
        {
            string path = @"C:\Users\Lian\Desktop\LexRankScores.txt";
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            for (int i = 0; i < LexRankScores.Length; i++)
                sw.WriteLine(LexRankScores[i]);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();

        }
        public void ShowLexRankScores()
        {
            for (int i = 0; i < Data.MatrixSize; i++)
                Console.WriteLine(LexRankScores[i]);
        }
        public void ShowStochasticMatrix()
        {
            for (int i = 0; i < Data.MatrixSize; i++)
            {
                for (int j = 0; j < Data.MatrixSize; j++)
                    Console.Write(StochasticMatrix[i][j] + " ");
                Console.WriteLine();
            }
        }
    }
}
