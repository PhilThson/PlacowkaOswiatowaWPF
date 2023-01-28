namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class SkladkiDto
    {
        public decimal SkladkaEmerytalnaProcent { get; set; }
        public decimal SkladkaRentowaProcent { get; set; }
        public decimal SkladkaChorobowaProcent { get; set; }
        public decimal SkladkaZdrowotnaProcent { get; set; }
        public decimal KosztUzyskaniaPrzychodu { get; set; } 
        public decimal PodatekProcent { get; set; }
        public decimal KwotaWolnaOdPodatku { get; set; }
        public decimal ZaliczkaNaPodatekDochodowy { get; set; }
        public decimal WynagrodzenieNetto { get; set; }
        public decimal StawkaNaGodzine { get; set; }
    }
}
