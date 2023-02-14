using PlacowkaOswiatowa.ViewModels.Abstract;

namespace PlacowkaOswiatowa.ViewModels
{
    public class FlagViewModel : BaseViewModel
    {
        private bool isSet;
        public FlagViewModel(bool isSet, string name)
        {
            IsSet = isSet;
            Name = name;
        }
        public bool IsSet
        {
            get => isSet;
            set
            {
                isSet = value;
                OnPropertyChanged(() => IsSet);
            }
        }
        public string Name { get; set; }
    }
}
