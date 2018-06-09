using System;
using System.Collections.Generic;
using System.Net;

namespace BeatTheStreak.Repositories
{
    public class BaseApiRequest
    {
        public List<PlayerDto> Players { get; set; }
        public List<TeamDto> Teams { get; set; }
        public List<GameDto> Games { get; set; }

        public HttpWebRequest CreateRequest(
            string sport,
            string league,
            string apiRequest,
            string queryParms)
        {
            var apiKey = Environment.GetEnvironmentVariable("STATAPI");
            var url = $@"https://api.stattleship.com/{
                sport
                }/{
                league
                }/{
                apiRequest
                }?{
                queryParms
                }";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(
                requestUriString: url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "application/vnd.stattleship.com; version=1";
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add(
                "Authorization",
                apiKey);
			Console.WriteLine($"url:{url}");
            return httpWebRequest;
        }

        public HttpWebRequest CreateGameLogRequest(
            string queryParms)
        {
            var url = $@"https://api.stattleship.com/game_logs?{
                queryParms
                }";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(
                requestUriString: url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "application/vnd.stattleship.com; version=1";
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add(
                "Authorization",
                "35a160218fc36942348a14ddaec71d43");

            return httpWebRequest;
        }

        public HttpWebRequest CreateRankingsRequest(
            string queryParms)
        {
            var url = $@"https://api.stattleship.com/rankings?{
                queryParms
                }";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(
                requestUriString: url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "application/vnd.stattleship.com; version=1";
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add(
                "Authorization",
                "35a160218fc36942348a14ddaec71d43");

            return httpWebRequest;
        }

        public string GetName(string playerId, List<PlayerDto> players)
        {
            string name = "???";
            foreach (var p in players)
            {
                if (p.Id == playerId)
                {
                    name = $"{p.FirstName} {p.LastName}";
                    break;
                }
            }
            return name;
        }

		public string GetHandedness(string playerId, List<PlayerDto> players)
		{
			string handedness = "???";
			foreach (var p in players)
			{
				if (p.Id == playerId)
				{
					handedness = p.Handedness;
					if (handedness == "unknown")
						handedness = "?";
					if (handedness.Length > 0)
						handedness = handedness.Substring(0, 1).ToUpper();
					break;
				}
			}
			return handedness;
		}

		public string GetBats(string playerId, List<PlayerDto> players)
		{
			string bats = "?";
			foreach (var p in players)
			{
				if (p.Id == playerId)
				{
					bats = p.Bats;
					if (bats == "bats_right")
						bats = "R";
					else if (bats == "bats_left")
						bats = "L";
					else if (bats == "bats_switch")
						bats = "S";
					else 
						bats = "?";
					break;
				}
			}
			return bats;
		}

		public string GetPlayerSlug(string playerId, List<PlayerDto> players)
        {
            string name = "???";
            foreach (var p in players)
            {
                if (p.Id == playerId)
                {
                    name = $"{p.Slug}";
                    break;
                }
            }
            return name;
        }

        public string TeamFor(string teamId, List<TeamDto> teams)
        {
            string name = "???";
            foreach (var t in teams)
            {
                if (t.TeamId == teamId)
                {
                    name = $"{t.Name} {t.NickName}";
                    break;
                }
            }
            return name;
        }

        public string GameFor(string gameId, List<GameDto> games)
        {
            string name = "???";
            foreach (var g in games)
            {
                if (g.Id == gameId)
                {
                    name = $"{g.Name}";
                    break;
                }
            }
            return name;
        }

        public string TeamSlugFor(string teamId, List<TeamDto> teams)
        {
            var slug = string.Empty;
            foreach (var item in teams)
            {
                if (item.TeamId == teamId)
                {
                    slug = item.Slug;
                    break;
                }
            };
            return slug;
        }

        public string TeamNameFor(string teamId, List<TeamDto> teams)
        {
            var name = string.Empty;
            foreach (var item in teams)
            {
                if (item.TeamId == teamId)
                {
                    name = item.Name;
                    break;
                }
            };
            return name;
        }
    }
}
