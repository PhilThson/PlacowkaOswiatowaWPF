﻿using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces
{
    //public interface ILoadAsync<T>
    //    where T : WorkspaceViewModel, IDownload
    //{
    //    //public Task Pobierz();
    //    static T Load(IPlacowkaRepository repository, IMapper mapper)
    //    {
    //        var vm = Activator.CreateInstance(typeof(T), repository, mapper) as T;
    //        Task.Run(async () => await vm.Download());
    //        return vm;
    //    }
    //}
    
    /// <summary>
    /// Interfejs pozwala na asynchroniczne pobieranie zasobów z bazy danych
    /// </summary>
    public interface ILoadable
    {
        Task LoadAsync();
    }
}
