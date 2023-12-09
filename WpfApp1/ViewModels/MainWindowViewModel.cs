using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class MainWindowViewModel : ObservableObject, INotifyPropertyChanged
    {
       


        public RelayCommand ListBoxItemClickCommand { get; }
    
        

        private ObservableCollection<Person> _Persons; //= new ObservableCollection<Person>();

        public ObservableCollection<Person> Persons
        {
            get
            {
                return _Persons;
            }
            set
            {
                _Persons = value;
                OnPropertyChanged("UserD");
            }

        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

       
        public MainWindowViewModel()
        {
            Persons = new ObservableCollection<Person>
            {
            new Person { Name = "John", Age = 25 },
            new Person { Name = "Jane", Age = 30 },
            new Person { Name = "Alex", Age = 20 },
            };
            ListBoxItemClickCommand = new RelayCommand(Msg);
           
        }

        private Person _Person;

        public Person Person
        {
            get { return _Person;}
            set
            {
                _Person = value;
                OnPropertyChanged(nameof(Person));
            }
        }
        private void Msg()
        {
            MessageBox.Show("OK!");
        }

    }
}
