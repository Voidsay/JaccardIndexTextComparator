using System;

namespace TextAnalysis
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var path1 = "test.txt";
            var path2 = "compair.txt";

            var analysis = new Analysis(path1, path2, true);
            return;
        }
    }
}
