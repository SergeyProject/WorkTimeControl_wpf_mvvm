using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    internal class MyCommand : ICommand
    {
        private readonly MainWindowViewModel viewModel;
        public RelayCommand MessageBoxCommand {  get;}
        public MyCommand(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
            MessageBoxCommand = new RelayCommand(MsgBox);
        }

        private void MsgBox()
        {
            MessageBox.Show("OK!");
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            viewModel.Person = (Person)parameter;
            MessageBox.Show(viewModel.Person.Name);
            //viewModel.ListBoxItemClickCommand.Execute(parameter);
        }

        public event EventHandler? CanExecuteChanged;
    }
}
