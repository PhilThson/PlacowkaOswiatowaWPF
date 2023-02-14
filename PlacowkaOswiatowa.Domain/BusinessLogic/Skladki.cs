using PlacowkaOswiatowa.Domain.DTOs;
using System;

namespace PlacowkaOswiatowa.Domain.BusinessLogic
{
    public class Skladki
    {
        #region Właściwości

        public PracownikDto Pracownik { get; set; }

        #region Składki społeczne
        public decimal SkladkaEmerytalnaProcent { get; set; }
        public decimal SkladkaRentowaProcent { get; set; }
        public decimal SkladkaChorobowaProcent { get; set; }
        #endregion

        //obliczna z pensji brutto po odjeciu skladek społecznych
        public decimal SkladkaZdrowotnaProcent { get; set; }
        public decimal KosztUzyskaniaPrzychodu { get; set; } //PLN
        //kwota pozostała po odjęciu powyższych w I progu podatkowym to 12%
        public decimal PodatekProcent { get; set; }
        //właściwie 1/12 kwoty wolnej od podatku czyli 300 PLN (ew. 450)
        //jeżeli praconwik złożył PIT-2
        public decimal KwotaWolnaOdPodatku { get; set; }
        //Podatek pomniejszony o kwote wolna od podatku
        public decimal ZaliczkaNaPodatekDochodowy { get; private set; }

        #region Obliczone wartości ubezpieczeń społecznych
        public decimal SkladkaEmerytalna{ get; private set; }
        public decimal SkladkaRentowa { get; private set; }
        public decimal SkladkaChorobowa { get; private set; }
        public decimal SkladkaZdrowotna { get; private set; }
        public decimal Podatek { get; private set; }
        #endregion

        #region Obliczone wartości biznesowe
        public decimal WynagrodzenieNetto { get; private set; }
        public decimal StawkaNaGodzine { get; private set; }
        #endregion

        #endregion

        #region Konstruktor
        public Skladki()
        {
            SkladkaEmerytalnaProcent = 0.0976m;
            SkladkaRentowaProcent = 0.015m;
            SkladkaChorobowaProcent = 0.0245m;
            SkladkaZdrowotnaProcent = 0.09m;
            KosztUzyskaniaPrzychodu = 250.0m;
            PodatekProcent = 0.12m;
            KwotaWolnaOdPodatku = 300.0m;
        }
        #endregion

        #region Metody

        public void ObliczNetto()
        {
            if (Pracownik == null ||
                string.IsNullOrEmpty(Pracownik.WynagrodzenieBrutto.ToString()))
                return;

            //1) obliczenie składek społecznych + zdrowotnej:
            var kwotaBrutto = Pracownik.WynagrodzenieBrutto;
            SkladkaEmerytalna = kwotaBrutto * SkladkaEmerytalnaProcent;
            SkladkaRentowa = kwotaBrutto * SkladkaRentowaProcent;
            SkladkaChorobowa = kwotaBrutto * SkladkaChorobowaProcent;
            var skladkiSpoleczne = SkladkaEmerytalna + SkladkaRentowa + SkladkaChorobowa;
            //Kwota z której oblicza się skladke zdrowotna, to pensja brutto pomniejszona
            //o skladki spoleczne
            SkladkaZdrowotna = Math.Round((kwotaBrutto - skladkiSpoleczne) * SkladkaZdrowotnaProcent, 2);
            
            //2) obliczenie zaliczki na podatek dochodowy:
            // tj. kwotaBrutto - skladki - koszty uzyskania przychodu
            // Zwykłe koszty uzyskania przychodu to: 250zl, podwyzszone: 300zl
            // - wynagrodzenie pomniejszone o składki społeczne
            var kwotaBezSkladek = kwotaBrutto - skladkiSpoleczne - SkladkaZdrowotna;
            // - wyliczenie podstawy do opodatkowania (zaokrąglenie do pełnych złotych)
            var kwotaDoOpodatkowania = Math.Round(kwotaBezSkladek - KosztUzyskaniaPrzychodu);

            //2.1) obliczenie podatku:
            // tj. wyliczona podstawa do opodatkowania pomnożona razy 12%
            // 12%: dla podatkników których dochody <= 120 000.00 zł
            // 32%: dochody > 120 000.00 zł (od części która przekroczyła I próg)
            Podatek = Math.Round(kwotaDoOpodatkowania * PodatekProcent, 2);

            //2.2) finalne obliczenie zaliczki na podatek dochodowy (zaokrąglenie do pełnych złotych)
            //Kwotę wolną odejmuje się w przypadku gry pracownik złożył PIT-2 (jak nie to się nie odlicza)
            var zaliczkaNaPodatekDochodowy = 0.0m;
            //jeżeli pracownik zwolniony od podatku (np. "zwolnienie z PIT dla młodych")
            //to nie oblicza się zaliczki na podatek dochodowy
            if(!Pracownik.CzyZwolnionyOdPodatku)
                zaliczkaNaPodatekDochodowy = Math.Round(Podatek - KwotaWolnaOdPodatku);

            WynagrodzenieNetto = Math.Round(kwotaBrutto - skladkiSpoleczne - SkladkaZdrowotna - zaliczkaNaPodatekDochodowy, 2);
        }

        public void ObliczStawkeNaGodzine()
        {
            if (!Pracownik.WymiarGodzinowy.HasValue)
                return;
            //założenie: WymiarGodzinowy - liczba godzin przepracowana tygodniowo
            double godzinyPrzepracowane = Pracownik.WymiarGodzinowy.Value * 4.0d + Pracownik.Nadgodziny.GetValueOrDefault();
            if (godzinyPrzepracowane == 0.0) 
                return;
            if (!decimal.TryParse(godzinyPrzepracowane.ToString(), out decimal liczbaGodzin))
                return;

            StawkaNaGodzine = Math.Round(WynagrodzenieNetto / liczbaGodzin, 2);
        }

        #endregion

        //źródło:
        //https://poradnikprzedsiebiorcy.pl/kalkulator-wynagrodzen
    }
}
