using System;
using static System.Math;
using System.Collections.Generic;
using QuickInterest.Services;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace QuickInterest.ViewModel
{
    public class LoanPageVM : INotifyPropertyChanged
    {
        public LoanPageVM()
        {
            CalculatePayoffCommand = new Command(CalculateLoanPayoff);
           
        }


        public ICommand CalculatePayoffCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private float principalLoanAmount;
        public float PrincipalLoanAmount
        {
            get { return principalLoanAmount; }
            set
            {
                principalLoanAmount = value;
                OnPropertyChanged(nameof(PrincipalLoanAmount));


            }
        }

        private float termLoan;
        public float TermLoan
        {
            get { return termLoan; }
            set
            {
                termLoan = value;
                OnPropertyChanged(nameof(TermLoan));

            }
        }

        private float remainingTermLoan;
        public float RemainingTermLoan
        {
            get { return remainingTermLoan; }
            set
            {
                remainingTermLoan = value;
                OnPropertyChanged(nameof(CalculateRemainingTerm));
            }
        }

        private float interestLoan;
        public float InterestLoan
        {
            get { return interestLoan; }
            set
            {
                interestLoan = value;
                OnPropertyChanged(nameof(InterestLoan));
            }
        }


        private float resultsLoan;
        public float ResultsLoan
        {
            get { return resultsLoan; }
            set
            {
                resultsLoan = value;
                OnPropertyChanged(nameof(DisplayLoanResults));
            }
        }

        private float monthlyPayments;
        public float MonthlyPayments
        {
            get { return monthlyPayments; }
            set
            {
                monthlyPayments = value;
                OnPropertyChanged(nameof(MonthlyPayments));
            }
        }

        private float additionPrincipal;
        public float AdditionalPrincipal
        {
            get => additionPrincipal;
            set
            {
                additionPrincipal = value;
                OnPropertyChanged(nameof(AdditionalPrincipal));
            }

            
        }


        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;

        }


        private float interestPaid;

        private float initialPrincipal;


        
        public string DisplayLoanResults => $"You will pay your loan {Round(ResultsLoan,2)} faster while paying a total of ${Round(interestPaid,2)} in interest!!";



        void CalculateLoanPayoff()
        {

            exceptionHandling();


            interestLoan /= 100;

            initialPrincipal = principalLoanAmount;

            float towardsPrincipal;

            Console.WriteLine(interestLoan);


            for (int i = 1; i <= remainingTermLoan; i++)
            {
                if(principalLoanAmount <= monthlyPayments)
                {
                    
                    remainingTermLoan = CalculateRemainingTerm(i) ;
                    //ResultsLoan = remainingTermLoan;
                    break;


                    //Console.WriteLine("Finished.......");

                    //Console.WriteLine($"Interest paid: {interestPaid}");

                    //Console.WriteLine($"Monthly Payments: {monthlyPayments}");

                    //Console.WriteLine($"Principal: {principalLoanAmount}");

                    //Console.WriteLine($"Remaining months: {remainingTermLoan}\n");
                }
               

                    //interestPaid += principalLoanAmount * (interestLoan / 365 * 30);

                    interestPaid = principalLoanAmount * (interestLoan / 365 * 30);

                    towardsPrincipal = monthlyPayments - interestPaid;

                    principalLoanAmount -= towardsPrincipal + AdditionalPrincipal;




                    //Console.WriteLine($"Interest paid: {interestLoan}");

                    //Console.WriteLine($"Monthly Payments: {monthlyPayments}");

                    //Console.WriteLine($"Principal: {principalLoanAmount}");

                    //Console.WriteLine($"Remaining months: {remainingTermLoan - i}\n");


            }
            ResultsLoan = remainingTermLoan;


        }

        private float CalculateRemainingTerm(int number)
        {
            int moduloNum = number % 12;

            int remainMonths = number - (12 * moduloNum);

            number = (int)(termLoan - remainMonths);


            return number;
        }



        void exceptionHandling()
        {
            DialogService dialogService = new DialogService();
            if(interestLoan < 0)
            {
                _ = dialogService.ValidationError("Invalid Interest", "Please try again", "OK");
            }
            else if(termLoan <= 0)
            {
                _ = dialogService.ValidationError("Invalid Term", "Please try again", "OK");
            }
            else if(principalLoanAmount < 0)
            {
                _ = dialogService.ValidationError("Invalid Principal", "Please try again", "OK");
            }
        }

       
    }
}
