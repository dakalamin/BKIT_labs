using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    class SampleClass
    {
        public SampleClass() { }
        public SampleClass(string str) { }
        public SampleClass(int b) { }

        public string mStr
        {
            get { return MStr; }
            set { MStr = value; }
        }

        [SampleAttribute(Description = "this field for random number generator")]
        public Random Random { get; set; }
        public string MStr { get => MStr1; set => MStr1 = value; }
        public string MStr1 { get; set; }

        [SampleAttribute(Description = "this field for time")]
        public TimeSpan LastTime { get; set; }

        public bool CheckDivide10(int a) => a % 10 == 0;

        public int Len => field1.Length;

        public string field1;
        public Array  field2;
    }
}
