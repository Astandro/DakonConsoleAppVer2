using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dakon
{
    class Program
    {
        //Referensi : https://www.youtube.com/watch?v=zAGYhT05AIc
        //Game contoh : http://dakon-the-game.appspot.com/
        //Kondisi-kondisi permainan dakon :
        //Ronde 1
        // 1. berjalan secara berputar searah jarum jam
        // 2. deposit biji pada store house (lubang besar) milik sendiri bukan milik musuh
        // 3. jika biji di tangan habis pada lubang yang terdapat biji, maka perputaran dilanjutkan dengan mengambil seluruh biji pada lubang tersebut
        // 4. jika biji di tangan habis pada store house (lubang besar) milik sendiri, maka player tersebut mendapat jatah giliran lagi
        // 5. jika biji di tangan habis pada lubang kosong milik musuh, maka giliran pemain tersebut berakhir
        // 6. jika biji di tangan habis pada lubang kosong milik sendiri, maka ambil seluruh biji pada lubang milik musuh yang berseberangan
        // 
        //Ronde 2
        //1. sama seperti ronde 1, hanya saja sebagai permulaan, seluruh isi storehouse didistribusikan ke masing2 lubang, 
        //   jika tidak cukup untuk memenuhi 1 lubang, maka sisanya dibiarkan di storehouse
        //2. jika ada lubang yang kosong/tidak terisi, maka lubang tersebut dinyatakan burnt/tidak bisa digunakan

        static PapanDakon papanDakon1;
        static PointerTangan tangan;

        static void Main(string[] args)
        {
            papanDakon1 = PapanDakon.GetInstance();
            tangan = new PointerTangan();

            //Round 1
            //Print Kondisi awal Papan Dakon pada Round 1 
            papanDakon1.printPapanDakon();

            //Permainan dimulai oleh Player 1
            tangan.playerInTurn = "Player 1";

            //Looping giliran pada Ronde 1
            while(papanDakon1.getTotalMarblesInStoreHouses()!=98)
            {
                //Get user input
                pilihDanAmbilIsiLubang();

                //Geser posisi tangan selama permainan searah jarum jam
                while (tangan.marblesDiTangan != 0)
                {
                    geserPosisiTangan();
                }

                //jika posisi berhenti bukan di storehouse, maka giliran pemain tersebut berakhir
                if (!papanDakon1.listLubang[tangan.indexPosisi].isStoreHouse)
                    switchPlayer();
                else
                    printGetAdditionlRoundMessage();  //mendapat jatah 1 giliran lagi

                papanDakon1.printPapanDakon();
            }

            //Inisialisasi Player yang berhak memulai duluan di ronde ke 2
            setSecondRoundFirstPlayer();
            //Inisialisasi ronde ke 2
            papanDakon1.startSecondRound();
            //Print kondisi awal papan dakon pada ronde ke 2
            papanDakon1.printPapanDakon();

            //Looping giliran pada Ronde 2
            while (papanDakon1.getTotalMarblesInStoreHouses() != 98)
            {
                //Get user input
                pilihDanAmbilIsiLubang();

                //Geser posisi tangan selama permainan searah jarum jam
                while (tangan.marblesDiTangan != 0)
                {
                    geserPosisiTangan();
                }

                //jika posisi berhenti bukan di storehouse, maka giliran pemain tersebut berakhir
                if (!papanDakon1.listLubang[tangan.indexPosisi].isStoreHouse)
                    switchPlayer();
                else
                    printGetAdditionlRoundMessage();  //mendapat jatah 1 giliran lagi

                papanDakon1.printPapanDakon();
            }

            showFinalMessage();

            Console.ReadLine();
        }

        public static void showFinalMessage()
        {
            Console.WriteLine("Ronde 2 telah berakhir\nSkor akhir P1 : " + papanDakon1.listLubang[8].marblesCount + " P2 : " + papanDakon1.listLubang[0].marblesCount);
            String winner = "";

            if (papanDakon1.listLubang[8].marblesCount > papanDakon1.listLubang[0].marblesCount)
                winner = "Player 1";
            else
                winner = "Player 2";

            Console.WriteLine("Permainan dimenangkan oleh "+winner);
        }

        public static void printGetAdditionlRoundMessage()
        {
            Console.WriteLine(tangan.playerInTurn + " mendapatkan jatah giliran lagi karena berhenti di storehouse");
        }

        public static void setSecondRoundFirstPlayer()
        {
            if (papanDakon1.listLubang[0].marblesCount > papanDakon1.listLubang[8].marblesCount)
                tangan.playerInTurn = "Player 2";
            else
                tangan.playerInTurn = "Player 1";
        }

        public static void pilihDanAmbilIsiLubang()
        {
            Console.Write("\nGiliran " + tangan.playerInTurn + ", Silahkan pilih posisi (1-7) : ");

            //Player 1 memilih house yang akan digunakan sebagai titik awal giliran
            tangan.indexPosisi = getHousePilihanUser(tangan.playerInTurn);

            //Ambil seluruh marbles pada lubang/house tersebut
            tangan.marblesDiTangan = papanDakon1.listLubang[tangan.indexPosisi].marblesCount;
            papanDakon1.listLubang[tangan.indexPosisi].marblesCount = 0;
            papanDakon1.listLubang[tangan.indexPosisi].isEmpty = true;

            Console.WriteLine(tangan.playerInTurn + " memilih posisi : " + tangan.indexPosisi + " dengan jumlah marbles " + tangan.marblesDiTangan);
        }

        public static int getHousePilihanUser(string playerInThisTurn)
        {
            bool isValidInput = false;
            int houseNum=-1;
            bool isNumeric;
            string input;
            while (!isValidInput)
            {
                input = Console.ReadLine();
                isNumeric = int.TryParse(input, out houseNum);

                if (isNumeric && houseNum >= 0 && houseNum < 8)
                    isValidInput = true;
                else
                {
                    Console.WriteLine("Input tidak valid!");
                }
            }

            if (playerInThisTurn == "Player 1")
                return houseNum;
            else
                return houseNum + 8;
        }

        public static void geserPosisiTangan()
        {
            if (tangan.indexPosisi != 15)
                tangan.indexPosisi++;
            else
                tangan.indexPosisi = 0;

            // 2. deposit biji pada store house (lubang besar) milik sendiri bukan milik musuh
            if (papanDakon1.listLubang[tangan.indexPosisi].isStoreHouse && papanDakon1.listLubang[tangan.indexPosisi].owner != tangan.playerInTurn)
                tangan.indexPosisi++;

            if (!papanDakon1.listLubang[tangan.indexPosisi].isBurnt)
            {
                tangan.marblesDiTangan--;

                // 3. jika biji di tangan habis pada lubang yang terdapat biji, maka perputaran dilanjutkan dengan mengambil seluruh biji pada lubang tersebut
                if (tangan.marblesDiTangan == 0 && !papanDakon1.listLubang[tangan.indexPosisi].isEmpty && !papanDakon1.listLubang[tangan.indexPosisi].isStoreHouse)
                {
                    tangan.marblesDiTangan = papanDakon1.listLubang[tangan.indexPosisi].marblesCount + 1;
                    papanDakon1.listLubang[tangan.indexPosisi].marblesCount = 0;
                    papanDakon1.listLubang[tangan.indexPosisi].isEmpty = true;
                }
                // 6. jika biji di tangan habis pada lubang kosong milik sendiri, maka ambil seluruh biji pada lubang milik musuh yang berseberangan
                else if (tangan.marblesDiTangan == 0 && papanDakon1.listLubang[tangan.indexPosisi].isEmpty && !papanDakon1.listLubang[tangan.indexPosisi].isStoreHouse && papanDakon1.listLubang[tangan.indexPosisi].owner == tangan.playerInTurn)
                {
                    papanDakon1.listLubang[tangan.indexPosisi].marblesCount = 1;
                    papanDakon1.listLubang[tangan.indexPosisi].isEmpty = false;

                    if (tangan.playerInTurn == "Player 1")
                    {
                        papanDakon1.listLubang[8].marblesCount += papanDakon1.listLubang[16 - tangan.indexPosisi].marblesCount;
                        papanDakon1.listLubang[16 - tangan.indexPosisi].marblesCount = 0;
                        papanDakon1.listLubang[16 - tangan.indexPosisi].isEmpty = true;
                    }
                    else
                    {
                        papanDakon1.listLubang[0].marblesCount += papanDakon1.listLubang[7 - (tangan.indexPosisi - 9)].marblesCount;
                        papanDakon1.listLubang[7 - (tangan.indexPosisi - 9)].marblesCount = 0;
                        papanDakon1.listLubang[7 - (tangan.indexPosisi - 9)].isEmpty = true;
                    }
                }
                // 1. berjalan secara berputar searah jarum jam
                // 2. deposit biji pada store house (lubang besar) milik sendiri bukan milik musuh
                else
                {
                    papanDakon1.listLubang[tangan.indexPosisi].marblesCount++;
                    papanDakon1.listLubang[tangan.indexPosisi].isEmpty = false;
                }
            }

        }

        public static void switchPlayer()
        {
            if (tangan.playerInTurn == "Player 1")
                tangan.playerInTurn = "Player 2";
            else
                tangan.playerInTurn = "Player 1";
        }
    }
}
