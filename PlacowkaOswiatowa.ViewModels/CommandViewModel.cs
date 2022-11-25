using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PlacowkaOswiatowa.ViewModels.Abstract;

namespace PlacowkaOswiatowa.ViewModels
{
    //klasa która do tworzenia komend z menu z lewej strony
    //każdy CVM zawiera komendę, która wywoła funkcję, która otworzy zakładkę
    public class CommandViewModel : BaseViewModel
    {
        #region Właściwości
        public string DisplayName { get; set; }
        public ICommand Command { get; set; }

        #endregion

        #region Konstruktor
        public CommandViewModel(string displayName, ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException("Command");
            this.DisplayName = displayName;
            this.Command = command;
        }

        #endregion
    }
}
