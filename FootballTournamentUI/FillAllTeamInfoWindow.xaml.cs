using FootballTournamentUI.ViewModels;
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

namespace FootballTournamentUI
{
    /// <summary>
    /// Interaction logic for FillWindow.xaml
    /// </summary>
    public partial class FillAllTeamInfoWindow : Window
    {
        private ViewModel _viewModel;

        public FillAllTeamInfoWindow(ViewModel viewModel, string type)
        {
            InitializeComponent();
            _viewModel = viewModel;
            if(type == "Update" || type == "Delete")
            {
                nameTextBlock.FontWeight = FontWeights.DemiBold;
                cityTextBlock.FontWeight = FontWeights.DemiBold;
            }
            button1.Content = type;
            Title = type + " team";
            DataContext = _viewModel;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
