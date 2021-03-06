﻿using BasketballRosterNew.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BasketballRosterNew.ViewModel
{
    class RosterViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<PlayerViewModel> Starters { get; private set; }
        public ObservableCollection<PlayerViewModel> Bench { get; private set; }

        private Roster _roster;

        private string _teamName;
        public string TeamName
        {
            get { return _teamName; }
            set
            {
                _teamName = value;
                OnPropertyChanged("TeamName");
            }
        }

        public RosterViewModel(Roster roster)
        {
            _roster = roster;

            Starters = new ObservableCollection<PlayerViewModel>();
            Bench = new ObservableCollection<PlayerViewModel>();

            TeamName = _roster.TeamName;

            UpdateRoster();
        }

        private void UpdateRoster()
        {
            var startingPlayers = _roster.Players
                .Where(player => player.Starter == true);

            Starters.Clear();

            foreach (Player player in startingPlayers)
                Starters.Add(new PlayerViewModel(player.Name, player.Number));

            var benchPlayers = _roster.Players
                .Where(player => player.Starter == false);

            Bench.Clear();

            foreach (Player player in benchPlayers)
                Bench.Add(new PlayerViewModel(player.Name, player.Number));

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
