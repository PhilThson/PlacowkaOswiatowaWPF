using Microsoft.EntityFrameworkCore;
using PlacowkaOswiatowa.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PlacowkaOswiatowa.Infrastructure.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> IncludeUmowa<T>(this IQueryable<T> query,
            Expression<Func<T, Umowa>> expression) where T : class =>
            query
                .Include(expression).ThenInclude(u => u.Etat)
                .Include(expression).ThenInclude(u => u.Stanowisko);

        public static IQueryable<T> IncludeAdresy<T>(this IQueryable<T> query,
            Expression<Func<T, ICollection<Adres>>> expression) where T : class =>
            query
                .Include(expression).ThenInclude(a => a.Panstwo)
                .Include(expression).ThenInclude(a => a.Miejscowosc)
                .Include(expression).ThenInclude(a => a.Ulica)
            ;

        public static IQueryable<T> IncludeAdres<T>(this IQueryable<T> query,
            Expression<Func<T, Adres>> expression) where T : class =>
            query
                .Include(expression).ThenInclude(a => a.Panstwo)
                .Include(expression).ThenInclude(a => a.Miejscowosc)
                .Include(expression).ThenInclude(a => a.Ulica)
            ;

        public static IQueryable<Pracownik> IncludePracownicyAdresy(this IQueryable<Pracownik> query) =>
            query
            .Include(p => p.PracownikPracownicyAdresy).ThenInclude(pa => pa.Adres).ThenInclude(a => a.Panstwo)
            .Include(p => p.PracownikPracownicyAdresy).ThenInclude(pa => pa.Adres).ThenInclude(a => a.Miejscowosc)
            .Include(p => p.PracownikPracownicyAdresy).ThenInclude(pa => pa.Adres).ThenInclude(a => a.Ulica)
            ;
    }
}
