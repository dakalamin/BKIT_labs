using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab5;

namespace Lab6
{
    class Program
    {
        delegate int SampleDelegate(String a, String b);

        static int GetLevDistance(String a, String b)
        {
            return LevDistance.Distance(a, b);
        }

        static int GetSummaryLength(String a, String b)
        {
            return a.Length + b.Length;
        }

        static void SampleDelegateAsParam(string consoleOut, string s1, string s2, SampleDelegate sampleDelegate)
        {
            int result = sampleDelegate(s1, s2);
            Console.WriteLine(consoleOut + result.ToString());
        }

        static void SampleDelegateCommon(string consoleOut, string s1, string s2, Func<string, string, int> sampleDelegate)
        {
            int result = sampleDelegate(s1, s2);
            Console.WriteLine(consoleOut + result.ToString());
        }


        static void Main(string[] args)
        {
            String str1 = "sampleStr1", str2 = "secondSampleString";

            Console.WriteLine("PART 1: \n");

            SampleDelegateAsParam("LevDistance: ", str1, str2, GetLevDistance);
            SampleDelegateAsParam("Summary length: ", str1, str2, GetSummaryLength);

            SampleDelegate sampleDelegate1 = new SampleDelegate(GetLevDistance);
            SampleDelegateAsParam("delegate based on method: ", str1, str2, sampleDelegate1);

            SampleDelegate sampleDelegate2 = GetSummaryLength;
            SampleDelegateAsParam("delegate based on compiler \"suggestion\": ", str1, str2, sampleDelegate2);

            SampleDelegate sampleDelegate3 = delegate (string a, string b) {
                return LevDistance.Distance(a, b);
            };
            SampleDelegateAsParam("delegate based on anonymous method: ", str1, str2, sampleDelegate3);

            SampleDelegateAsParam("delegate based on lambda-func 1(dist): ", str1, str2, (a, b) => { return LevDistance.Distance(a, b); });
            SampleDelegateAsParam("delegate based on lambda-func 2(sum): ", str1, str2, (a, b) => { return a.Length + b.Length; });

            SampleDelegateCommon("Common delegate based on method: ", str1, str2, GetLevDistance);

            SampleDelegateCommon("Common delegate based on lambda-func1: ", str1, str2, (a, b) => LevDistance.Distance(a, b));
            SampleDelegateCommon("Common delegate based on lambda-func2: ", str1, str2, (a, b) => a.Length + b.Length);

            Console.WriteLine("\n\nPART 2: \n");

            Type t = typeof(SampleClass);

            Console.WriteLine("Type " + t.FullName + " inherits from " + t.BaseType.FullName);
            Console.WriteLine("Namespace " + t.Namespace);
            Console.WriteLine("Assembly " + t.AssemblyQualifiedName);

            Console.WriteLine("\nConstructors:");
            foreach (var i in t.GetConstructors())
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("\nProperties:");
            foreach (var i in t.GetProperties())
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("\nMethods:");
            foreach (var i in t.GetMethods())
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("\npublic fields:");
            foreach (var i in t.GetFields())
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("\nPropeties with attribute:");
            foreach (var i in t.GetProperties())
            {
                object attrObj;
                if (GetPropertyAttribute(i, typeof(SampleAttribute), out attrObj))
                {
                    SampleAttribute attr = attrObj as SampleAttribute;
                    Console.WriteLine(i.Name + " - " + attr.Description);
                }
            }

            Console.WriteLine("\nCall method:");
            SampleClass sc = (SampleClass)t.InvokeMember(null, BindingFlags.CreateInstance, null, null, new object[] { });

            int s = 6;
            Console.WriteLine("{0} is divided by 10: {1}", s,
                t.InvokeMember("CheckDivide10", BindingFlags.InvokeMethod, null, sc, new object[] { s }));

            Console.Write("\n\nPress any button...");
            Console.ReadKey();
        }

        public static bool GetPropertyAttribute(PropertyInfo checkType, Type attrType, out object attr)
        {
            bool result = false;
            attr = null;

            var isAttribute = checkType.GetCustomAttributes(attrType, false);
            if (isAttribute.Length > 0)
            {
                result = true;
                attr = isAttribute[0];
            }

            return result;
        }
    }
}
