using System.Text;

namespace PlacowkaOswiatowa.Domain.DTOs
{
    public class AdresDto
    {
        public int Id { get; set; }
        public string Panstwo { get; set; }
        public string Miejscowosc { get; set; }
        public string Ulica { get; set; }
        public string NumerDomu { get; set; }
        public string NumerMieszkania { get; set; }
        public string KodPocztowy { get; set; }

        public override string ToString()
        {
            //wykorzystanie StringBuildera, ponieważ jest wydajniejszy
            //niż zwykła konkatenacja stringów
            var addressDescription = new StringBuilder(
                $"{Panstwo}, ul. {Ulica} {NumerDomu}"
                );
            addressDescription.Append(
                string.IsNullOrEmpty(NumerMieszkania) ? "" : $"/{NumerMieszkania}"
            );
            addressDescription.Append($", {KodPocztowy} {Miejscowosc}");
            return addressDescription.ToString();
        }
    }
}
