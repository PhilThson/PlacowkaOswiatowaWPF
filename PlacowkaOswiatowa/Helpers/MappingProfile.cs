using AutoMapper;
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
                ;

            CreateMap<Pracownik, CreatePracownikDto>()
                .ForMember(d => d.Adres, o => o.MapFrom(
                        s => s.PracownikPracownicyAdresy.FirstOrDefault().Adres));

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

            CreateMap<UmowaDto, Umowa>();

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
        }
    }
}
