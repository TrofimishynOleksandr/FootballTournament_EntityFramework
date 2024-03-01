using FootballTournamentUI.DAL.Entities;
using FootballTournamentUI.DAL.Repositories;
using FootballTournamentUI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using FootballTournamentUI.ViewModels;
using System.Xml.Linq;

namespace FootballTournamentUI.Providers
{
    public class TeamProvider
    {
        private readonly TeamRepository _teamRepository;
        private readonly MatchRepository _matchRepository;

        private TableRowGroup _table;
        private ViewModel _viewModel;

        public TeamProvider(FootballTournamentContext context, TableRowGroup tableRowGroup, ViewModel viewModel)
        {
            _teamRepository = new TeamRepository(context);
            _matchRepository = new MatchRepository(context);
            _table = tableRowGroup;
            _viewModel = viewModel;
        }

        private void InitializeTeamTable()
        {
            _table.Rows.Clear();
            var content = new List<string>()
                { "Name", "City", "Victories", "Losses", "Draws", "Scored", "Conceded", "Points" };

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

        public int GetPoints(Team team) => team.VictoriesAmount * 3 + team.DrawsAmount;

        private void AddTeamToTable(Team team)
        {
            var content = new List<string>()
                { team.Name, team.City, team.VictoriesAmount.ToString(), team.LossesAmount.ToString(), team.DrawsAmount.ToString(),
                  team.ScoredGoalsAmount.ToString(), team.ConcededGoalsAmount.ToString(), GetPoints(team).ToString() };
            AddRow(content);
        }

        private void AddPlayerToTable(Player player)
        {
            var content = new List<string>()
                { player.FullName, player.Country, player.Number.ToString(), player.Position, player.Team.Name };
            AddRow(content);
        }

        public TableRowGroup ShowTeam(Team team)
        {
            InitializeTeamTable();
            var content = new List<string>()
                { team.Name, team.City, team.VictoriesAmount.ToString(), team.LossesAmount.ToString(), team.DrawsAmount.ToString(),
                  team.ScoredGoalsAmount.ToString(), team.ConcededGoalsAmount.ToString(), GetPoints(team).ToString() };
            AddRow(content);

            return _table;
        }

        public TableRowGroup ShowTeams()
        {
            InitializeTeamTable();
            var teams = _teamRepository.GetTeams();
            foreach (var team in teams)
            {
                AddTeamToTable(team);
            }

            return _table;
        }

        public TableRowGroup ShowTeams(IEnumerable<Team> teams)
        {
            InitializeTeamTable();
            foreach (var team in teams)
            {
                AddTeamToTable(team);
            }

            return  _table;
        }

        private bool NameCityCheckTeamTextBox()
        {
            if (_viewModel.TeamName != "" && _viewModel.TeamCity != "")
                return true;
            else
            {
                _viewModel.TeamInfo = "Enter team name and city";
                return false;
            }
        }

        private bool TopCheckTeamTextBox()
        {
            if (_viewModel.TeamTop != "")
            {
                try
                {
                    int top = int.Parse(_viewModel.TeamTop);
                    if (top > 0)
                        return true;
                    else
                    {
                        _viewModel.TeamInfo = "Enter positive top";
                        return false;
                    }
                }
                catch
                {
                    _viewModel.TeamInfo = "Enter top in correct format";
                    return false;
                }
            }
            else
                _viewModel.TeamInfo = "Enter top";
            return false;
        }

        private bool FullCheckTeamTextBox()
        {
            if (NameCityCheckTeamTextBox())
            {
                try
                {
                    int victories = int.Parse(_viewModel.TeamVictories);
                    int losses = int.Parse(_viewModel.TeamLosses);
                    int draws = int.Parse(_viewModel.TeamDraws);
                    int scored = int.Parse(_viewModel.TeamScored);
                    int conceded = int.Parse(_viewModel.TeamConceded);
                    if (victories >= 0 && losses >= 0 && draws >= 0 && scored >= 0 && conceded >= 0)
                        return true;
                    else
                    {
                        _viewModel.TeamInfo = "Enter positive numbers";
                        return false;
                    }
                }
                catch 
                {
                    _viewModel.TeamInfo = "Enter data in correct format";
                    return false;
                }
            }

            return false;
        }

        public TableRowGroup FindByName()
        {
            if (_viewModel.TeamName != "")
            {
                InitializeTeamTable();
                var teams = _teamRepository.FindByName(_viewModel.TeamName);
                if(teams.Count() > 0)
                    ShowTeams(teams);
                else
                    _viewModel.TeamInfo = "No results";
            }
            else
                _viewModel.TeamInfo = "Enter team name first";

            return _table;
        }

        public TableRowGroup FindByCity()
        {
            if (_viewModel.TeamCity != "")
            {
                InitializeTeamTable();
                var teams = _teamRepository.FindByCity(_viewModel.TeamCity);
                if (teams.Count() > 0)
                    ShowTeams(teams);
                else
                    _viewModel.TeamInfo = "No results";
            }
            else
                _viewModel.TeamInfo = "Enter team city first";

            return _table;
        }

        public TableRowGroup FindByNameAndCity()
        {
            if (NameCityCheckTeamTextBox())
            {
                InitializeTeamTable();
                var team = _teamRepository.FindByNameAndCity(_viewModel.TeamName, _viewModel.TeamCity);
                if(team != null)
                    ShowTeam(team);
                else
                    _viewModel.TeamInfo = "No results";
            }

            return _table;
        }

        public TableRowGroup ShowTeamWithMaxVictories()
        {
            ShowTeam(_teamRepository.TeamWithMaxVictories());
            return _table;
        }

        public TableRowGroup ShowTeamWithMaxLosses()
        {
            ShowTeam(_teamRepository.TeamWithMaxLosses());
            return _table;
        }

        public TableRowGroup ShowTeamWithMaxDraws()
        {
            ShowTeam(_teamRepository.TeamWithMaxDraws());
            return _table;
        }

        public TableRowGroup ShowTeamWithMaxScored()
        {
            ShowTeam(_teamRepository.TeamWithMaxScored());
            return _table;
        }

        public TableRowGroup ShowTeamWithMaxConceded()
        {
            ShowTeam(_teamRepository.TeamWithMaxConceded());
            return _table;
        }

        public void AddTeam()
        {
            ClearTeamInfo();
            var dialog = new FillAllTeamInfoWindow(_viewModel, "Add").ShowDialog();
            if (FullCheckTeamTextBox() && dialog == true)
            {
                var team = new Team()
                {
                    Name = _viewModel.TeamName,
                    City = _viewModel.TeamCity,
                    VictoriesAmount = int.Parse(_viewModel.TeamVictories),
                    LossesAmount = int.Parse(_viewModel.TeamLosses),
                    DrawsAmount = int.Parse(_viewModel.TeamDraws),
                    ScoredGoalsAmount = int.Parse(_viewModel.TeamScored),
                    ConcededGoalsAmount = int.Parse(_viewModel.TeamConceded)
                };

                if (!_teamRepository.IsExists(team))
                {
                    _teamRepository.AddTeam(team);
                    _viewModel.TeamInfo = "Team added successfully!";
                }
                else
                    _viewModel.TeamInfo = "This team is already exists!";
            }
        }

        public void UpdateTeam()
        {
            ClearTeamInfo();
            var dialog = new FillAllTeamInfoWindow(_viewModel, "Update").ShowDialog();
            if (FullCheckTeamTextBox() && dialog == true)
            {
                var team = _teamRepository.FindByNameAndCity(_viewModel.TeamName, _viewModel.TeamCity);

                if (team != null)
                {
                    team.VictoriesAmount = int.Parse(_viewModel.TeamVictories);
                    team.LossesAmount = int.Parse(_viewModel.TeamLosses);
                    team.DrawsAmount = int.Parse(_viewModel.TeamDraws);
                    team.ScoredGoalsAmount = int.Parse(_viewModel.TeamScored);
                    team.ConcededGoalsAmount = int.Parse(_viewModel.TeamConceded);

                    _teamRepository.UpdateTeam(team);
                    _viewModel.TeamInfo = "Team updated successfully!";
                }
                else
                    _viewModel.TeamInfo = "This team doesn't exist!";
            }
        }

        public void DeleteTeam()
        {
            ClearTeamInfo();
            var dialog1 = new FillAllTeamInfoWindow(_viewModel, "Delete").ShowDialog();
            if (NameCityCheckTeamTextBox() && dialog1 == true)
            {
                var team = _teamRepository.FindByNameAndCity(_viewModel.TeamName, _viewModel.TeamCity);
                if (team != null)
                {
                    var dialog2 = new DialogWindow("Are you sure you want to delete this team?");
                    bool? result = dialog2.ShowDialog();

                    if (result == true)
                    {
                        _teamRepository.DeleteTeam(team);
                        _viewModel.TeamInfo = "Team deleted successfully!";
                    }
                    else
                        return;
                }
                else
                    _viewModel.TeamInfo = "This team doesn't exist";
            }
        }

        private void ClearTeamInfo()
        {
            _viewModel.TeamName = "";
            _viewModel.TeamCity = "";
            _viewModel.TeamVictories = "";
            _viewModel.TeamLosses = "";
            _viewModel.TeamDraws = "";
            _viewModel.TeamScored = "";
            _viewModel.TeamConceded = "";
        }

        public TableRowGroup TopBombardiersOfTeam()
        {
            if (NameCityCheckTeamTextBox() && TopCheckTeamTextBox())
            {
                var team = _teamRepository.FindByNameAndCity(_viewModel.TeamName, _viewModel.TeamCity);

                if (team != null)
                {
                    InitializePlayerTable();
                    var matchesWherePlayersScored = from match in _matchRepository.GetMatches()
                                                    where match.Team1 == team || match.Team2 == team
                                                    select match.PlayersScored;

                    var topPlayers = matchesWherePlayersScored
                        .Where(match => match != null)
                        .SelectMany(match => match)
                        .ToList()
                        .Where(p => p.Team.Name == team.Name)
                        .GroupBy(p => p)
                        .OrderByDescending(p => p.Count())
                        .Select(p => p.Key)
                        .Take(int.Parse(_viewModel.TeamTop));

                    foreach (var player in topPlayers)
                    {
                        AddPlayerToTable(player);
                    }
                }
            }

            return _table;
        }

        public TableRowGroup TopBombardiersOfTournament()
        {
            if (TopCheckTeamTextBox())
            {
                InitializePlayerTable();
                var matchesWherePlayersScored = from match in _matchRepository.GetMatches()
                                                select match.PlayersScored;

                var topPlayers = matchesWherePlayersScored
                    .Where(match => match != null)
                    .SelectMany(match => match)
                    .ToList()
                    .GroupBy(p => p)
                    .OrderByDescending(p => p.Count())
                    .Select(p => p.Key)
                    .Take(int.Parse(_viewModel.TeamTop));

                foreach (var player in topPlayers)
                {
                    AddPlayerToTable(player);   
                }
            }

            return _table;
        }

        public TableRowGroup TopTeamsByMaxScored()
        {
            if (TopCheckTeamTextBox())
            {
                InitializeTeamTable();
                var topTeamsByScored = _teamRepository.GetTeams().ToList().OrderByDescending(t => t.ScoredGoalsAmount).Take(int.Parse(_viewModel.TeamTop));

                foreach (var team in topTeamsByScored)
                {
                    AddTeamToTable(team);
                }
            }

            return _table;
        }

        public TableRowGroup TopTeamsByMinConceded()
        {
            if (TopCheckTeamTextBox())
            {
                InitializeTeamTable();
                var topTeamsByScored = _teamRepository.GetTeams().ToList().OrderBy(t => t.ConcededGoalsAmount).Take(int.Parse(_viewModel.TeamTop));

                foreach (var team in topTeamsByScored)
                {
                    AddTeamToTable(team);
                }
            }

            return _table;
        }

        public TableRowGroup TopTeamsByMaxPoints()
        {
            if (TopCheckTeamTextBox())
            {
                InitializeTeamTable();
                var topTeamsByPoints = _teamRepository.GetTeams().ToList().OrderByDescending(t => GetPoints(t)).Take(int.Parse(_viewModel.TeamTop));

                foreach (var team in topTeamsByPoints)
                {
                    AddTeamToTable(team);
                }
            }

            return _table;
        }

        public TableRowGroup TopTeamsByMinPoints()
        {
            if (TopCheckTeamTextBox())
            {
                InitializeTeamTable();
                var topTeamsByPoints = _teamRepository.GetTeams().ToList().OrderBy(t => GetPoints(t)).Take(int.Parse(_viewModel.TeamTop));

                foreach (var team in topTeamsByPoints)
                {
                    AddTeamToTable(team);
                }
            }

            return _table;
        }
    }
}
