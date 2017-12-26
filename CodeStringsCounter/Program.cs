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
            if (args.Length == 0)
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
                dirs = Directory.GetFiles(Directory.GetCurrentDirectory(), format, SearchOption.AllDirectories);
            }
            catch (Exception e)
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
                   
                    var parent = Directory.GetParent(path);
                    var shortPath = Path.GetFileName(path);

                    while (parent.FullName != currentPath)
                    {
                        shortPath = Path.Combine(parent.Name, shortPath);
                        parent = Directory.GetParent(parent.FullName);
                    }


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
