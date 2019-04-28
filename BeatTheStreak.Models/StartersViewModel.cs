using BeatTheStreak.Helpers;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeatTheStreak.Models
{
	public class StartersViewModel
	{
		public DateTime GameDate { get; set; }

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public List<Pitcher> Pitchers { get; set; }

		public Dictionary<string,PlayerGameLogViewModel> GameLogs { get; set; }

		public StartersViewModel()
		{
			Pitchers = new List<Pitcher>();
			GameLogs = new Dictionary<string, PlayerGameLogViewModel>();
		}

		public List<string> Dump()
		{
			var reportWidth = 118;
			var lines = new List<string>();
			LineOfDashs(reportWidth, lines);
			AddLine(lines, $"STARTING PITCHERS  {GameDate.ToLongDateString()} US");
			LineOfDashs(reportWidth, lines);
			AddLine(lines, new String(' ', 71) + "IP   H  ER  BB   K  QS   W  L  SV   WHIP");
			var i = 0;
			List<Pitcher> pitchers = Pitchers
				.OrderBy(x => x.TeamName)
				.ToList();
			foreach (var pitcher in pitchers)
			{
				++i;
				AddLine(lines, $@"{i.ToString(),2} {
					PitcherLine(pitcher)
					}  {
					StatLine(pitcher.Slug, pitcher)
					}");
			}
			LineOfDashs(reportWidth, lines);
			return lines;
		}

		public List<string> DumpHotList()
		{
			var reportWidth = 121;
			var lines = new List<string>();
			LineOfDashs(reportWidth, lines);
			AddLine(lines, $"HOT FREE AGENT PITCHERS  {StartDate.ToLongDateString()} to {EndDate.ToLongDateString()} US");
			LineOfDashs(reportWidth, lines);
			AddLine(lines, new String(' ', 77) + "IP   H  ER  BB   K  QS   W  L  SV   WHIP");
			var i = 0;
			List<Pitcher> pitchers = Pitchers
				.OrderBy(x => x.Era)
				.ToList();
			foreach (var pitcher in pitchers)
			{
				++i;
				AddLine(lines, $@"{i.ToString(),2} {
					PitcherLine(pitcher)
					}  {DatePart(pitcher)}  {
					StatLine2(pitcher.Slug, pitcher)
					} {
					SlugPart(pitcher)
					}");
			}
			LineOfDashs(reportWidth, lines);
			return lines;
		}

		private string DatePart(Pitcher pitcher)
		{
			var gl = GameLogs[pitcher.Slug];
			return Utility.UniversalDate(gl.AsOf);
		}

		private string SlugPart(Pitcher pitcher )
		{
			if (GameLogs[pitcher.Slug].InningsPitched.Equals(0.0M))
				return pitcher.Slug;
			return string.Empty;
		}

		private void LineOfDashs(int reportWidth, List<string> lines)
		{
			AddLine(lines, new String('-', reportWidth));
		}

		private string StatLine(string slug, Pitcher p)
		{
			var gl = GameLogs[slug];
			return gl.PitcherEspnLine() + "  " + Star(gl,p);
		}

		private string StatLine2(string slug, Pitcher p)
		{
			var gl = GameLogs[slug];
			return gl.PitcherEspnLinePart2() + "  " + Star(gl, p);
		}

		private string Star(
			PlayerGameLogViewModel gl,
			Pitcher p)
		{
			var result = "  ";
			if (gl.EarnedRuns.Equals(0)
				&& p.FantasyTeam.Equals("FA"))
				result = "**";

			return result;
		}

		public void Add(Pitcher sp, PlayerGameLogViewModel gl)
		{
			Pitchers.Add(sp);
			GameLogs[sp.Slug] = gl;
		}

		public void Add(PlayerGameLogViewModel gl, string playerSlug)
		{
			GameLogs[playerSlug] = gl;
		}

		private void AddLine(List<string> lines, string line)
		{
			Console.WriteLine(line);
			lines.Add(line);
		}

		private string PitcherLine(Pitcher p)
		{
			return $@"{p.Name,-20} {
				p.Throws
				}  {
				p.TeamName,-15
				} {
				p.FantasyTeam,4
				} ({
				p.Wins,2
				}-{
				p.Losses,2
				}) {p.Era,5}"; 			
		}
	}
}
