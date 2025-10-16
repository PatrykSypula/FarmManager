using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FarmManager.App.ViewModels.Workdays;
using FarmManager.Model.Model;

namespace FarmManager.App.Views.Workdays;
/// <summary>
/// Interaction logic for WorkdaysSelectTypeWindow.xaml
/// </summary>
public partial class WorkdaysSelectTypeWindow : Window
{
    public WorkdayType? WorkdayType { get; private set; }
    public WorkdaysSelectTypeWindow()
    {
        InitializeComponent();
        if (DataContext is WorkdaysSelectTypeViewModel vm)
        {
            vm.RequestClose += workdayType =>
            {
                WorkdayType = workdayType;
                DialogResult = true;
            };
        }
    }
}
