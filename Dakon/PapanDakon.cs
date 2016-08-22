using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dakon
{
    class PapanDakon
    {
        public List<Lubang> listLubang = null;

        private static PapanDakon papanDakon = null;

        public PapanDakon()
        {
            listLubang = new List<Lubang>();

            for (int i = 0; i < 16; i++)
            {
                if (i == 0 || i == 8)
                {
                    String player = "";
                    if (i == 0)
                        player = "Player 2";
                    else
                        player = "Player 1";
                    listLubang.Add(new Lubang(i, 0, player, true, true, false));
                }
                else if (i < 8)
                {
                    listLubang.Add(new Lubang(i, 7, "Player 1", false, false, false));
                }
                else
                {
                    listLubang.Add(new Lubang(i, 7, "Player 2", false, false, false));
                }
            }
        }

        public static PapanDakon GetInstance()
        {
            if (papanDakon == null)
            {
                papanDakon = new PapanDakon();
            }

            return papanDakon;
        }

        public void printPapanDakon()
        {
            Console.Write("\nP1:");
            for (int i = 1; i < 8; i++)
            {
                Console.Write("  "+i+"  ");
            }

            Console.WriteLine("\n");
            Console.Write("   | ");
            for (int i = 1; i < 8;i++)
            {
                Console.Write(listLubang[i].marblesCount);
                if (listLubang[i].marblesCount < 10 && i!=8)
                    Console.Write("  | ");
                else if(i!=8)
                    Console.Write(" | ");   
            }
            Console.Write("\n");
            if (listLubang[0].marblesCount < 10)
                Console.Write(" "+listLubang[0].marblesCount+"                                      "+listLubang[8].marblesCount);
            else
                Console.Write(" " + listLubang[0].marblesCount + "                                     " + listLubang[8].marblesCount);
            Console.Write("\n   | ");
            for (int j = 15; j > 8; j--)
            {
                Console.Write(listLubang[j].marblesCount);
                if (listLubang[j].marblesCount < 10)
                    Console.Write("  | ");
                else
                    Console.Write(" | ");
            }
            Console.WriteLine("\n");
            Console.Write("P2:");
            for (int i = 7; i > 0; i--)
            {
                Console.Write("  " + i + "  ");
            }
            Console.WriteLine("\n");
        }

        public int getTotalMarblesInStoreHouses()
        {
            return listLubang[0].marblesCount + listLubang[8].marblesCount;
        }

        public Boolean startSecondRound()
        {
            Console.WriteLine("Ronde 1 telah berakhir\nSkor sementara P1 : " + listLubang[8].marblesCount + " P2 : " + listLubang[0].marblesCount);
            Console.WriteLine("Ronde 2 akan dimulai, Player yang menang akan memulai duluan");

            int p1HouseCountWillGotMarbles = 0;
            int p2HouseCountWillGotMarbles = 0;
            if (listLubang[8].marblesCount > 49)
            {
                p1HouseCountWillGotMarbles = 7;
                listLubang[8].marblesCount = listLubang[8].marblesCount - 49;
            }
            else
            {
                p1HouseCountWillGotMarbles = (int)(listLubang[8].marblesCount / 7);
                listLubang[8].marblesCount = listLubang[8].marblesCount % 7;
            }

            if (listLubang[0].marblesCount > 49)
            {
                p2HouseCountWillGotMarbles = 7;
                listLubang[0].marblesCount = listLubang[0].marblesCount - 49;
            }
            else
            {
                p2HouseCountWillGotMarbles = (int)(listLubang[0].marblesCount / 7);
                listLubang[0].marblesCount = listLubang[0].marblesCount % 7;
            }

            if (p1HouseCountWillGotMarbles < 4 || p2HouseCountWillGotMarbles < 4)
                return false;

            //Refill P1 Houses
            for (int i = 1; i < 8; i++)
            {
                if (i <= (7 - p1HouseCountWillGotMarbles))
                {
                    listLubang[i].isEmpty = true;
                    listLubang[i].isBurnt = true;
                    listLubang[i].marblesCount = 0;
                }
                else
                {
                    listLubang[i].isEmpty = false;
                    listLubang[i].marblesCount = 7;
                }
            }

            //Refill P2 Houses
            for (int i = 9; i < 16; i++)
            {
                if (i <= (15 - p2HouseCountWillGotMarbles))
                {
                    listLubang[i].isEmpty = true;
                    listLubang[i].isBurnt = true;
                    listLubang[i].marblesCount = 0;
                }
                else
                {
                    listLubang[i].isEmpty = false;
                    listLubang[i].marblesCount = 7;
                }
            }

            //Proses Ngacang
            if (p1HouseCountWillGotMarbles != 7)
            {
                int i = 7 - p1HouseCountWillGotMarbles;
                int sisa = listLubang[8].marblesCount % 7;
                while (sisa > 0)
                {
                    listLubang[i].marblesCount++;
                    sisa--;
                    if (i == 1)
                        i = 7 - p1HouseCountWillGotMarbles;
                    else
                        i--;
                }
                listLubang[8].marblesCount = 0;
            }
            else
            {
                int i = 15 - p2HouseCountWillGotMarbles;
                int sisa = listLubang[0].marblesCount % 7;
                while (sisa > 0)
                {
                    listLubang[i].marblesCount++;
                    i--;
                    sisa--;
                    if (i == 1)
                        i = 15 - p2HouseCountWillGotMarbles;
                    else
                        i--;
                }
                listLubang[0].marblesCount = 0;
            }

            return true;
        }
    }
}
