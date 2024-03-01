using FootballTournamentUI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTournamentUI.DAL.Repositories
{
    public class MatchRepository
    {
        private readonly FootballTournamentContext _context;

        public MatchRepository(FootballTournamentContext context)
        {
            _context = context;
        }

        public IEnumerable<Match> MatchesWithDate(DateTime date)
        {
            return _context.Matches.Include(t => t.Team1).Include(t => t.Team2).Include(p => p.PlayersScored).Where(m => m.Date == date);
        }

        public IEnumerable<Match> GetMatches()
        {
            return _context.Matches.Include(t => t.Team1).Include(t => t.Team2).Include(p => p.PlayersScored);
        }

        public IEnumerable<Match> MatchesWithTeam(Team team)
        {
            return _context.Matches.Include(t => t.Team1).Include(t => t.Team2).Include(p => p.PlayersScored).Where(m => m.Team1 == team || m.Team2 == team);
        }

        public bool IsExists(Match match)
        {
            return _context.Matches.Include(t => t.Team1).Include(t => t.Team2)
                .Any(m => m.Team1.Name == match.Team1.Name && m.Team2.Name == match.Team2.Name && m.Date == match.Date);
        }

        public bool IsExistsWithId(Match match)
        {
            return _context.Matches.Include(t => t.Team1).Include(t => t.Team2)
                .Any(m => m.Team1Id == match.Team1Id && m.Team2Id == match.Team2Id && m.Date == match.Date);
        }

        public void AddMatch(Match match)
        {
            _context.Matches.Add(match);
            _context.SaveChanges();
        }

        public void UpdateMatch(Match match)
        {
            _context.Matches.Update(match);
            _context.SaveChanges();
        }

        public void DeleteMatch(Match match)
        {
            _context.Matches.Remove(match);
            _context.SaveChanges();
        }

        public Match FindMatch(Match match)
        {
            return _context.Matches.Include(t => t.Team1).Include(t => t.Team2)
                .FirstOrDefault(m => m.Team1.Name == match.Team1.Name && m.Team2.Name == match.Team2.Name && m.Date == match.Date);
        }
    }
}
