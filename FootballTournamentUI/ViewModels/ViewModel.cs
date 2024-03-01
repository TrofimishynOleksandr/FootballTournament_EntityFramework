using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTournamentUI.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private List<string> _teamShowItems = new List<string>() { "All", "With max victories",
        "With max losses", "With max draws", "With max scored", "With max conceded",
        "Top with max scored", "Top with min conceded", "Top with max points", "Top with min points"};
        public List<string> TeamShowItems
        {
            get { return _teamShowItems; }
        }

        private List<string> _teamFindItems = new List<string>() { "By name", "By city",
        "By name and city"};
        public List<string> TeamFindItems
        {
            get { return _teamFindItems; }
        }

        private List<string> _footballEditItems = new List<string>() { "Add", "Update",
        "Delete"};
        public List<string> FootballEditItems
        {
            get { return _footballEditItems; }
        }

        private List<string> _matchShowItems = new List<string>() { "All", "Match info",
        "Match in current date", "Matches of team"};
        public List<string> MatchShowItems
        {
            get { return _matchShowItems; }
        }

        private string _teamName = "";
        public string TeamName
        {
            get { return _teamName; }
            set
            {
                _teamName = value;
                OnPropertyChanged(nameof(TeamName));
            }
        }

        private string _teamCity = "";
        public string TeamCity
        {
            get { return _teamCity; }
            set
            {
                _teamCity = value;
                OnPropertyChanged(nameof(TeamCity));
            }
        }

        private string _teamVictories = "";
        public string TeamVictories
        {
            get { return _teamVictories;  }
            set
            {
                _teamVictories = value;
                OnPropertyChanged(nameof(TeamVictories));
            }
        }

        private string _teamLosses = "";
        public string TeamLosses
        {
            get { return _teamLosses; }
            set
            {
                _teamLosses = value;
                OnPropertyChanged(nameof(TeamLosses));
            }
        }

        private string _teamDraws = "";
        public string TeamDraws
        {
            get { return _teamDraws; }
            set
            {
                _teamDraws = value;
                OnPropertyChanged(nameof(TeamDraws));
            }
        }

        private string _teamScored = "";
        public string TeamScored
        {
            get { return _teamScored; }
            set
            {
                _teamScored = value;
                OnPropertyChanged(nameof(TeamScored));
            }
        }

        private string _teamConceded = "";
        public string TeamConceded
        {
            get { return _teamConceded; }
            set
            {
                _teamConceded = value;
                OnPropertyChanged(nameof(TeamConceded));
            }
        }

        private string _teamTop = "";
        public string TeamTop
        {
            get { return _teamTop; }
            set
            {
                _teamTop = value;
                OnPropertyChanged(nameof(TeamTop));
            }
        }

        private string _teamInfo = "";
        public string TeamInfo
        {
            get { return _teamInfo; }
            set
            {
                _teamInfo = value;
                OnPropertyChanged(nameof(TeamInfo));
            }
        }

        private string _matchTeamOneName = "";
        public string MatchTeamOneName
        {
            get { return _matchTeamOneName; }
            set
            {
                _matchTeamOneName = value;
                OnPropertyChanged(nameof(MatchTeamOneName));
            }
        }

        private string _matchTeamTwoName = "";
        public string MatchTeamTwoName
        {
            get { return _matchTeamTwoName; }
            set
            {
                _matchTeamTwoName = value;
                OnPropertyChanged(nameof(MatchTeamTwoName));
            }
        }

        private string _matchTeamOneScored = "";
        public string MatchTeamOneScored
        {
            get { return _matchTeamOneScored; }
            set
            {
                _matchTeamOneScored = value;
                OnPropertyChanged(nameof(MatchTeamOneScored));
            }
        }

        private string _matchTeamTwoScored = "";
        public string MatchTeamTwoScored
        {
            get { return _matchTeamTwoScored; }
            set
            {
                _matchTeamTwoScored = value;
                OnPropertyChanged(nameof(MatchTeamTwoScored));
            }
        }

        private string _matchDate = "";
        public string MatchDate
        {
            get { return _matchDate; }
            set
            {
                _matchDate = value;
                OnPropertyChanged(nameof(MatchDate));
            }
        }

        private string _matchTeamNameToFind = "";
        public string MatchTeamNameToFind
        {
            get { return _matchTeamNameToFind; }
            set
            {
                _matchTeamNameToFind = value;
                OnPropertyChanged(nameof(MatchTeamNameToFind));
            }
        }

        private string _matchTeamCityToFind = "";
        public string MatchTeamCityToFind
        {
            get { return _matchTeamCityToFind; }
            set
            {
                _matchTeamCityToFind = value;
                OnPropertyChanged(nameof(MatchTeamCityToFind));
            }
        }

        private string _matchInfo = "";
        public string MatchInfo
        {
            get { return _matchInfo; }
            set
            {
                _matchInfo = value;
                OnPropertyChanged(nameof(MatchInfo));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
