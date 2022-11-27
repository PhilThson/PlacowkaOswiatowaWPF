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
            CreateMap<Oddzial, OddzialDto>();

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

            CreateMap<Uczen, UczenDto>();

            CreateMap<Pracodawca, PracodawcaDto>();

            CreateMap<UmowaDto, Umowa>();

            //CreateMap<PracownikDTO, PracownikDto>()
            //    .ForMember(p => p.PracownikId, src => src.MapFrom(s => s.Id))
            //    .ForMember(p => p.DataUrodzenia, 
            //        src => src.MapFrom(s => s.DataUrodzenia.Value.ToShortDateString()))
            //    .ForMember(p => p.PensjaBrutto, src => src.MapFrom(s => s.Pensja))
            //    .ForMember(p => p.WymiarGodz, src => src.MapFrom(s => s.WymiarGodzinowy));
            
            //CreateMap<Osoba, OsobaDTO>();
            
            //CreateMap<UczenDTO, UczenDto>()
            //    .ForMember(u => u.UczenId, s => s.MapFrom(s => s.Id))
            //    .ForMember(u => u.Imie, s => s.MapFrom(s => s.Imie))
            //    .ForMember(u => u.Nazwisko, s => s.MapFrom(s => s.Nazwisko))
            //    .ForMember(u => u.DataUrodzenia,
            //        s => s.MapFrom(s => s.DataUrodzenia.Value.ToShortDateString()))
            //    .ForMember(u => u.NazwaGrupy, s => s.MapFrom(s => s.Grupa.Nazwa))
            //    .ForMember(u => u.ImieNazwiskoWychowawcy,
            //        s => s.MapFrom(s => 
            //            $"{s.Wychowawca.Imie} {s.Wychowawca.Nazwisko}"));
        }
    }
}
