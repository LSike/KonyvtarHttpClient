namespace KonyvtarHttpClient.DTOs
{
    public class KonyvtarakDTO
    {
        public int Id { get; set; }

        public string KonyvtarNev { get; set; } = null!;

        public int Irsz { get; set; }

        public string Cim { get; set; } = null!;

        public string Telepnev { get; set; }

        public string Megyenev { get; set; }
    }
}
