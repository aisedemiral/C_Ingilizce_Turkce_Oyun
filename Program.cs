using System.Data.Common;
using System.IO;
using C_Console_ingilizce;

List<Kelime> dosyadanOku()
{
    string aiseDoc = "/Users/mumineaisedemiral/Documents/sözlük/ingilizce/ingilizce.txt";
    string furkanDoc = "C:\\Users\\Enes\\Documents\\Sozluk\\ingilizce\\ingilizce.txt";
    Console.WriteLine("Oyunu Kim Oynuyor : (Furkan/Aise)");
    string oyuncu = Console.ReadLine();
    string dosya="";
    if (oyuncu == "Halime")
    {
        dosya = furkanDoc;
        
    }
    else
    {
        dosya = aiseDoc;
    }
    
    FileStream fs = new FileStream(dosya, FileMode.Open, FileAccess.Read);
    StreamReader sw = new StreamReader(fs);
    
    string yazi = sw.ReadLine();
    List<Kelime> kelimecevirisi = new List<Kelime>();
    int sayac = 1;
    while (yazi!=null)
    {
        Kelime kelime = new Kelime();
        //Come-gelmek,ulaşmak,görünmek 
        string bosluklariSilinenKelime = yazi.ToString().Trim();
        string[] kelimeler = bosluklariSilinenKelime.Split('-');
        string ingilizcekelime = kelimeler[0];
        ingilizcekelime = ingilizcekelime.TrimStart().TrimEnd().Trim();
        kelime.Ingilizce = ingilizcekelime;
        char karakter = ',';
        bool sonuc;
        sonuc = kelimeler[1].Contains(karakter);
        if (true==sonuc)
        {
            string[] turkcekelimeler = kelimeler[1].TrimStart().TrimEnd().Trim().Split(',');
            foreach (var itemTurkceKelime in turkcekelimeler)
            {
                kelime.Turkce.Add(itemTurkceKelime);
            }
        }
        else
        {
            kelime.Turkce.Add(kelimeler[1]);
        }

        kelime.Id = sayac;
        sayac++;
        kelimecevirisi.Add(kelime);
        yazi = sw.ReadLine();
    }

    return kelimecevirisi;
}

