using FootballTournamentUI.DAL;
using FootballTournamentUI.DAL.Entities;
using FootballTournamentUI.DAL.Repositories;
using FootballTournamentUI.Providers;
using FootballTournamentUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace FootballTournamentUI.Services
{
    public class FootballTournamentService
    {
        private TableRowGroup _table;
        private TeamProvider _teamProvider;
        private MatchProvider _matchProvider;

        public FootballTournamentService(FootballTournamentContext context, TableRowGroup tableRowGroup, ViewModel viewModel)
        {
            _table = tableRowGroup;
            _teamProvider = new TeamProvider(context, _table, viewModel);
            _matchProvider = new MatchProvider(context, _table, viewModel);
        }

        public void FillDb()
        {
            using (var context = new FootballTournamentContext())
            {
                if (context.Teams.Count() == 0)
                {
                    List<Team> teams = new List<Team>()
                    {
                        new Team() { Name = "Team1", City = "City3", VictoriesAmount = 2, LossesAmount = 1, DrawsAmount = 1, ScoredGoalsAmount = 4, ConcededGoalsAmount = 2 },
                        new Team() { Name = "Team4", City = "City2", VictoriesAmount = 5, LossesAmount = 0, DrawsAmount = 2, ScoredGoalsAmount = 12, ConcededGoalsAmount = 3 },
                        new Team() { Name = "Team2", City = "City3", VictoriesAmount = 3, LossesAmount = 4, DrawsAmount = 4, ScoredGoalsAmount = 5, ConcededGoalsAmount = 7 },
                        new Team() { Name = "Team3", City = "City1", VictoriesAmount = 7, LossesAmount = 3, DrawsAmount = 2, ScoredGoalsAmount = 15, ConcededGoalsAmount = 5 }
                    };

                    context.Teams.AddRange(teams);
                    context.SaveChanges();
                }

                if (context.Players.Count() == 0)
                {
                    List<Player> players = new List<Player>()
                    {
                        new Player() { FullName = "Player2", Country = "Country3", Number = 7, Position = "ST", Team = context.Teams.ToList()[3]},
                        new Player() { FullName = "Player3", Country = "Country2", Number = 2, Position = "LW", Team = context.Teams.ToList()[2]},
                        new Player() { FullName = "Player5", Country = "Country3", Number = 17, Position = "RW", Team = context.Teams.ToList()[1]},
                        new Player() { FullName = "Player1", Country = "Country5", Number = 13, Position = "CT", Team = context.Teams.ToList()[0]},
                        new Player() { FullName = "Player4", Country = "Country1", Number = 21, Position = "GK", Team = context.Teams.ToList()[3]},
                        new Player() { FullName = "Player6", Country = "Country2", Number = 3, Position = "ST", Team = context.Teams.ToList()[2]},
                        new Player() { FullName = "Player8", Country = "Country4", Number = 1, Position = "GK", Team = context.Teams.ToList()[1]},
                        new Player() { FullName = "Player7", Country = "Country5", Number = 10, Position = "CT", Team = context.Teams.ToList()[0]}
                    };

                    context.Players.AddRange(players);
                    context.SaveChanges();
                }

                if (context.Matches.Count() == 0)
                {
                    List<Match> matches = new List<Match>()
                    {
                        new Match() {Team1 = context.Teams.ToList()[0], Team2 = context.Teams.ToList()[3], Team1Score = 3, Team2Score = 4, Date = new DateTime(2024, 1, 5),
                            PlayersScored = new List<Player>(){context.Players.ToList()[0], context.Players.ToList()[3], context.Players.ToList()[4], context.Players.ToList()[7] } },
                        new Match() {Team1 = context.Teams.ToList()[3], Team2 = context.Teams.ToList()[1], Team1Score = 4, Team2Score = 1, Date = new DateTime(2024, 1, 15),
                            PlayersScored = new List<Player>(){context.Players.ToList()[0], context.Players.ToList()[2], context.Players.ToList()[4] }},
                        new Match() {Team1 = context.Teams.ToList()[1], Team2 = context.Teams.ToList()[2], Team1Score = 1, Team2Score = 0, Date = new DateTime(2023, 12, 13),
                            PlayersScored = new List<Player>(){context.Players.ToList()[2] }},
                        new Match() {Team1 = context.Teams.ToList()[0], Team2 = context.Teams.ToList()[2], Team1Score = 2, Team2Score = 3, Date = new DateTime(2023, 12, 6),
                            PlayersScored = new List<Player>(){context.Players.ToList()[1], context.Players.ToList()[3], context.Players.ToList()[5], context.Players.ToList()[7] }}
                    };

                    context.Matches.AddRange(matches);
                    context.SaveChanges();
                }
            }
        }

        public TableRowGroup ShowTeams() => _teamProvider.ShowTeams();

        public TableRowGroup FindByName() => _teamProvider.FindByName();

        public TableRowGroup FindByCity() => _teamProvider.FindByCity();

        public TableRowGroup FindByNameAndCity() => _teamProvider.FindByNameAndCity();

        public TableRowGroup ShowTeamWithMaxVictories() => _teamProvider.ShowTeamWithMaxVictories();

        public TableRowGroup ShowTeamWithMaxLosses() => _teamProvider.ShowTeamWithMaxLosses();

        public TableRowGroup ShowTeamWithMaxDraws() => _teamProvider.ShowTeamWithMaxDraws();

        public TableRowGroup ShowTeamWithMaxScored() => _teamProvider.ShowTeamWithMaxScored();

        public TableRowGroup ShowTeamWithMaxConceded() => _teamProvider.ShowTeamWithMaxConceded();

        public void AddTeam() => _teamProvider.AddTeam();

        public void UpdateTeam() => _teamProvider.UpdateTeam();
        
        public void DeleteTeam() => _teamProvider.DeleteTeam();

        public TableRowGroup TopBombardiersOfTeam() => _teamProvider.TopBombardiersOfTeam();

        public TableRowGroup TopBombardiersOfTournament() => _teamProvider.TopBombardiersOfTournament();

        public TableRowGroup TopTeamsByMaxScored() => _teamProvider.TopTeamsByMaxScored();

        public TableRowGroup TopTeamsByMinConceded() => _teamProvider.TopTeamsByMinConceded();

        public TableRowGroup TopTeamsByMaxPoints() => _teamProvider.TopTeamsByMaxPoints();

        public TableRowGroup TopTeamsByMinPoints() => _teamProvider.TopTeamsByMinPoints();


        public TableRowGroup ShowMatches() => _matchProvider.ShowMatches();

        public TableRowGroup ShowMatchInfo() => _matchProvider.ShowMatchInfo();

        public TableRowGroup ShowMatchWithDate() => _matchProvider.ShowMatchWithDate();

        public TableRowGroup ShowMatchesOfTeam() => _matchProvider.ShowMatchesOfTeam();

        public TableRowGroup ShowPlayersScoredInDate() => _matchProvider.ShowPlayersScoredInDate();

        public void AddMatch() => _matchProvider.AddMatch();

        public void UpdateMatch() => _matchProvider.UpdateMatch();

        public void DeleteMatch() => _matchProvider.DeleteMatch();
    }
}
