using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PlacowkaOswiatowa.Views
{
    /// <summary>
    /// Logika interakcji dla klasy WszystkieFakturyView.xaml
    /// </summary>
    public partial class WszystkieFakturyView : UserControl
    {
        public List<Person> people = new List<Person>();
        public Person user = new Person();

        public WszystkieFakturyView()
        {
            InitializeComponent();

            people.Add(new Person { FirstName="Alice", LastName="Smith" });
            people.Add(new Person { FirstName="Bob", LastName="Cortney" });
            people.Add(new Person { FirstName="John", LastName="Woznak" });

            mojComboBox.ItemsSource = people;
            user.BirthDay = mojDatePicker.SelectedDate.Value;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void kliknijPrzycisk_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Cześć {oknoTekstowe.Text}! Urodziłeś się: {user.BirthDay.ToShortDateString()}");
        }
    }
    public class Person
    {
        private string _firstName;
        public string FirstName 
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }
        public string LastName { get; set; }
        public string FullName 
        { 
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public DateTime BirthDay { get; set; }
    }
}
