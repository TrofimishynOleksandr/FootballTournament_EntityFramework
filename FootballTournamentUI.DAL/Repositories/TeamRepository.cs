using FootballTournamentUI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTournamentUI.DAL.Repositories
{
    public class TeamRepository
    {
        private FootballTournamentContext _context;

        public TeamRepository(FootballTournamentContext context)
        {
            _context = context;
        }

        public IEnumerable<Team> GetTeams()
        {
            return _context.Teams.ToList();
        }

        public IEnumerable<Team> FindByName(string name)
        {
            return _context.Teams.Where(t => t.Name == name);
        }

        public Team FindByOnlyName(string name)
        {
            return _context.Teams.FirstOrDefault(t => t.Name == name);
        }

        public IEnumerable<Team> FindByCity(string city)
        {
            return _context.Teams.Where(t => t.City == city);
        }

        public Team FindByNameAndCity(string name, string city)
        {
            return _context.Teams.FirstOrDefault(t => t.Name == name && t.City == city);
        }

        public Team TeamWithMaxVictories()
        {
            int maxVictories = _context.Teams.Max(t => t.VictoriesAmount);
            return _context.Teams.FirstOrDefault(t => t.VictoriesAmount == maxVictories);
        }

        public Team TeamWithMaxLosses()
        {
            int maxLosses = _context.Teams.Max(t => t.LossesAmount);
            return _context.Teams.FirstOrDefault(t => t.LossesAmount == maxLosses);
        }

        public Team TeamWithMaxDraws()
        {
            int maxDraws = _context.Teams.Max(t => t.DrawsAmount);
            return _context.Teams.FirstOrDefault(t => t.DrawsAmount == maxDraws);
        }

        public Team TeamWithMaxScored()
        {
            int maxScored = _context.Teams.Max(t => t.ScoredGoalsAmount);
            return _context.Teams.FirstOrDefault(t => t.ScoredGoalsAmount == maxScored);
        }

        public Team TeamWithMaxConceded()
        {
            int maxConceded = _context.Teams.Max(t => t.ConcededGoalsAmount);
            return _context.Teams.FirstOrDefault(t => t.ConcededGoalsAmount == maxConceded);
        }

        public void AddTeam(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
        }

        public bool IsExists(Team team)
        {
            return _context.Teams.Any(t => t.Name == team.Name && t.City == team.City);
        }

        public bool IsExistsWithId(int id)
        {
            return _context.Teams.Any(t => t.Id == id);
        }

        public void UpdateTeam(Team team)
        {
            _context.Teams.Update(team);
            _context.SaveChanges();
        }

        public void DeleteTeam(Team team)
        {
            _context.Teams.Remove(team);
            _context.SaveChanges();
        }
    }
}
