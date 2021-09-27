using System;
using System.Threading.Tasks;

namespace QuickInterest.Services
{
    public interface IDialogService
    {

        Task ValidationError(string title, string message, string buttonText);

    }
}
