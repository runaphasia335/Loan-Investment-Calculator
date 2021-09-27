using System;
using static System.Math;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using QuickInterest.Services;

namespace QuickInterest.ViewModel
{
    public class MainPageVM : INotifyPropertyChanged
    {


        public MainPageVM()
        {

            CalculateInterestCommand = new Command(CalculateInterest);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ICommand CalculateInterestCommand { get; }


        private float principalAmount;
        public float PrincipalAmount
        {
            get { return principalAmount; }
            set
            {
                principalAmount = value;
                OnPropertyChanged(nameof(PrincipalAmount));
            }
        }

        
        private float term;
        public float Term
        {
            get { return term; }
            set
            {
                term = value;
                OnPropertyChanged(nameof(Term));
            }
        }

        private float interestRate;
        public float InterestRate
        {
            get { return interestRate; }
            set
            {
                interestRate = value;
                OnPropertyChanged(nameof(InterestRate));

            }
        }

        private float results;
        public float Results
        {
            get { return results; }
            set
            {
                results = value;
                OnPropertyChanged(nameof(DisplayResults));
            }
        }

        private float interestEarned;
        public float EarnedInterest
        {
            get { return interestEarned; }
            set
            {
                interestEarned = value;
                OnPropertyChanged(nameof(EarnedInterest));
            }
        }
        
        public string DisplayResults => $"Principal plus interest earned is: {Round(Results,2)} \nWith an earned interest of: {Round(EarnedInterest,2)}";



        void CalculateInterest()
        {
            term /= 12;

            interestRate /= 100;

            float insideParantheses = 1 + (interestRate / SelectedItem.Value);

            float toThePower = (float)Pow(insideParantheses, SelectedItem.Value * term);

            float principalPlusInterestEarned = principalAmount * toThePower;

            EarnedInterest = principalPlusInterestEarned - principalAmount;


            exceptionHandling();


            Results = principalPlusInterestEarned;



        }





        Dictionary<string, float> compoundsDict = new Dictionary<string, float>
        {
            {"Daily",365},{"Weekly",52},{"Monthly",12},{"Quarterly",4 },
            {"Semi-Annually",2},{"Annually",1}
        };


        public List<KeyValuePair<string, float>> compoundsList
        {
            get => compoundsDict.ToList();
        }

        private KeyValuePair<string, float> _selectedItem;
        public KeyValuePair<string, float> SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;

        }


        void exceptionHandling()
        {
            DialogService dialogService = new DialogService();
            if (interestRate < 0)
            {
                _ = dialogService.ValidationError("Invalid Interest", "Please try again", "OK");
            }
            else if (term <= 0)
            {
                _ = dialogService.ValidationError("Invalid Term", "Please try again", "OK");
            }
            else if (principalAmount < 0)
            {
                _ = dialogService.ValidationError("Invalid Principal", "Please try again", "OK");
            }
        }




    }
}
