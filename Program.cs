using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // file io islemi icin 
using System.Collections; // arraylist icin

namespace Proje2_2      // 05170000751 Alp Furkan Urkmez proje 1 2. soru
{
    class Program
    {
        static void Main(string[] args)
        {
            // a, b, c sikki baslangici

            // stream olusturma - veri ve test edilecek .txt dosyalari
            FileStream test_s = new FileStream("test.txt", FileMode.Open, FileAccess.Read); // (18-20) dosyalar bin/debug icinde.
            StreamReader tr = new StreamReader(test_s);
            FileStream veri_s = new FileStream("veri.txt", FileMode.Open, FileAccess.Read);
            StreamReader vr = new StreamReader(veri_s);

            // (22-23) sayiyi bilmeseydik while (tr.ReadLine() != null) ile sayardik.
            string[] test = new string[30];     // test verilerini tutan array
            string[] veri = new string[120];    // karsilastirma verilerini tutan array
            double[] sonuc = new double[120];   // formul sonuclarinin tutulacagi array;
            // 28-42 dosyalarin okunmasi
            int Index = 0;
            string satir = "a";
            while(satir != null && Index < test.Length)
            {
                test[Index] = tr.ReadLine();
                satir = test[Index++] ;
            }

            Index = 0;
            satir = "a";
            while(satir != null && Index < veri.Length)
            {
                veri[Index] = vr.ReadLine();
                satir = veri[Index++];
            }
            
            Console.WriteLine("komsu sayisi giriniz: ");
            int komsuSayisi = Convert.ToInt32(Console.ReadLine());
            int[] indeksler = new int[komsuSayisi]; // en kucuk uzakliga sahip turlerin veri icindeki indexini tutar
            double dogruBulunanlar = 0; // dogru tespit edilen tur sayisini tutar

            for (int i = 0; i<test.Length; i++) 
            {
                string lineTest = test[i];
                lineTest = lineTest.Replace(',', '+');  // double.Parse virgule gore calisiyor. bizim verilerde de double sayilar nokta ile ayrilmis o yuzden replace methodu kullandim. 
                lineTest = lineTest.Replace('.', ',');
                string[] lineTestSp = lineTest.Split('+'); // split edilmis halini icerir.
                string testTuru = lineTestSp[lineTestSp.Length - 1];
                string veriTuru = "";

                for(int j = 0; j<veri.Length; j++)    
                {
                    double sum = 0;
                    string lineVeri = veri[j];
                    lineVeri = lineVeri.Replace(',', '+');
                    lineVeri = lineVeri.Replace('.', ',');
                    string[] lineVeriSp = lineVeri.Split('+');

                    for(int k = 0; k<4; k++)
                    {
                        double param1 = double.Parse(lineTestSp[k]); // 4 adet ozelligi sirasiyla alır. (68-69)
                        double param2 = double.Parse(lineVeriSp[k]);
                        sum += (param1 - param2) * (param1 - param2);
                    }
                    sonuc[j] = (Math.Sqrt(sum));
                }

                for(int j = 0; j<komsuSayisi; j++)
                {
                    double min = 10000;
                    int k = 0, minIndeks = 0;
                    for (k = 0; k < sonuc.Length; k++)
                    {
                        if (min > sonuc[k])
                        {
                            min = sonuc[k];
                            minIndeks = k;
                        }
                    }
                    indeksler[j] = minIndeks;
                    sonuc[minIndeks] = 100000;
                }

                List<int> turler = new List<int>() { 0, 0, 0 }; // 0: setosa 1: versicolor 2: virginica
                int max = 0;
                for (int n = 0; n<indeksler.Length; n++)
                {
                    string lineVeri = veri[indeksler[n]];
                    lineVeri = lineVeri.Replace(',', '+');
                    lineVeri = lineVeri.Replace('.', ',');
                    string[] lineVeriSp = lineVeri.Split('+');
                    veriTuru = lineVeriSp[lineVeriSp.Length - 1];

                    if (veriTuru.Equals("Iris-setosa"))
                        turler[0]++;
                    else if (veriTuru.Equals("Iris-versicolor"))
                        turler[1]++;
                    else
                        turler[2]++;

                    max = turler.IndexOf(turler.Max());
                }

                string bulunanTur = "";
                if (max == 0)
                    bulunanTur = "Iris-setosa";
                else if (max == 1)
                    bulunanTur = "Iris-versicolor";
                else
                    bulunanTur = "Iris-virginica";

                if (testTuru.Equals(bulunanTur))
                    dogruBulunanlar++;
            }
            
            Console.Write("basari orani: " + (dogruBulunanlar / (double)test.Length) +"\n\n");  // komsuSayisi 40 ustu olursa 1 oran dusmeye baslar

            // a, b, c sikki bitisi

            // d sikki baslangic
            //Console.Write("tum verileri silmek istiyor musunuz (Y/N): \n\n");
            //char answer = Console.ReadKey().KeyChar;
            //if(answer == 'Y')
            //    File.WriteAllText(@"C: \Users\urkmez\Desktop\DataStructures_Homeworks\veri.txt", "");
            //Console.ReadKey();
            //Console.Write("bir satiri silmek istiyor musunuz (Y/N): \n\n");
            //answer = Console.ReadKey().KeyChar;
            //if(answer == 'Y')
            //{
            //    StreamWriter writer = new StreamWriter(@"C: \Users\urkmez\Desktop\DataStructures_Homeworks\veri.txt");
            //    Console.Write("silmek istediginiz satiri giriniz :");
            //    int line = Convert.ToInt32(Console.ReadLine());
            //    writer.WriteLine(line);
            //    writer.Close();
            //}
            //Console.ReadKey();
            //Console.Write("bir cicek eklemek istiyor musunuz (Y/N): ");
            //answer = Console.ReadKey().KeyChar;
            //if(answer == 'Y')
            //{
            //    Console.Write("eklemek istediginiz cicegin ozelliklerini giriniz: (3.5, 3.5, 3.5, 3.5 , \"turu\" ");
            //    string cicek = Console.ReadLine();
            //    TextWriter writer = new StreamWriter(@"C: \Users\urkmez\Desktop\DataStructures_Homeworks\veri.txt");
            //    writer.WriteLine();
            //    writer.Close();
            //}
            // d sikki bitisi

            // e sikki baslangic
            //Console.WriteLine("\n\nveri setindeki veriler: \n");
            //foreach (string s in veri)
            //    Console.WriteLine(s);
            // e sikki bitis

            tr.Close();
            vr.Close();
            Console.ReadKey();
        }
    }
}