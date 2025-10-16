using System.Collections.ObjectModel;
using FarmManager.App.Helpers;
using FarmManager.App.Models.Payments;
using FarmManager.App.Models.Sprayings;
using FarmManager.App.Views.Payments;
using FarmManager.App.Views.Sprayings;
using FarmManager.Model.Model;
using FarmManager.Services.Interfaces;

namespace FarmManager.App.ViewModels.Payments;

public class PaymentsViewModel(IPaymentService paymentService) : BaseViewModel
{
    #region Properties

    public PaymentsModel Model = new PaymentsModel();

    public ObservableCollection<Payment> Payments
    {
        get { return Model.Payments; }
        set
        {
            Model.Payments = value;
            OnPropertyChanged();
        }
    }

    public async Task InitializeAsync()
    {
        Payments = new ObservableCollection<Payment>(await paymentService.GetAll(false));
    }

    public Payment SelectedItem
    {
        get { return Model.SelectedItem; }
        set
        {
            Model.SelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public RelayCommand Create => new RelayCommand(execute => OpenPaymentAddWindow());
    private async void OpenPaymentAddWindow()
    {
        var window = new PaymentAddWindow();
        if (window.ShowDialog() == true && window.Payment != null)
        {
            var payment = await paymentService.Get(window.Payment.Id);
            Payments.Add(payment);
            OnPropertyChanged(nameof(Payments));
        }
    }

    public RelayCommand Edit => new RelayCommand(execute => OpenPaymentEditWindow());
    private void OpenPaymentEditWindow()
    {
        var window = new PaymentEditWindow(SelectedItem.Id);
        if (window.ShowDialog() == true && window.Payment != null)
        {
            var payment = window.Payment;
            var index = Payments.ToList().FindIndex(d => d.Id == payment.Id);

            if (index >= 0)
            {
                if (payment.IsDeleted)
                {
                    Payments.RemoveAt(index);
                }
                else
                {
                    Payments.RemoveAt(index);
                    Payments.Insert(index, payment);
                }
            }
            OnPropertyChanged(nameof(Payments));
        }
    }
}
