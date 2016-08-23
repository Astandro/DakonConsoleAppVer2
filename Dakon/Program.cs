using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dakon
{
    class Program
    {
        //Aplikasi Console Dakon - Bukalapak Test

        //Referensi : https://www.youtube.com/watch?v=zAGYhT05AIc 
        //Referensi 2 : http://www.expat.or.id/info/congklakinstructions.html

        //Kondisi-kondisi permainan dakon : Ronde 1

        //1. berjalan secara berputar searah jarum jam
        //2. deposit biji pada store house (lubang besar) milik sendiri bukan milik musuh
        //3. jika biji di tangan habis pada lubang yang terdapat biji, maka perputaran dilanjutkan dengan mengambil seluruh biji pada lubang tersebut
        //4. jika biji di tangan habis pada store house (lubang besar) milik sendiri, maka player tersebut mendapat jatah giliran lagi
        //5. jika biji di tangan habis pada lubang kosong milik musuh, maka giliran pemain tersebut berakhir
        //6. jika biji di tangan habis pada lubang kosong milik sendiri, maka ambil seluruh biji pada lubang tersebut ditambah milik musuh yang berseberangan
        //7. Permainan berakhir jika salah satu pemain kehabisan biji pada seluruh lubang miliknya (kalah jalan)

        //Ronde 2

        //1. sama seperti ronde 1, hanya saja sebagai permulaan, seluruh isi storehouse didistribusikan ke masing2 lubang, jika tidak cukup untuk memenuhi 1 lubang, maka sisanya dibagikan secara merata pada lubang sisanya.
        //2. jika ada lubang yang tidak terisi penuh (7 biji), maka lubang tersebut dinyatakan burnt (tidak bisa dilewati musuh, biji di dalamnya tidak boleh diambil, tidak bisa ditembak)


        static PapanDakon papanDakon1;
        static PointerTangan tangan;
        static Boolean isSecondRound;

        static void Main(string[] args)
        {
            papanDakon1 = PapanDakon.GetInstance();
            tangan = new PointerTangan();
            isSecondRound = false;

            //Round 1
            //Print Kondisi awal Papan Dakon pada Round 1 
            papanDakon1.printPapanDakon();

            //Permainan dimulai oleh Player 1
            tangan.playerInTurn = "Player 1";

            //Looping giliran pada Ronde 1
            while(!isP1KalahJalan() && !isP2KalahJalan())
            {
                //Get user input
                pilihDanAmbilIsiLubang();

                //Geser posisi tangan selama permainan searah jarum jam
                while (tangan.marblesDiTangan != 0)
                {
                    geserPosisiTangan();
                }

                //jika tidak ada player yang kalah jalan
                if (!isP1KalahJalan() && !isP2KalahJalan())
                {
                    //jika posisi berhenti bukan di storehouse, maka giliran pemain tersebut berakhir
                    if (!papanDakon1.listLubang[tangan.indexPosisi].isStoreHouse)
                        switchPlayer();
                    else
                        printGetAdditionlRoundMessage();  //mendapat jatah 1 giliran lagi
                }
                else
                    printKalahJalanMessage();

                papanDakon1.printPapanDakon();
            }

            //Ambil sisa biji milik pemenang & masukkan ke storehousenya
            if (isP1KalahJalan())
                papanDakon1.listLubang[0].marblesCount += getSisaBijiP2();
            else if (isP2KalahJalan())
                papanDakon1.listLubang[8].marblesCount += getSisaBijiP1();
            else//Inisialisasi Player yang berhak memulai duluan di ronde ke 2
                setSecondRoundFirstPlayer();

            //Inisialisasi ronde ke 2
            isSecondRound = papanDakon1.startSecondRound();

            if (!isSecondRound)
                Console.WriteLine("Ronde 2 tidak dapat dilaksanakan karena salah satu player memiliki jumlah biji terlalu sedikit");

            //Print kondisi awal papan dakon pada ronde ke 2
            papanDakon1.printPapanDakon();

            //Looping giliran pada Ronde 2
            while (!isP1KalahJalan() && !isP2KalahJalan() && isSecondRound)
            {
                //Get user input
                pilihDanAmbilIsiLubang();

                //Geser posisi tangan selama permainan searah jarum jam
                while (tangan.marblesDiTangan != 0)
                {
                    geserPosisiTangan();
                }

                //jika tidak ada player yang kalah jalan
                if (!isP1KalahJalan() && !isP2KalahJalan())
                {
                    //jika posisi berhenti bukan di storehouse, maka giliran pemain tersebut berakhir
                    if (!papanDakon1.listLubang[tangan.indexPosisi].isStoreHouse)
                        switchPlayer();
                    else
                        printGetAdditionlRoundMessage();  //mendapat jatah 1 giliran lagi
                }
                else
                    printKalahJalanMessage();

                papanDakon1.printPapanDakon();
            }

            //Hitung semua sisa biji di papan Dakon
            isSecondRound = false;
            papanDakon1.listLubang[0].marblesCount += getSisaBijiP2();
            papanDakon1.listLubang[8].marblesCount += getSisaBijiP1();
            showFinalMessage();

            Console.ReadLine();
        }

        public static void showFinalMessage()
        {
            Console.WriteLine("Permainan telah berakhir\nSkor akhir P1 : " + papanDakon1.listLubang[8].marblesCount + " P2 : " + papanDakon1.listLubang[0].marblesCount);
            String winner = "";

            if (papanDakon1.listLubang[8].marblesCount > papanDakon1.listLubang[0].marblesCount)
                winner = "Player 1";
            else
                winner = "Player 2";

            Console.WriteLine("Permainan dimenangkan oleh "+winner);
        }

        public static void printKalahJalanMessage()
        {
            if(isP1KalahJalan())
                Console.WriteLine("Player 1 mengalami kalah jalan, Player 2 menang jalan");
            else
                Console.WriteLine("Player 2 mengalami kalah jalan, Player 1 menang jalan");
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
                Console.Write("\nGiliran " + tangan.playerInTurn + ", Silahkan pilih posisi (1-7) : ");
                input = Console.ReadLine();
                isNumeric = int.TryParse(input, out houseNum);

                if (isNumeric && houseNum >= 0 && houseNum < 8)
                {
                    if (playerInThisTurn == "Player 2")
                        houseNum += 8;

                    if (papanDakon1.listLubang[houseNum].isEmpty)
                    {
                        Console.WriteLine("Lubang yang anda pilih kosong, silahkan pilih yang lain");
                    }
                    else
                    {
                        if (papanDakon1.listLubang[houseNum].isBurnt)
                        {
                            Console.WriteLine("Lubang yang anda pilih adalah lubang ngacang, silahkan pilih yang lain");
                        }
                        else
                            isValidInput = true;
                    }
                    
                }
                else
                {
                    Console.WriteLine("Input tidak valid!");
                }
            }

            return houseNum;
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

            if (!papanDakon1.listLubang[tangan.indexPosisi].isBurnt || (papanDakon1.listLubang[tangan.indexPosisi].isBurnt && papanDakon1.listLubang[tangan.indexPosisi].owner==tangan.playerInTurn))
            {
                tangan.marblesDiTangan--;

                // 3. jika biji di tangan habis pada lubang yang terdapat biji, maka perputaran dilanjutkan dengan mengambil seluruh biji pada lubang tersebut
                if (tangan.marblesDiTangan == 0 && !papanDakon1.listLubang[tangan.indexPosisi].isEmpty && !papanDakon1.listLubang[tangan.indexPosisi].isStoreHouse && !papanDakon1.listLubang[tangan.indexPosisi].isBurnt)
                {
                    tangan.marblesDiTangan = papanDakon1.listLubang[tangan.indexPosisi].marblesCount + 1;
                    papanDakon1.listLubang[tangan.indexPosisi].marblesCount = 0;
                    papanDakon1.listLubang[tangan.indexPosisi].isEmpty = true;
                }
                // 6. jika biji di tangan habis pada lubang kosong milik sendiri, maka ambil seluruh biji pada lubang milik musuh yang berseberangan
                else if (tangan.marblesDiTangan == 0 && papanDakon1.listLubang[tangan.indexPosisi].isEmpty && !papanDakon1.listLubang[tangan.indexPosisi].isStoreHouse && papanDakon1.listLubang[tangan.indexPosisi].owner == tangan.playerInTurn && !papanDakon1.listLubang[tangan.indexPosisi].isBurnt)
                {
                    if (tangan.playerInTurn == "Player 1" && !papanDakon1.listLubang[16 - tangan.indexPosisi].isBurnt && !papanDakon1.listLubang[16 - tangan.indexPosisi].isEmpty)
                    {
                        papanDakon1.listLubang[tangan.indexPosisi].marblesCount = 0;
                        papanDakon1.listLubang[tangan.indexPosisi].isEmpty = true;
                        papanDakon1.listLubang[8].marblesCount += papanDakon1.listLubang[16 - tangan.indexPosisi].marblesCount + 1;
                        papanDakon1.listLubang[16 - tangan.indexPosisi].marblesCount = 0;
                        papanDakon1.listLubang[16 - tangan.indexPosisi].isEmpty = true;
                    }
                    else if (tangan.playerInTurn == "Player 2" && !papanDakon1.listLubang[7 - (tangan.indexPosisi - 9)].isBurnt && !papanDakon1.listLubang[7 - (tangan.indexPosisi - 9)].isEmpty)
                    {
                        papanDakon1.listLubang[tangan.indexPosisi].marblesCount = 0;
                        papanDakon1.listLubang[tangan.indexPosisi].isEmpty = true;
                        papanDakon1.listLubang[0].marblesCount += papanDakon1.listLubang[7 - (tangan.indexPosisi - 9)].marblesCount + 1;
                        papanDakon1.listLubang[7 - (tangan.indexPosisi - 9)].marblesCount = 0;
                        papanDakon1.listLubang[7 - (tangan.indexPosisi - 9)].isEmpty = true;
                    }
                    else
                    {
                        papanDakon1.listLubang[tangan.indexPosisi].marblesCount++;
                        papanDakon1.listLubang[tangan.indexPosisi].isEmpty = false;
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

        public static int getSisaBijiP1()
        {
            int jumlah = 0;
            if (!isSecondRound)
            {                
                for (int i = 1; i < 8; i++)
                    jumlah += papanDakon1.listLubang[i].marblesCount;             
            }
            else
            {
                for (int i = papanDakon1.p1HouseBurnt + 1; i < 8; i++)
                    jumlah += papanDakon1.listLubang[i].marblesCount; 
            }

            return jumlah;
        }

        public static int getSisaBijiP2()
        {
            int jumlah = 0;
            if (!isSecondRound)
            {
                for (int i = 9; i < 16; i++)
                    jumlah += papanDakon1.listLubang[i].marblesCount;
            }
            else
            {
                for (int i = papanDakon1.p2HouseBurnt + 9; i < 16; i++)
                    jumlah += papanDakon1.listLubang[i].marblesCount;
            }

            return jumlah;
        }

        public static bool isP1KalahJalan()
        {
            if (getSisaBijiP1() == 0)
                return true;
            else
                return false;
        }

        public static bool isP2KalahJalan()
        {
            if (getSisaBijiP2() == 0)
                return true;
            else
                return false;
        }
    }
}
