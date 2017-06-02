using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_PageRank
{
    class Aux_WordsInfo
    {
        public string word;
        public int count;

        public Aux_WordsInfo(string theword)
        {
            word = theword;
            count = 1;
        }


    }

}
