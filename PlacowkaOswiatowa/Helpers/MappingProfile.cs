using AutoMapper;
using PlacowkaOswiatowa.Domain.BusinessLogic;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Models;
using System.Linq;

namespace PlacowkaOswiatowa.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Adres, AdresDto>()
                .ForMember(d => d.Panstwo, o => o.MapFrom(s => s.Panstwo.Nazwa))
                .ForMember(d => d.Miejscowosc, o => o.MapFrom(s => s.Miejscowosc.Nazwa))
                .ForMember(d => d.Ulica, o => o.MapFrom(s => s.Ulica.Nazwa))
                ;

            CreateMap<Pracownik, PracownikDto>()
                .ForMember(d => d.Adres, 
                    o => o.MapFrom(
                        s => s.PracownikPracownicyAdresy.FirstOrDefault().Adres))
                .ForMember(d => d.Stanowisko, o => o.MapFrom(s => s.PracownikUmowa.Stanowisko))
                .ForMember(d => d.Etat, o => o.MapFrom(s => s.PracownikUmowa.Etat))
                .ForMember(d => d.DataRozpoczeciaPracy, o => o.MapFrom(
                    s => s.PracownikUmowa.DataRozpoczeciaPracy))
                .ForMember(d => d.DataZakonczeniaPracy, o => o.MapFrom(
                    s => s.PracownikUmowa.DataZakonczeniaPracy))
                .ForMember(d => d.WynagrodzenieBrutto, o => o.MapFrom(
                    s => s.PracownikUmowa.WynagrodzenieBrutto))
                .ForMember(d => d.WymiarGodzinowy, o => o.MapFrom(
                    s => s.PracownikUmowa.WymiarGodzinowy))
                .ForMember(d => d.DataZawarciaUmowy, o => o.MapFrom(
                    s => s.PracownikUmowa.DataZawarciaUmowy))
                .ForMember(d => d.OkresPracy, o => o.MapFrom(
                    s => s.PracownikUmowa.OkresPracy))
                .ForMember(d => d.CzyZwolnionyOdPodatku, o => o.MapFrom(
                    s => s.PracownikUmowa.CzyZwolnionyOdPodatku))
                ;

            CreateMap<Pracownik, CreatePracownikDto>()
                .ForMember(d => d.Adresy, o => o.MapFrom(
                    s => s.PracownikPracownicyAdresy.Select(pa => pa.Adres)))
                ;

            CreateMap<Uczen, UczenDto>()
                .ForMember(d => d.Wychowawca, o => o.MapFrom(s => s.Oddzial.Pracownik))
                ;

            CreateMap<UczenDto, Uczen>()
                .ForMember(d => d.Adres, o => o.Ignore())
                ;

            CreateMap<Oddzial, OddzialDto>();

            CreateMap<OddzialDto, Oddzial>()
                .ForMember(d => d.Pracownik, o => o.Ignore())
                ;

            CreateMap<Pracodawca, PracodawcaDto>();

            CreateMap<Umowa, UmowaDto>();

            //konfiguracja wynika z wybierania poniższych właściwości
            //z ComboBoxów oraz wymagalności na każdej z nich
            CreateMap<UmowaDto, Umowa>()
                .ForMember(d => d.PracownikId, o => o.MapFrom(s => s.Pracownik.Id))
                .ForMember(d => d.PracodawcaId, o => o.MapFrom(s => s.Pracodawca.Id))
                .ForMember(d => d.StanowiskoId, o => o.MapFrom(s => s.Stanowisko.Id))
                .ForMember(d => d.EtatId, o => o.MapFrom(s => s.Etat.Id))
                .ForMember(d => d.Pracownik, o => o.Ignore())
                .ForMember(d => d.Pracodawca, o => o.Ignore())
                .ForMember(d => d.Stanowisko, o => o.Ignore())
                .ForMember(d => d.Etat, o => o.Ignore())
                ;

            CreateMap<AdresDto, Adres>();

            CreateMap<CreatePracownikDto, Pracownik>();

            CreateMap<string, Panstwo>()
                .ForMember(d => d.Nazwa, o => o.MapFrom(s => s));
            CreateMap<string, Miejscowosc>()
                .ForMember(d => d.Nazwa, o => o.MapFrom(s => s));
            CreateMap<string, Ulica>()
                .ForMember(d => d.Nazwa, o => o.MapFrom(s => s));

            CreateMap<Przedmiot, PrzedmiotDto>();

            CreateMap<Ocena, OcenaDto>();

            CreateMap<Stanowisko, StanowiskoDto>();

            CreateMap<UrlopDto, Urlop>()
                .ForMember(d => d.Pracownik, o => o.Ignore())
                .ForMember(d => d.PracownikId, o => o.MapFrom(s => s.Pracownik.Id))
                ;

            CreateMap<Urlop, UrlopDto>();

            CreateMap<Skladki, SkladkiDto>();

            CreateMap<CreateUzytkownikDto, Uzytkownik>()
                .ForMember(d => d.RolaId, o => o.MapFrom(s => s.Rola.Id))
                .ForMember(d => d.Rola, o => o.Ignore())
                ;
        }
    }
}
