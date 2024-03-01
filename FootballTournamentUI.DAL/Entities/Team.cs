using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootballTournamentUI.DAL.Entities
{
    public class Team
    {
        public int Id { get; set; }

        [Column("TeamName"), MaxLength(50, ErrorMessage = "TeamName must be 10 characters or less"), MinLength(2)]
        public string Name { get; set; }

        [Column("TeamCity"), MinLength(3), MaxLength(40)]
        public string City { get; set; }

        public int VictoriesAmount { get; set; }

        public int LossesAmount { get; set; }
        public int DrawsAmount { get; set; }

        public int ScoredGoalsAmount { get; set; }

        public int ConcededGoalsAmount { get; set; }

        public List<Player> Players { get; set; }

        public List<Match>? Matches { get; set; }
    }
}
