using System;
using System.Threading.Tasks;

namespace QuickInterest.Services
{
    public class DialogService : IDialogService
    {
        public DialogService()
        {
        }

        public async Task ValidationError(string title, string message, string buttonText)
        {
            await App.Current.MainPage.DisplayAlert(title, message,buttonText);
        }
    }
}
