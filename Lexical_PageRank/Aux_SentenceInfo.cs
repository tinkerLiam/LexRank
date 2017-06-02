using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_PageRank
{
    class Aux_SentenceInfo
    {
        public int LineNumber;
        public List<Aux_WordsInfo> ListofWordsInfo;

        public Aux_SentenceInfo(int linenumber)
        {
            LineNumber = linenumber;
            ListofWordsInfo = new List<Aux_WordsInfo>();
        }

    }

}
