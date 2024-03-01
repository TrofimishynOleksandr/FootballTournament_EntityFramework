using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootballTournamentUI.DAL.Entities
{
    public class Player
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Country { get; set; }

        public int Number { get; set; }

        [MinLength(2), MaxLength(3)]
        public string Position { get; set; }

        public List<Match> Matches { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; }
    }
}
