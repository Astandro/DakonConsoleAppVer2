# DakonConsoleApp
Aplikasi Console Dakon - Bukalapak Test

Referensi : https://www.youtube.com/watch?v=zAGYhT05AIc
Game contoh : http://dakon-the-game.appspot.com/

Kondisi-kondisi permainan dakon :
Ronde 1
 1. berjalan secara berputar searah jarum jam
 2. deposit biji pada store house (lubang besar) milik sendiri bukan milik musuh
 3. jika biji di tangan habis pada lubang yang terdapat biji, maka perputaran dilanjutkan dengan mengambil seluruh biji pada lubang tersebut
 4. jika biji di tangan habis pada store house (lubang besar) milik sendiri, maka player tersebut mendapat jatah giliran lagi
 5. jika biji di tangan habis pada lubang kosong milik musuh, maka giliran pemain tersebut berakhir
 6. jika biji di tangan habis pada lubang kosong milik sendiri, maka ambil seluruh biji pada lubang milik musuh yang berseberangan
 
Ronde 2
 1. sama seperti ronde 1, hanya saja sebagai permulaan, seluruh isi storehouse didistribusikan ke masing2 lubang, 
    jika tidak cukup untuk memenuhi 1 lubang, maka sisanya dibiarkan di storehouse
 2. jika ada lubang yang kosong/tidak terisi, maka lubang tersebut dinyatakan burnt/tidak bisa digunakan

Testcase :

1. P1 Wins 62-36 :
5 5 5 6 4 1 1 6 4 7 3 2 7 7 1 2 6 3 3 2 1 4 4 1 7 6 7 2 5 1 5 3 7 2 6 7 1 3 5 5 7 X 6 X 7

2. P2 Wins 52-46 :
3 1 1 6 7 3 4 6 1 2 4 7 5 4 4 1 6 5 7 7 1 7 4 5 7 2 7 6 7 3 3 2 6 3 7 X 5 7

*Note X pada input artinya pilih angka terserah antara 1-7 karena pada giliran tersebut semua lubang milik player telah kosong
