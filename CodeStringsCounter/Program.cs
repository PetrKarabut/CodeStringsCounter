using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CodeStringsCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            string format;
            if (args == null || args.Length == 0 || args[0] == null)
            {
                Console.WriteLine("введите формат файлов(например *.cs)");
                format = Console.ReadLine();
            }
            else
            {
                format = args[0];
            }

            string[] dirs;
            string currentPath = Directory.GetCurrentDirectory();
            try
            {
                dirs = Directory.GetFiles(currentPath, format, SearchOption.AllDirectories);
            }
            catch(Exception e)
            {
                Console.WriteLine("ПОИСК ФАЙЛОВ НЕ УДАЛСЯ");
                Console.WriteLine();
                Console.WriteLine(e);
                Console.ReadKey();
                return;
            }

            var sharp = new CSharpStateMachine();
            var machine = sharp.GetMachineStructure();

            int count = 0;
            foreach (string path in dirs)
            {
                var runner = new StateMachineRunner(machine);
                using (var reader = new StreamReader(path))
                {
                    runner.RunMachine(reader);
                    string shortPath = path.Remove(0, currentPath.Length + 1);
                    Console.WriteLine(shortPath + "       " + runner.StringsCount);
                    count += runner.StringsCount;
                }
            }
            Console.WriteLine();
            Console.WriteLine("итого строк: {0}", count);
            Console.ReadKey();
        }
    }
}
