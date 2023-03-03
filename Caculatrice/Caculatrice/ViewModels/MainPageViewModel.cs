using Caculatrice.Models;
using Caculatrice.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Caculatrice.ViewModels
{
    class MainPageViewModel : BaseViewModel
    {
        private readonly ICalculator _calculator;
        private string _numberText;
        private string _previousNumberText;
        private string _operationText;
        private bool _clearTextOnNextPress;

        public ICommand NumberCommand { get; }
        public ICommand ClearLastCommand { get; }
        public ICommand ClearAllCommand { get; }
        public ICommand OperationCommand { get; }
        public ICommand CalculateCommand { get; }


        public string OperationText
        {
            get
            {
                return _operationText;
            }
            private set
            {
                _operationText = value;
                NotifyPropertyChanged(nameof(OperationText));
                NotifyPropertyChanged(nameof(BottomInfoText));
            }
        }

        public string NumberText
        {
            get
            {
                return _numberText;
            }
            private set
            {
                _numberText = value;
                NotifyPropertyChanged(nameof(NumberText));
            }
        }

        public string PreviousNumberText
        {
            get
            {
                return _previousNumberText;
            }
            private set
            {
                _previousNumberText = value;
                NotifyPropertyChanged(nameof(PreviousNumberText));
                NotifyPropertyChanged(nameof(BottomInfoText));
            }
        }

        public string BottomInfoText
        {
            get
            {
                return $"{PreviousNumberText} {OperationText}";
            }
        }

        public MainPageViewModel()
        {
            NumberText = "";
            OperationText = "";

            NumberCommand = new Command(NumberButtonClick);
            ClearAllCommand = new Command(ClearAllButtonClick);
            ClearLastCommand = new Command(ClearLastButtonClick);
            OperationCommand = new Command(OperationClick);
            CalculateCommand = new Command(CalculateClick);
            _calculator = new Models.Calculator();
        }

        private void ClearAllButtonClick()
        {
            NumberText = "";
            OperationText = "";
            PreviousNumberText = "";
        }

        private void ClearLastButtonClick()
        {
            if (NumberText.Length > 0)
                NumberText = NumberText.Remove(NumberText.Length - 1);
        }

        private void NumberButtonClick(object sender)
        {
            Button button = sender as Button;

            if (_clearTextOnNextPress)
            {
                _clearTextOnNextPress = false;
                NumberText = "";
            }

            if (NumberText.StartsWith("0"))
                NumberText = "";

            if (button.Text == ".")
            {
                if (!NumberText.Contains("."))
                {
                    NumberText += ".";
                }
            }
            else
            {
                NumberText += button.Text;
            }

            NotifyPropertyChanged(nameof(NumberText));
        }

        private void OperationClick(object sender)
        {
            Button button = sender as Button;

            if (NumberIsValid(NumberText))
            {
                decimal tempNumber;

                if (decimal.TryParse(NumberText, out tempNumber))
                {
                    _calculator.Number1 = tempNumber;

                    _clearTextOnNextPress = true;
                    OperationText = button.Text;
                    PreviousNumberText = NumberText;
                }
                else
                    Alert.Display("Invalid number", "Number is too large or is invalid", "OK");
            }
            else if (string.IsNullOrWhiteSpace(NumberText) && button.Text == _calculator.SubstractSymbol)
                NumberText += "-";
        }

        private void CalculateClick()
        {
            if (NumberIsValid(NumberText))
            {
                decimal tempNumber;

                if (decimal.TryParse(NumberText, out tempNumber))
                {
                    _calculator.Number2 = tempNumber;

                    var result = _calculator.Calculate(OperationText);
                    NumberText = result.ToString();
                    OperationText = "";
                    PreviousNumberText = "";
                }
                else
                    Alert.Display("Invalid number", "Number is too large or is invalid", "OK");
            }
        }

        private bool NumberIsValid(string numberText)
        {
            if (string.IsNullOrWhiteSpace(numberText) || numberText.EndsWith("."))
                return false;

            return true;
        }
    }
}
