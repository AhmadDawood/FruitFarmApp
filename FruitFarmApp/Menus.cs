using System;
using System.Collections.Generic;
using System.Linq;

namespace FruitFarmApp
{
    class Menus
    {
        // Declaration of our user defined Data types.

        List<FruitsPlucked> fList = new List<FruitsPlucked>();
        
        public void MainMenu()
        {
            //it displays Main Menu screen.
            Console.Clear();
            WriteAt("WelCome, Fruits Farm Management App!   ", 40, 3);
            WriteAt("(A) - Enter Work Done:  ", 25, 7);
            WriteAt("(B) - View Worker Summary:   ", 25, 8);
            WriteAt("(C) - Today's Work Summary:   ", 25, 9);
            Console.ForegroundColor = ConsoleColor.Red;
            WriteAt("(D) - Today's Fruit Basket Rates:  ", 25, 10);
            Console.ForegroundColor = ConsoleColor.Gray;
            WriteAt("(Esc) - Exit :   ", 25, 11);
            WriteAt(" Enter your Choice: ", 25, 14);
        }
        public void subMenu1()
        {
            //(A) - Enter Work Done: Menu
            FruitsPlucked f = new FruitsPlucked();

            try
            {
                Console.Clear();
                WriteAt("WelCome, Fruits Farm Management App!   ", 40, 3);
                WriteAt("(A) --> Enter Work Done:   ", 25, 7);
                WriteAt("Please Enter Worker ID :   ", 25, 9);
                f.WorkerID = Convert.ToInt32(Console.ReadLine());
                WriteAt("Please Enter Worker Name :  ", 25, 10);
                f.Name = Console.ReadLine().ToString();
                WriteAt("Number of Grapes Baskets :  ", 25, 11);
                f.GrapesBaskets = Convert.ToInt32(Console.ReadLine());
                WriteAt("Number of Orange Baskets :  ", 25, 12);
                f.OrangesBaskets = Convert.ToInt32(Console.ReadLine());
                WriteAt("Number of Mangoes Baskets : ", 25, 13);
                f.MangoesBaskets = Convert.ToInt32(Console.ReadLine());
                //Saves Data here.
                fList.Add(f);
                WriteAt("Data Saved Successfully, Press any key to Exit.", 25, 15);
                Console.ReadKey();
                Console.Clear();
            }
            catch (Exception e)
            {
                //System.FormatException   
                WriteAt(e.Message +" Press any Key to Exit! ", 25, 16);
                Console.ReadKey();
                //throw;
            }
        }
        public void subMenu2()
        {
            //Find Today's Work Summary for an individual Worker.

            int ID = 0;    // Default value for ID variable
            int yVal = 15; // Value for holding y coordinates of the screen and incrementing in loop.
            int total = 0;  // Variable for Calculating Amount

            Console.Clear();
            WriteAt("WelCome, Fruits Farm Management App!   ", 40, 3);
            WriteAt("(B) --> View Worker Summary:   ", 25, 7);
            WriteAt(" Please Enter Worker ID :  ", 25, 9);
            
            try
            {
                ID = Convert.ToInt32(Console.ReadLine());
                WriteAt(" ********************* Fruit Baskets Pick List ********************* ", 20, 11);
                
                Console.WriteLine("\n");
                WriteAt(" Grapes || Oranges || Mangoes ", 30, 13);
                Console.WriteLine("\n");
                
                //Code for Displaying Details.

                var workerList = from a in fList
                                 where a.WorkerID == ID
                                 select a;
                foreach (var wrkr in workerList)
                {
                    Console.SetCursorPosition(30, yVal);
                    yVal += 1;
                    Console.WriteLine(" {0} >> || {1}  ||  {2} || {3} ",
                          wrkr.Name , wrkr.GrapesBaskets, wrkr.OrangesBaskets, wrkr.MangoesBaskets);
                }

                WriteAt(" ********************************************************** ", 
                    20, Console.CursorTop + 1);
                Console.WriteLine("\n");
                
                //Code which Produces summary.
                var workerProduce = from a in fList
                                    where a.WorkerID == ID
                                    group a by a.WorkerID into Total
                                    select new
                                    {
                                        keyvalue = Total.Key,
                                        Total_Grapes_Baskets = Total.Sum(x => x.GrapesBaskets),
                                        Total_Oranges_Baskets = Total.Sum(x => x.OrangesBaskets),
                                        Total_Mangoes_Baskets = Total.Sum(x => x.MangoesBaskets)
                                    };
                Console.SetCursorPosition(20, Console.CursorTop +1);
                
                foreach (var wP in workerProduce)
                {
                    Console.WriteLine(" Bill (in RS)=  {0} x " + BasketRate.GrapesBasketRate +
                                          "|| {1} x " + BasketRate.OrangesBasketRate+
                                          "|| {2} x " + BasketRate.MangoesBasketRate,
                        wP.Total_Grapes_Baskets, wP.Total_Oranges_Baskets,
                                            wP.Total_Mangoes_Baskets);

                    total = ((wP.Total_Grapes_Baskets * BasketRate.GrapesBasketRate) +
                        (wP.Total_Oranges_Baskets * BasketRate.OrangesBasketRate) +
                        (wP.Total_Mangoes_Baskets * BasketRate.MangoesBasketRate));

                    WriteAt(" Amount (in RS) = " + total, 20, Console.CursorTop + 1);
                }
                WriteAt(" ************************************************************** ",
                    20, Console.CursorTop + 1);
                WriteAt(" Note:- Formula(([Grapes]x[Basket Rate])+([Oranges]x[Basket Rate])+([Mangoes]x[Basket Rate]))",
                    5, Console.CursorTop + 3);

                Console.SetCursorPosition(5, Console.CursorTop + 1);
                Console.WriteLine("\n\n"+" Press any key to Exit.");
                Console.ReadKey();
                Console.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //throw;
            }
        }
        public void subMenu3()
        {
            // (C) - Today's Work Summary Menu:
            
            //Variables for holding summary calculations.
            int total_Grapes_Baskets = 0;
            int total_Oranges_Baskets = 0;
            int total_Mangoes_Baskets = 0;
            int total = 0; //Variable which stores bill Amount

            Console.Clear();
            WriteAt("WelCome, Fruits Farm Management App!   ", 40, 3);
            WriteAt("(C) --> Today's Work Summary:   ", 25, 6);
            WriteAt(" ********************* Fruit Baskets List ********************* ", 20, 9);
            Console.WriteLine("\n");
            WriteAt("Worker ID >> Name >> Grapes || Oranges || Mangoes ", 30, 11);
            Console.WriteLine("\n");
            
            try
            {
                //Displaying each transaction.
                var workerList = from a in fList
                                 select a;
                foreach (var wrkr in workerList)
                {
                    Console.SetCursorPosition(35, Console.CursorTop + 1);
                    Console.WriteLine(" {0} >>  {1} >> {2} || {3} || {4} ",
                        wrkr.WorkerID, wrkr.Name, wrkr.GrapesBaskets, wrkr.OrangesBaskets, wrkr.MangoesBaskets);
                }
                WriteAt(" ************************************************************** ",
                    20, Console.CursorTop + 1);

                //Code which Produces summary.
                var workerProduce = from a in fList
                                    orderby a.WorkerID
                                    group a by a.WorkerID into Total
                                    select new
                                    {
                                        keyvalue = Total.Key,
                                        grapes_Baskets = Total.Sum(x => x.GrapesBaskets),
                                        oranges_Baskets = Total.Sum(x => x.OrangesBaskets),
                                        mangoes_Baskets = Total.Sum(x => x.MangoesBaskets)
                                    };
                //Calculating Summary Fields against each transaction. 
                
                foreach (var wP in workerProduce)
                {
                    total_Grapes_Baskets += wP.grapes_Baskets;
                    total_Oranges_Baskets += wP.oranges_Baskets;
                    total_Mangoes_Baskets += wP.mangoes_Baskets;
                }
                Console.SetCursorPosition(20, Console.CursorTop + 1);

                //On Screen number display according to formula.

                Console.WriteLine(" Bill (in RS)=  {0} x " + BasketRate.GrapesBasketRate +
                                          "|| {1} x " + BasketRate.OrangesBasketRate +
                                          "|| {2} x " + BasketRate.MangoesBasketRate,
                        total_Grapes_Baskets, total_Oranges_Baskets,
                                            total_Mangoes_Baskets);
                    //Total Bill Amount.
                    total = ((total_Grapes_Baskets * BasketRate.GrapesBasketRate) +
                        (total_Oranges_Baskets * BasketRate.OrangesBasketRate) +
                        (total_Mangoes_Baskets * BasketRate.MangoesBasketRate));
                
                    WriteAt(" Amount (in RS) = " + total, 20, Console.CursorTop + 1);

                WriteAt(" ************************************************************** ",
                    20, Console.CursorTop + 1);
                WriteAt(" Note:- Formula(([Grapes]x[Basket Rate])+([Oranges]x[Basket Rate])+([Mangoes]x[Basket Rate]))",
                    5, Console.CursorTop + 3);

                Console.SetCursorPosition(5, Console.CursorTop + 1);
                Console.WriteLine("\n\n" + " Press any key to Exit.");
                Console.ReadKey();
                Console.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //throw;
            }
        }
        public void subMenu4()
        {
            //This module displays subMenu4 which is about getting Today's rate for each type of baskets.
            // Without setting the rate No Calculation can be performed.

            try
            {
                Console.Clear();
                WriteAt("WelCome, Fruits Farm Management App!   ", 40, 3);
                Console.ForegroundColor = ConsoleColor.Red;
                WriteAt("(D) --> Enter Basket Rate:  ", 25, 7);
                Console.ForegroundColor = ConsoleColor.Gray;
                WriteAt("Please Enter Grapes Rate :    ", 25, 9);
                BasketRate.GrapesBasketRate = Convert.ToInt32(Console.ReadLine());
                WriteAt("Please Enter Oranges rates :  ", 25, 10);
                BasketRate.OrangesBasketRate = Convert.ToInt32(Console.ReadLine());
                WriteAt("Please Enter Mangoes rates :  ", 25, 11);
                BasketRate.MangoesBasketRate = Convert.ToInt32(Console.ReadLine());
                
                WriteAt("Data Saved Successfully, Press any key to Exit.", 25, 14);
                Console.ReadKey();
                Console.Clear();
            }
            catch (Exception e)
            {
                //System.FormatException
                WriteAt(e.Message + " Press any Key to Exit! ", 25, 15);
                Console.ReadKey();
                //throw;
            }
        }
        protected static void WriteAt(string s, int x, int y)
        {
            //A useful function for Reading and writing text at a specific location.
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}