using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public class ParallelSearchThreadParams
    {
        public List<string> TmpList { get; set; }
        public string SearchWord { get; set; }

        public int LevMaxValue { get; set; }
        public int ThreadQuant { get; set; }
    }
}
