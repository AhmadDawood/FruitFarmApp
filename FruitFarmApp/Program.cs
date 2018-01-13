using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FruitFarmApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            Console.Title = "Fruit Farm Manager " + (Assembly.GetExecutingAssembly().GetName().Version);

            ConsoleKeyInfo userKey;
            Menus m = new Menus();
            
            do
            {
                m.MainMenu();
                userKey = Console.ReadKey(false);

                switch (userKey.KeyChar.ToString().ToUpper())
                {
                    case "A":
                        m.subMenu1();
                        break;
                    case "B":
                        m.subMenu2();
                        break;
                    case "C":
                        m.subMenu3();
                        break;
                    case "D":
                        m.subMenu4();
                        break;
                    default:
                        m.MainMenu();
                        break;
                }
            } while (userKey.Key!= ConsoleKey.Escape);
        }
    }
}