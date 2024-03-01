using FootballTournamentUI.DAL;
using FootballTournamentUI.DAL.Entities;
using FootballTournamentUI.DAL.Repositories;
using FootballTournamentUI.ViewModels;
using Microsoft.Extensions.FileSystemGlobbing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using Match = FootballTournamentUI.DAL.Entities.Match;

namespace FootballTournamentUI.Providers
{
    public class MatchProvider
    {
        private readonly MatchRepository _matchRepository;
        private readonly TeamRepository _teamRepository;

        private TableRowGroup _table;
        private ViewModel _viewModel;

        public MatchProvider(FootballTournamentContext context, TableRowGroup tableRowGroup, ViewModel viewModel)
        {
            _matchRepository = new MatchRepository(context);
            _teamRepository = new TeamRepository(context);
            _table = tableRowGroup;
            _viewModel = viewModel;
        }

        private void InitializeMatchTable()
        {
            _table.Rows.Clear();
            var content = new List<string>()
                { "Team1Name", "Team2Name", "Team1Score", "Team2Score", "Date" };

            AddRow(content, isHeader: true);
        }

        private void InitializePlayerTable()
        {
            _table.Rows.Clear();
            var content = new List<string>()
                { "FullName", "Country", "Number", "Position", "Team" };

            AddRow(content, isHeader: true);
        }

        private void AddRow(List<string> content, bool isHeader = false)
        {
            var row = new TableRow();

            if (isHeader)
                row.Background = new SolidColorBrush(Colors.LightGray);
            else if (_table.Rows.Count % 2 == 0)
                row.Background = new SolidColorBrush(Colors.AliceBlue);
            else if (_table.Rows.Count % 2 == 1)
                row.Background = new SolidColorBrush(Colors.LightBlue);

            foreach (string item in content)
            {
                row.Cells.Add(new TableCell(new Paragraph(new Run(item))));
            }
            _table.Rows.Add(row);
        }

        private void AddMatchToTable(Match match)
        {
            var content = new List<string>()
                { match.Team1.Name, match.Team2.Name, match.Team1Score.ToString(), match.Team2Score.ToString(), DateOnly.FromDateTime(match.Date).ToString() };
            AddRow(content);
        }

        private void AddPlayerToTable(Player player)
        {
            var content = new List<string>()
                { player.FullName, player.Country, player.Number.ToString(), player.Position, player.Team.Name };
            AddRow(content);
        }

        public TableRowGroup ShowMatch(Match match)
        {
            if (match != null)
            {
                InitializeMatchTable();
                AddMatchToTable(match);
            }
            else
                _viewModel.MatchInfo = "No results";

            return _table;
        }

        public TableRowGroup ShowMatches()
        {
            InitializeMatchTable();
            var matches = _matchRepository.GetMatches();
            foreach (var match in matches)
            {
                AddMatchToTable(match);
            }

            return _table;
        }

        public TableRowGroup ShowMatches(IEnumerable<Match> matches)
        {
            if (matches.Count() > 0)
            {
                InitializeMatchTable();
                foreach (var match in matches)
                {
                    AddMatchToTable(match);
                }
            }
            else
                _viewModel.MatchInfo = "No results";

            return _table;
        }

        private bool CheckDateTextBox()
        {
            try
            {
                if (_viewModel.MatchDate != "")
                {
                    var date = DateTime.Parse(_viewModel.MatchDate);
                    return true;
                }
                else
                    _viewModel.MatchInfo = "Enter match date";

            }
            catch { _viewModel.MatchInfo = "Enter data in correct format"; }
            return false;
        }

        private bool FullCheckMatchTextBox()
        {
            try
            {
                if (_viewModel.MatchTeamOneName != "" && _viewModel.MatchTeamTwoName != ""
                    && _viewModel.MatchTeamOneScored != "" && _viewModel.MatchTeamTwoScored != ""
                    && _viewModel.MatchDate != "")
                {
                    var score1 = int.Parse(_viewModel.MatchTeamOneScored);
                    var score2 = int.Parse(_viewModel.MatchTeamTwoScored);
                    var date = DateTime.Parse(_viewModel.MatchDate);
                    return true;
                }
                else
                    _viewModel.MatchInfo = "Enter all fields of match";
            }
            catch { _viewModel.MatchInfo = "Enter data in correct format"; }
            return false;
        }

        public TableRowGroup ShowMatchInfo()
        {
            ClearMatchInfo();
            var dialog = new FillAllMatchInfoWindow(_viewModel, "Show").ShowDialog();
            if (CheckDateTextBox() && _viewModel.MatchTeamOneName != "" && _viewModel.MatchTeamTwoName != "")
            {
                var match = new Match()
                {
                    Team1 = _teamRepository.FindByOnlyName(_viewModel.MatchTeamOneName),
                    Team2 = _teamRepository.FindByOnlyName(_viewModel.MatchTeamTwoName),
                    Date = DateTime.Parse(_viewModel.MatchDate)
                };

                if (match.Team1 != null && match.Team2 != null)
                {
                    match = _matchRepository.FindMatch(match);
                    if (match != null)
                    {
                        ShowMatch(match);
                    }
                    else
                        _viewModel.TeamInfo = "This match doesn't exist";
                }
                else
                    _viewModel.MatchInfo = "One or two teams doesn't exist";
            }

            return _table;
        }

        public TableRowGroup ShowMatchWithDate()
        {
            if (CheckDateTextBox())
            {
                var date = DateTime.Parse(_viewModel.MatchDate);
                ShowMatches(_matchRepository.MatchesWithDate(date));
            }

            return _table;
        }