bool SoruDogrumu(Kelime kelime, string cevap)
{
    bool sonuc = false;
    foreach (var itemTurkceKelime in kelime.Turkce)
    {
        if (cevap.Trim() == itemTurkceKelime.Trim())
        {
            sonuc = true;
        }
    }

    return sonuc;
}
Sonuclar sonuclar = new Sonuclar();
List<Kelime> kelimeListesi = dosyadanOku();
Oyun();
void Oyun()
{
    
    Console.WriteLine("Oyun Turunu Sec : Sıralımı, Seçimli mi (sirali/secimli)");
    string oyunTuru = Console.ReadLine();
    if (oyunTuru == "sirali")
    {
        
        Console.WriteLine("verilen ingilizce kelimenin türkçesini veriniz:");
        Console.WriteLine("*********************************************");
        String Klavye = Console.ReadLine();
        int i = 1;
        while (i < 1000)
        {
            Console.WriteLine("Yeni Soru Geliyoooooorrr");
            Console.WriteLine("***********************************");
            Kelime kelime = kelimeListesi.LastOrDefault(x => x.Id == i);
            Console.WriteLine(kelime.Ingilizce, " türkçesini yazınız");
            Klavye = Console.ReadLine();
            kelime.KullaniciCevabi = Klavye;
            bool sonuc = SoruDogrumu(kelime, Klavye);

            if (sonuc == true)
            {
                Console.WriteLine("++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("tebrikler doğru girdiniz");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++"); sonuclar.Dogrular.Add(kelime);
            }
            else
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine("yanlış yaptınız. Tekrar Deneyin !!!");
                Console.WriteLine("------------------------------------");
                Console.WriteLine(kelime.Ingilizce, " türkçesini yazınız");
                string ikinciCevap = Console.ReadLine();
                kelime.KullaniciCevabi = Klavye;
                sonuc = SoruDogrumu(kelime, ikinciCevap);
                if (sonuc == true)
                {
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine("tebrikler doğru girdiniz");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++");
                    sonuclar.Dogrular.Add(kelime);
                }
                else
                {
                    Console.WriteLine("***********************************");
                    Console.WriteLine("Yanlış yaptın lan !!!");
                    Console.WriteLine("***********************************");
                    foreach (var item in kelime.Turkce)
                    {
                        Console.Write(" Doğru Cevap : " + item );

                    }
                    sonuclar.Yanlislar.Add(kelime);
                }

                Console.WriteLine();


            }

            i++;
            if (i % 10 == 0)
            {

                Console.WriteLine("sonuçlarınızı görmek ister misiniz:");
                string cevap2 = Console.ReadLine();
                while (cevap2.ToLower() != "evet" || cevap2.ToLower() != "hayir")
                {

                    if (cevap2.ToLower() == "evet")
                    {
                        int dogruSayisi = sonuclar.DogruListele().Count;
                        int yanlisSayisi = sonuclar.YanlisListele().Count;
                        int cozulenSoruToplami = dogruSayisi + yanlisSayisi;
                        Console.WriteLine("##############################################");
                        Console.WriteLine("Toplam çözülen soru sayısı:" + cozulenSoruToplami);
                        Console.WriteLine("##############################################");
                        
                            foreach (var itemdogrular in sonuclar.DogruListele())
                            {
                                string cevaplar = "";
                                foreach (var itemCevaplar in itemdogrular.Turkce)
                                {
                                    cevaplar += itemCevaplar + " , ";
                                }
                                Console.WriteLine("DOĞRULAR");
                                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++");
                                Console.WriteLine("Id: " + itemdogrular.Id + " soru: " + itemdogrular.Ingilizce +
                                                  " senin cevabın: " + itemdogrular.KullaniciCevabi + " Doğru cevap :" +
                                                  cevaplar);
                                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++");
                            }
                        
                            foreach (var itemyanlislar in sonuclar.YanlisListele())
                            {
                                string cevaplar = "";
                                foreach (var itemCevaplar in itemyanlislar.Turkce)
                                {
                                    cevaplar += itemCevaplar + " , ";
                                }
                            Console.WriteLine("####################################################");
                            Console.WriteLine("YANLIŞLAR");
                            Console.WriteLine("----------------------------------------------------");
                            Console.WriteLine("Id: " + itemyanlislar.Id + " soru: " + itemyanlislar.Ingilizce +
                                                  " senin cevabın: " + itemyanlislar.KullaniciCevabi +
                                                  " Doğru cevap :" + cevaplar);
                            }
                        break;
                        
                    }
                    else if (cevap2.ToLower() == "hayir" || cevap2.ToLower() =="hayır")
                    {
                        Console.WriteLine("Soruları çözmeye devam!!!");
                        break;
                    }
                }

            }

        }

        Console.WriteLine("Listemizdeki bütün kelimeleri gördünüz.");
    }
    
    else if (oyunTuru == "secimli")
    {
        int sayac = 0;
        while (true)
        {
            Console.WriteLine("Yeni Soru Geliyoooooorrr");
            Console.WriteLine("***********************************");
            Console.WriteLine("Soru Numarasi Gir");
            int soruNo =Convert.ToInt32(Console.ReadLine());
            Kelime soru = kelimeListesi.FirstOrDefault(x => x.Id == soruNo);
            Console.WriteLine("Soru " + soruNo + " : " + soru.Ingilizce + " Türkçesini yazın" );
            string Klavye = Console.ReadLine();
            soru.KullaniciCevabi = Klavye;
            bool sonuc = SoruDogrumu(soru, Klavye);

            if (sonuc == true)
            {
                Console.WriteLine("+++++++++++++++++++++++++++++++++");
                Console.WriteLine("tebrikler doğru girdiniz");
                Console.WriteLine("+++++++++++++++++++++++++++++++++");
                sonuclar.Dogrular.Add(soru);
            }
            else
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Yanlış yaptınız. Tekrar Deneyin !!!");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(soru.Ingilizce, " türkçesini yazınız");
                string ikinciCevap = Console.ReadLine();
                soru.KullaniciCevabi = Klavye;
                sonuc = SoruDogrumu(soru, ikinciCevap);
                if (sonuc == true)
                {
                    Console.WriteLine("+++++++++++++++++++++++++++++++++");
                    Console.WriteLine("tebrikler doğru girdiniz");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++"); sonuclar.Dogrular.Add(soru);
                }
                else
                {
                    Console.WriteLine("**********************************");
                    Console.WriteLine("Yanlış yaptın lan");
                    Console.WriteLine("**********************************");
                    foreach (var item in soru.Turkce)
                    {
                        Console.Write(" Doğru Cevap : " + item);

                    }
                    sonuclar.Yanlislar.Add(soru);
                }
                Console.WriteLine();
            }
            sayac = sayac + 1;

            if (sayac % 10 == 0)
            {
                Console.WriteLine("devam etmek ister misiniz.");
                string cevap = Console.ReadLine();
                while (cevap != "evet" || cevap != "hayir")
                {
                    Console.WriteLine("bir seçenek seçin : (evet/hayir)");
                    cevap = Console.ReadLine();
                    if (cevap == "hayir")
                    {
                        break;
                    }
                    else if (cevap == "evet")
                    {
                        Console.WriteLine("istatistikleri görmek ister misin?");
                        string cevap2 = Console.ReadLine();

                        while (cevap2 != "evet" || cevap2 != "hayir")
                        {
                            if (cevap2 == "evet")
                            {
                                int dogruSayisi = sonuclar.DogruListele().Count;
                                int yanlisSayisi = sonuclar.YanlisListele().Count;
                                int cozulenSoruToplami = dogruSayisi + yanlisSayisi;

                                Console.WriteLine("Toplam çözülen soru sayısı:" + cozulenSoruToplami);
                                Console.WriteLine("Doğrularınızı mı görmek istersiniz yanlışlarınızı mı?");
                                string cevap3 = Console.ReadLine();
                                if (cevap3 == "dogru")
                                {
                                    foreach (var itemdogrular in sonuclar.DogruListele())
                                    {
                                        string cevaplar = "";
                                        foreach (var itemCevaplar in itemdogrular.Turkce)
                                        {
                                            cevaplar += itemCevaplar + " , ";
                                        }

                                        Console.WriteLine("Id: " + itemdogrular.Id + " soru: " +
                                                          itemdogrular.Ingilizce +
                                                          " senin cevabın: " + itemdogrular.KullaniciCevabi +
                                                          " Doğru cevap :" +
                                                          cevaplar);
                                    }
                                }
                                else if (cevap3 == "yanlis")
                                {
                                    foreach (var itemyanlislar in sonuclar.YanlisListele())
                                    {
                                        string cevaplar = "";
                                        foreach (var itemCevaplar in itemyanlislar.Turkce)
                                        {
                                            cevaplar += itemCevaplar + " , ";
                                        }

                                        Console.WriteLine("Id: " + itemyanlislar.Id + " soru: " +
                                                          itemyanlislar.Ingilizce +
                                                          " senin cevabın: " + itemyanlislar.KullaniciCevabi +
                                                          " Doğru cevap :" + cevaplar);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Devam Ediyorsun...");
                            }
                        }
                    }
                }
            }

        }
    }
}