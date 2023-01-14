namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class OddzialDto
    {
        public byte Id { get; set; }
        public string Nazwa { get; set; }
        public PracownikDto Pracownik { get; set; }
    }
}
