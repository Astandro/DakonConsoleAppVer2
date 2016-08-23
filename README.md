# DakonConsoleApp
Aplikasi Console Dakon - Bukalapak Test

Referensi : https://www.youtube.com/watch?v=zAGYhT05AIc
Referensi 2 : http://www.expat.or.id/info/congklakinstructions.html

Kondisi-kondisi permainan dakon :
Ronde 1
 1. berjalan secara berputar searah jarum jam
 2. deposit biji pada store house (lubang besar) milik sendiri bukan milik musuh
 3. jika biji di tangan habis pada lubang yang terdapat biji, maka perputaran dilanjutkan dengan mengambil seluruh biji pada lubang tersebut
 4. jika biji di tangan habis pada store house (lubang besar) milik sendiri, maka player tersebut mendapat jatah giliran lagi
 5. jika biji di tangan habis pada lubang kosong milik musuh, maka giliran pemain tersebut berakhir
 6. jika biji di tangan habis pada lubang kosong milik sendiri, maka ambil seluruh biji pada lubang tersebut ditambah milik musuh yang berseberangan
 7. Permainan berakhir jika salah satu pemain kehabisan biji pada seluruh lubang miliknya (kalah jalan)
 
Ronde 2
 1. sama seperti ronde 1, hanya saja sebagai permulaan, seluruh isi storehouse didistribusikan ke masing2 lubang, 
    jika tidak cukup untuk memenuhi 1 lubang, maka sisanya dibagikan secara merata pada lubang sisanya.
 2. jika ada lubang yang tidak terisi penuh (7 biji), maka lubang tersebut dinyatakan burnt (tidak bisa dilewati musuh, biji di dalamnya tidak boleh diambil, tidak bisa ditembak)

Testcase :
1
2
1
1
5
5
6
7
6
5
1
7
7
6
7
5
7 
4
6 -> P2 stop di storehouse
3 -> tembak
7
6
6
7
3 -> tembak
5
5
6 -> P1 stop di storehouse
4 -> P1 stop di lubang kosong milik musuh
1
1
3
6
7 -> P2 stop di lubang kosong milik musuh
7
1
1 
2
3
5
2
3 -> tembak
7
6
7 -> P1 Kalah jalan, P1 Menang Biji, skor P1=55 P2=43

Ronde 2
2 -> melompati burnt area
5
4 -> tembak
2
1
1
4
2
7
6 -> stop di storehouse
2
1
6
7
2
2
5
2
1
6
1
7 -> P2 Kalah Jalan, P2 Kalah Biji, Skor P1=71 P2=27