        public TableRowGroup ShowMatchesOfTeam()
        {
            if (_viewModel.MatchTeamNameToFind != "" && _viewModel.MatchTeamCityToFind != "")
            {
                var team = _teamRepository.FindByNameAndCity(_viewModel.MatchTeamNameToFind, _viewModel.MatchTeamCityToFind);

                if (team != null)
                {
                    ShowMatches(_matchRepository.MatchesWithTeam(team));
                }
                else
                    _viewModel.MatchInfo = "This team doesn't exist!";
            }
            else
                _viewModel.MatchInfo = "Enter team name and city";

            return _table;
        }

        public TableRowGroup ShowPlayersScoredInDate()
        {
            if (CheckDateTextBox())
            {
                InitializePlayerTable();
                DateTime date = DateTime.Parse(_viewModel.MatchDate);
                var matches = _matchRepository.MatchesWithDate(date);
                if (matches.Count() > 0)
                {
                    foreach (var match in matches)
                    {
                        foreach (var player in match.PlayersScored)
                        {
                            AddPlayerToTable(player);
                        }
                    }
                }
                else
                    _viewModel.MatchInfo = "No results";
            }

            return _table;
        }

        void ClearMatchInfo()
        {
            _viewModel.MatchTeamOneName = "";
            _viewModel.MatchTeamTwoName = "";
            _viewModel.MatchTeamOneScored= "";
            _viewModel.MatchTeamTwoScored = "";
            _viewModel.MatchDate = "";
        }

        public void AddMatch()
        {
            ClearMatchInfo();
            var dialog = new FillAllMatchInfoWindow(_viewModel, "Add").ShowDialog();
            if (FullCheckMatchTextBox() && dialog == true)
            {
                var match = new Match()
                {
                    Team1 = _teamRepository.FindByOnlyName(_viewModel.MatchTeamOneName),
                    Team2 = _teamRepository.FindByOnlyName(_viewModel.MatchTeamTwoName),
                    Team1Score = int.Parse(_viewModel.MatchTeamOneScored),
                    Team2Score = int.Parse(_viewModel.MatchTeamTwoScored),
                    Date = DateTime.Parse(_viewModel.MatchDate)
                };

                if (match.Team1 != null && match.Team2 != null)
                {
                    var tempMatch = _matchRepository.FindMatch(match);
                    if (tempMatch == null)
                    {
                        if (match.Team1.Name != match.Team2.Name)
                        {
                            _matchRepository.AddMatch(match);
                            _viewModel.MatchInfo = "Match added successfully";
                        }
                        else
                            _viewModel.MatchInfo = "Teams can't be same";
                    }
                    else
                        _viewModel.MatchInfo = "This match is already exists!";
                }
                else
                    _viewModel.MatchInfo = "One or two teams doesn't exist";
            }
        }

        public void UpdateMatch()
        {
            ClearMatchInfo();
            var dialog = new FillAllMatchInfoWindow(_viewModel, "Update").ShowDialog();
            if (FullCheckMatchTextBox() && dialog == true)
            {
                var match = new Match()
                {
                    Team1 = _teamRepository.FindByOnlyName(_viewModel.MatchTeamOneName),
                    Team2 = _teamRepository.FindByOnlyName(_viewModel.MatchTeamTwoName),
                    Team1Score = int.Parse(_viewModel.MatchTeamOneScored),
                    Team2Score = int.Parse(_viewModel.MatchTeamTwoScored),
                    Date = DateTime.Parse(_viewModel.MatchDate)
                };

                if (match.Team1 != null && match.Team2 != null)
                {
                    match = _matchRepository.FindMatch(match);
                    if (match != null)
                    {
                        match.Team1 = _teamRepository.FindByOnlyName(_viewModel.MatchTeamOneName);
                        match.Team2 = _teamRepository.FindByOnlyName(_viewModel.MatchTeamTwoName);
                        match.Team1Score = int.Parse(_viewModel.MatchTeamOneScored);
                        match.Team2Score = int.Parse(_viewModel.MatchTeamTwoScored);
                        match.Date = DateTime.Parse(_viewModel.MatchDate);

                        _matchRepository.UpdateMatch(match);
                        _viewModel.MatchInfo = "Match updated successfully";
                    }
                    else
                        _viewModel.MatchInfo = "This match doesn't exist!";
                }
                else
                    _viewModel.MatchInfo = "One or two teams doesn't exist";
            }
        }

        public void DeleteMatch()
        {
            ClearMatchInfo();
            var dialog1 = new FillAllMatchInfoWindow(_viewModel, "Delete").ShowDialog();
            if (CheckDateTextBox() && _viewModel.MatchTeamOneName != "" && _viewModel.MatchTeamTwoName != "" && dialog1 == true)
            {
                var match = new Match()
                {
                    Team1 = _teamRepository.FindByOnlyName(_viewModel.MatchTeamOneName),
                    Team2 = _teamRepository.FindByOnlyName(_viewModel.MatchTeamTwoName),
                    Date = DateTime.Parse(_viewModel.MatchDate)
                };

                if (match.Team1 != null && match.Team2 != null)
                {
                    match = _matchRepository.FindMatch(match);
                    if (match != null)
                    {
                        var dialog2 = new DialogWindow("Are you sure you want to delete this match?");
                        bool? result = dialog2.ShowDialog();

                        if (result == true)
                        {
                            _matchRepository.DeleteMatch(match);
                            _viewModel.MatchInfo = "Match deleted successfully";
                        }
                        else
                            return;
                    }
                    else
                        _viewModel.TeamInfo = "This match doesn't exist";
                }
                else
                    _viewModel.MatchInfo = "One or two teams doesn't exist";
            }
        }
    }
}
