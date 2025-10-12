using System.Windows;
using FarmManager.App.ViewModels.Payments;
using FarmManager.App.ViewModels.Sprayings;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Payments;

public partial class PaymentAddWindow : Window
{
    public Payment? Payment { get; private set; }
    public PaymentAddWindow()
    {
        InitializeComponent();
        if (DataContext is PaymentAddViewModel vm)
        {
            vm.RequestClose += payment =>
            {
                Payment = payment;
                DialogResult = true;
            };
        }
    }
}
