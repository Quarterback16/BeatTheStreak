using Application.Outputs;
using Application.StattlleShipApi;
using BeatTheStreak;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class PickBatters
    {
        public BatterReport Choose( DateTime gameDate, int numberRequired )
        {
            var report = new BatterReport(gameDate)
            {
                Selections = SelectBatters(gameDate, numberRequired)
            };
            return report;
        }

        private List<Selection> SelectBatters(DateTime gameDate, int numberRequired)
        {
            var batters = new List<Selection>();
            //  Get list of pitchers first
            var request = new ProbablePitcherRequest();
            var lineupRequest = new LineupRequest();
            var pitchers = request.Submit(gameDate);
            foreach (var pitcher in pitchers)
            {
                var opponentTeam = pitcher.OpponentSlug;
                var result = lineupRequest.Submit(
                    queryDate: gameDate.AddDays(-1),
                    teamId: opponentTeam);
                var batter = result.FirstOrDefault();
                if ( batter != null)
                {
                    var selection = new Selection
                    {
                        Batter = batter,
                        Pitcher = pitcher,
                        Game = new Game
                        {
                            Title = pitcher.NextOpponent
                        }
                    };
                    batters.Add(selection);
                }
                if (batters.Count == numberRequired)
                    break;
            }
            return batters;
        }
    }
}
