using System;

class DijkstraAlgoritmasi
{
    public static void Main()
    {
        int[,] grafik = new int[,] {
            { 0, 4, 0, 2, 0, 0 },
            { 4, 0, 8, 0, 1, 3 },
            { 0, 8, 0, 0, 2, 2 },
            { 2, 0, 0, 0, 5, 0 },
            { 0, 1, 2, 5, 0, 3 },
            { 0, 3, 2, 0, 3, 0 }
        };

        int kaynak = 0;
        Dijkstra(grafik, kaynak);
    }

    static void Dijkstra(int[,] grafik, int kaynak)
    {
        int dugumSayisi = grafik.GetLength(0);// düğüm sayısını aldık
        int[] mesafeler = new int[dugumSayisi];//düğüm sayısı kadar elemanlı mesafeler dizisi oluşturduk
        bool[] kisaYolSeti = new bool[dugumSayisi];// aynı şekilde ziyaret edilen düğümleri belirlemek için düğüm sayısı kadar elemanlı kısayol seti oluşturduk

        for (int i = 0; i < dugumSayisi; i++)
        {
            mesafeler[i] = int.MaxValue;//başlangıç mesafesi hariç max değer yapmalıyız
            kisaYolSeti[i] = false;//bu düğümler ziyaret edilmemiş olduğu için false oluyor
        }

        mesafeler[kaynak] = 0;// başlangıç düğümünün mesafesini 0 yapıyoruz ki ordan başlıyalım

        for (int sayac = 0; sayac < dugumSayisi - 1; sayac++)
        {
            int enKisaMesafeDugumu = EnKisaMesafe(mesafeler, kisaYolSeti, dugumSayisi);// en kısa mesafe metodunu çağırarak en kısa mesafeye sahip olan düğümün indisini alıyoruz
            kisaYolSeti[enKisaMesafeDugumu] = true;//aldığımız düğümü ziyaret edilmiş olarak işaretliyoruz

            for (int komsu = 0; komsu < dugumSayisi; komsu++)
                if (!kisaYolSeti[komsu]/*komşuya gidilmediyse*/ && grafik[enKisaMesafeDugumu, komsu] != 0/*bu yolun 0 değilse*/ && mesafeler[enKisaMesafeDugumu] != int.MaxValue/*bu yolun sonsuz değilse*/ && mesafeler[enKisaMesafeDugumu] + grafik[enKisaMesafeDugumu, komsu] < mesafeler[komsu]/*bu yolun diğer yollardan kısaysa*/)
                    mesafeler[komsu] = mesafeler[enKisaMesafeDugumu] + grafik[enKisaMesafeDugumu, komsu];//yeni en kısa yol bu diyip bunu mesafeler dizisine atıyoruz
        }

        CozumuYazdir(mesafeler, dugumSayisi);
    }

    static int EnKisaMesafe(int[] mesafeler, bool[] kisaYolSeti, int dugumSayisi)
    {
        int min = int.MaxValue, minIndeks = -1;// min değerini maxvalue yapıyoruz ki ilk karşılaştırmada onun yerini alabilsin

        for (int dugum = 0; dugum < dugumSayisi; dugum++)// her düğümü gezebilsin diye for döngüsü kurduk
            if (!kisaYolSeti[dugum] && mesafeler[dugum] <= min)// kısayolseti false olsun ki ziyaret edilmemiş düğümlere bakalım ve mesafe değeri minden küçük olsun ki minimum değeri alalım
            {
                min = mesafeler[dugum];//minimum değeri mesafelerdeki düğüm indeksinin değerine atıyoruz 
                minIndeks = dugum;//minindeksi duğum değerine atıyoruz çünkü yukarıda bu indeksi ziyaret edilmiş nodeları ayırt edebilmek için kullanıcaz
            }

        return minIndeks;
    }

    static void CozumuYazdir(int[] mesafeler, int dugumSayisi)
    {
        Console.WriteLine("Düğüm \t Mesafe (Kaynak Düğümden)");
        for (int i = 0; i < dugumSayisi; i++)
            Console.WriteLine($"{i} \t\t {mesafeler[i]}");
    }
}
