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
    /// Interaction logic for FillAllMatchInfoWindow.xaml
    /// </summary>
    public partial class FillAllMatchInfoWindow : Window
    {
        private ViewModel _viewModel;

        public FillAllMatchInfoWindow(ViewModel viewModel, string type)
        {
            InitializeComponent();
            _viewModel = viewModel;
            if (type == "Update" || type == "Delete" || type == "Show")
            {
                team1TextBlock.FontWeight = FontWeights.DemiBold;
                team2TextBlock.FontWeight = FontWeights.DemiBold;
                dateTextBlock.FontWeight = FontWeights.DemiBold;
            }
            button1.Content = type;
            Title = type + " match";
            DataContext = _viewModel;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
