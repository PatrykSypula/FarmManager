using System.Windows;
using FarmManager.App.ViewModels.Workdays;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Workdays
{
    /// <summary>
    /// Interaction logic for EditWorkdayWindow.xaml
    /// </summary>
    public partial class WorkdayEditWindow : Window
    {
        public Workday? Workday { get; private set; }
        public WorkdayEditWindow(int id)
        {
            InitializeComponent();
            //Loaded += async (_, __) => await ((WorkdayEditViewModel)DataContext).InitializeAsync(id);
            //if (DataContext is WorkdayEditViewModel vm)
            //{
            //    vm.RequestClose += workday =>
            //    {
            //        Workday = workday;
            //        DialogResult = true;
            //    };
            //}
        }
    }
}
