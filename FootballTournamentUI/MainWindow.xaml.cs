using FootballTournamentUI.DAL;
using FootballTournamentUI.Services;
using FootballTournamentUI.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FootballTournamentUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FootballTournamentService _teamService;
        private FootballTournamentService _matchService;
        private ViewModel _viewModel;

        private bool isInitialized = false;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModel();
            DataContext = _viewModel;
            FootballTournamentContext context = new FootballTournamentContext();
            _teamService = new FootballTournamentService(context, teamTableRowGroup, _viewModel);
            _matchService = new FootballTournamentService(context, matchTableRowGroup, _viewModel);
        }

        //private void teamMenuListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    _viewModel.TeamInfo = "";
        //    switch(teamMenuListView.SelectedIndex)
        //    {
        //        case 0:
        //            teamTableRowGroup = _teamService.ShowTeams();
        //            break;
        //        case 1:
        //            teamTableRowGroup = _teamService.FindByName();
        //            break;
        //        case 2:
        //            teamTableRowGroup = _teamService.FindByCity();
        //            break;
        //        case 3:
        //            teamTableRowGroup = _teamService.FindByNameAndCity();
        //            break;
        //        case 4:
        //            teamTableRowGroup = _teamService.ShowTeamWithMaxVictories();
        //            break;
        //        case 5:
        //            teamTableRowGroup = _teamService.ShowTeamWithMaxLosses();
        //            break;
        //        case 6:
        //            teamTableRowGroup = _teamService.ShowTeamWithMaxDraws();
        //            break;
        //        case 7:
        //            teamTableRowGroup = _teamService.ShowTeamWithMaxScored();
        //            break;
        //        case 8:
        //            teamTableRowGroup = _teamService.ShowTeamWithMaxConceded();
        //            break;
        //        case 9:
        //            _teamService.AddTeam();
        //            break;
        //        case 10:
        //            _teamService.UpdateTeam();
        //            break;
        //        case 11:
        //            _teamService.DeleteTeam();
        //            break;
        //        case 12:
        //            _teamService.TopBombardiersOfTeam();
        //            break;
        //        case 13:
        //            _teamService.TopBombardiersOfTournament();
        //            break;
        //        case 14:
        //            _teamService.TopTeamsByMaxScored();
        //            break;
        //        case 15:
        //            _teamService.TopTeamsByMinConceded();
        //            break;
        //        case 16:
        //            _teamService.TopTeamsByMaxPoints();
        //            break;
        //        case 17:
        //            _teamService.TopTeamsByMinPoints();
        //            break;
        //    }
        //}

        //private void matchMenuListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    _viewModel.MatchInfo = "";
        //    switch (matchMenuListView.SelectedIndex)
        //    {
        //        case 0:
        //            matchTableRowGroup = _matchService.ShowMatches(); 
        //            break;
        //        case 1:
        //            matchTableRowGroup = _matchService.ShowMatchInfo();
        //            break;
        //        case 2:
        //            matchTableRowGroup = _matchService.ShowMatchWithDate();
        //            break;
        //        case 3:
        //            matchTableRowGroup = _matchService.ShowMatchesOfTeam();
        //            break;
        //        case 4:
        //            matchTableRowGroup = _matchService.ShowPlayersScoredInDate();
        //            break;
        //        case 5:
        //            _matchService.AddMatch();
        //            break;
        //        case 6:
        //            _matchService.UpdateMatch();
        //            break;
        //        case 7:
        //            _matchService.DeleteMatch();
        //            break;
        //    }
        //}

        //private void teamMenuListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    teamMenuListView_SelectionChanged(sender, new SelectionChangedEventArgs(Selector.SelectionChangedEvent, new List<object>(), new List<object>()));
        //}

        //private void matchMenuListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    matchMenuListView_SelectionChanged(sender, new SelectionChangedEventArgs(Selector.SelectionChangedEvent, new List<object>(), new List<object>()));
        //}

        private void TeamShowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.TeamInfo = "";
            switch (teamShowComboBox.SelectedIndex)
            {
                case 0:
                    teamTableRowGroup = _teamService.ShowTeams();
                    break;
                case 1:
                    teamTableRowGroup = _teamService.ShowTeamWithMaxVictories();
                    break;
                case 2:
                    teamTableRowGroup = _teamService.ShowTeamWithMaxLosses();
                    break;
                case 3:
                    teamTableRowGroup = _teamService.ShowTeamWithMaxDraws();
                    break;
                case 4:
                    teamTableRowGroup = _teamService.ShowTeamWithMaxScored();
                    break;
                case 5:
                    teamTableRowGroup = _teamService.ShowTeamWithMaxConceded();
                    break;
                case 6:
                    _teamService.TopTeamsByMaxScored();
                    break;
                case 7:
                    _teamService.TopTeamsByMinConceded();
                    break;
                case 8:
                    _teamService.TopTeamsByMaxPoints();
                    break;
                case 9:
                    _teamService.TopTeamsByMinPoints();
                    break;
            }
        }

        private void TeamFindComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.MatchInfo = "";
            if (isInitialized)
            {
                switch (teamFindComboBox.SelectedIndex)
                {
                    case 0:
                        teamTableRowGroup = _teamService.FindByName();
                        break;
                    case 1:
                        teamTableRowGroup = _teamService.FindByCity();
                        break;
                    case 2:
                        teamTableRowGroup = _teamService.FindByNameAndCity();
                        break;
                }
            }
        }

        private void TeamEditComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.MatchInfo = "";
            if (isInitialized)
            {
                switch (teamEditComboBox.SelectedIndex)
                {
                    case 0:
                        _teamService.AddTeam();
                        break;
                    case 1:
                        _teamService.UpdateTeam();
                        break;
                    case 2:
                        _teamService.DeleteTeam();
                        break;
                }
            }
        }

        private void teamShowComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TeamShowComboBox_SelectionChanged(sender, new SelectionChangedEventArgs(Selector.SelectionChangedEvent, new List<object>(), new List<object>()));
        }

        private void teamFindComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TeamFindComboBox_SelectionChanged(sender, new SelectionChangedEventArgs(Selector.SelectionChangedEvent, new List<object>(), new List<object>()));
        }

        private void teamEditComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TeamEditComboBox_SelectionChanged(sender, new SelectionChangedEventArgs(Selector.SelectionChangedEvent, new List<object>(), new List<object>()));
        }

        private void matchShowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.MatchInfo = "";
            switch (matchShowComboBox.SelectedIndex)
            {
                case 0:
                    matchTableRowGroup = _matchService.ShowMatches();
                    break;
                case 1:
                    matchTableRowGroup = _matchService.ShowMatchInfo();
                    break;
                case 2:
                    matchTableRowGroup = _matchService.ShowMatchWithDate();
                    break;
                case 3:
                    matchTableRowGroup = _matchService.ShowMatchesOfTeam();
                    break;
            }
        }

        private void matchEditComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.MatchInfo = "";
            if (isInitialized)
            {
                switch (matchEditComboBox.SelectedIndex)
                {
                    case 0:
                        _matchService.AddMatch();
                        break;
                    case 1:
                        _matchService.UpdateMatch();
                        break;
                    case 2:
                        _matchService.DeleteMatch();
                        break;
                }
            }
            else isInitialized = true;
        }

        private void matchShowComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            matchShowComboBox_SelectionChanged(sender, new SelectionChangedEventArgs(Selector.SelectionChangedEvent, new List<object>(), new List<object>()));
        }

        private void matchEditComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            matchEditComboBox_SelectionChanged(sender, new SelectionChangedEventArgs(Selector.SelectionChangedEvent, new List<object>(), new List<object>()));
        }
    }
}