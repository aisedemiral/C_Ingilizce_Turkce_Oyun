using System.Security.AccessControl;

namespace C_Console_ingilizce;

public class Kelime
{
    public int Id { get; set; }
    public List<string> Turkce { get; set; }
    public string Ingilizce { get; set; }
    public string KullaniciCevabi { get; set; }
    
    public Kelime()
    {
        Turkce = new List<string>();
    }
    
}
