﻿using System.Threading.Tasks;
using System.Windows.Input;

namespace PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object? parameter);
    }
}
