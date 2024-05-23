using System;

namespace AtomreaktorSimulator
{
    class Program
    {
        static bool isReactorRunning = false;
        static double currentTemperature = 40;
        static double generatedEnergy = 0;
        static int selectedOption = 0;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Atomreaktor Szimulátor");
                string[] options = {
                    "Beindítás",
                    "Leállítás",
                    "Generált energia mennyiség",
                    "Hőfok",
                    "Hűtővíz beengedése",
                    "Kilépés"
                };

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.WriteLine(options[i]);
                    Console.ResetColor();
                }

                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = (selectedOption > 0) ? selectedOption - 1 : options.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption + 1) % options.Length;
                        break;
                    case ConsoleKey.Enter:
                        ExecuteOption(selectedOption);
                        break;
                }
            }
        }

        static void ExecuteOption(int option)
        {
            Console.Clear();
            switch (option)
            {
                case 0:
                    StartReactor();
                    break;
                case 1:
                    StopReactor();
                    break;
                case 2:
                    DisplayGeneratedEnergy();
                    break;
                case 3:
                    DisplayTemperature();
                    break;
                case 4:
                    CoolReactor();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Érvénytelen opció, próbálja újra.");
                    break;
            }

            Console.WriteLine("\nNyomjon meg egy gombot a folytatáshoz...");
            Console.ReadKey();
        }

        static void StartReactor()
        {
            if (isReactorRunning)
            {
                Console.WriteLine("A reaktor már működik.");
            }
            else
            {
                isReactorRunning = true;
                GenerateTemperature();
                GenerateEnergy();
                Console.WriteLine("A reaktor beindult.");
            }
        }

        static void StopReactor()
        {
            if (!isReactorRunning)
            {
                Console.WriteLine("A reaktor már le van állítva.");
                return;
            }

            if (currentTemperature >= 70)
            {
                Console.WriteLine("A reaktor hőmérséklete túl magas a leállításhoz. Hűtse le a reaktort először.");
            }
            else
            {
                isReactorRunning = false;
                Console.WriteLine("A reaktor biztonságosan leállítva.");
            }
        }

        static void DisplayGeneratedEnergy()
        {
            if (isReactorRunning)
            {
                GenerateEnergy();
                Console.WriteLine($"A reaktor által generált energia: {generatedEnergy:F2} gigawatt.");
            }
            else
            {
                Console.WriteLine("A reaktor nem működik, nem generál energiát.");
            }
        }

        static void DisplayTemperature()
        {
            if (isReactorRunning)
            {
                GenerateTemperature();
                Console.WriteLine($"A reaktor hőmérséklete: {currentTemperature:F2} °C.");
            }
            else
            {
                Console.WriteLine("A reaktor nem működik, nincs hőmérséklet adat.");
            }
        }

        static void CoolReactor()
        {
            if (isReactorRunning)
            {
                currentTemperature = 40;
                Console.WriteLine("A reaktor hőmérséklete 40 °C-ra hűlt.");
            }
            else
            {
                Console.WriteLine("A reaktor nem működik, nincs szükség hűtésre.");
            }
        }

        static void GenerateTemperature()
        {
            Random rand = new Random();
            currentTemperature = rand.Next(40, 101);
        }

        static void GenerateEnergy()
        {
            Random rand = new Random();
            generatedEnergy = generatedEnergy == 0 ? rand.NextDouble() * 10 : generatedEnergy + rand.NextDouble() * 10;
        }
    }
}
