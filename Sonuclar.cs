namespace C_Console_ingilizce;

public class Sonuclar
{
    public List<Kelime> Dogrular { get; set; }
    public List<Kelime> Yanlislar { get; set; }

    public Sonuclar()
    {
        Dogrular = new List<Kelime>();
        Yanlislar = new List<Kelime>();
    }
    public void YanlisEkle(Kelime kelime)
    {
        Yanlislar.Add(kelime);
    }

    public void DogruEkle(Kelime kelime)
    {
        Dogrular.Add(kelime);
    }

    public List<Kelime> DogruListele()
    {
        return Dogrular;
    }

    public List<Kelime> YanlisListele()
    {
        return Yanlislar;
    }
}