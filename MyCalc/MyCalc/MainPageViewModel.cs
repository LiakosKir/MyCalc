using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xamarin.Forms;

namespace MyCalc
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private string _displayText = "";
        public string DisplayText
        {
            get { return _displayText; }
            set
            {
                if (_displayText != value)
                {
                    _displayText = value;
                    OnPropertyChanged();
                    DelCharCommand.ChangeCanExecute();
                }
            }
        }

        public Command AddCharCommand { get; set; }
        public Command DelCharCommand { get; set; }
        public Command OperationCommand { get; set; }
        public Command CalcCommand { get; set; }

        private string Op;
        private double Op1;
        private double Op2;

        public MainPageViewModel()
        {
            AddCharCommand = new Command<string>((key) =>
            {
                DisplayText += key;
            });

            DelCharCommand = new Command(
                () =>
                {
                    DisplayText = DisplayText.Substring(0, DisplayText.Length - 1);
                },
                () =>
                {
                    return DisplayText.Length > 0;
                });

            OperationCommand = new Command<string>((key) =>
            {
                Op1 = Convert.ToDouble(DisplayText);
                Op = key;
                DisplayText = "";
            });

            CalcCommand = new Command(() =>
            {
                Op2 = Convert.ToDouble(DisplayText);

                switch(Op)
                {
                    case "+":
                        DisplayText = (Op1 + Op2).ToString();
                        break;
                    case "-":
                        DisplayText = (Op1 - Op2).ToString();
                        break;
                    case "*":
                        DisplayText = (Op1 * Op2).ToString();
                        break;
                    case "/":
                        DisplayText = (Op1 / Op2).ToString();
                        break;
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
