using System.Windows;
using FarmManager.App.ViewModels.Payments;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Payments;

public partial class PaymentEditWindow : Window
{
    public Payment? Payment { get; private set; }
    public PaymentEditWindow(int id)
    {
        InitializeComponent();
        Loaded += async (_, __) => await ((PaymentEditViewModel)DataContext).InitializeAsync(id);
        if (DataContext is PaymentEditViewModel vm)
        {
            vm.RequestClose += payment =>
            {
                Payment = payment;
                DialogResult = true;
            };
        }
    }
}
