using Newtonsoft.Json;
using System.Collections.Generic;

namespace BeatTheStreak.Repositories
{
    public class GameDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("at_neutral_site")]
        public string NeutralSite { get; set; }

        [JsonProperty("attendance")]
        public string Attendance { get; set; }

        [JsonProperty("away_team_outcome")]
        public string AwayTeamOutcome { get; set; }

        [JsonProperty("away_team_score")]
        public string AwayTeamScore { get; set; }

        [JsonProperty("broadcast")]
        public string Broadcast { get; set; }

        [JsonProperty("clock")]
        public string Clock { get; set; }

        [JsonProperty("clock_secs")]
        public string ClockSecs { get; set; }

        [JsonProperty("daytime")]
        public string Daytime { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("ended_at")]
        public string EndedAt { get; set; }

        [JsonProperty("home_team_outcome")]
        public string HomeTeamOutcome { get; set; }

        [JsonProperty("home_team_score")]
        public string HomeTeamScore { get; set; }

        [JsonProperty("humidity")]
        public string Humidity { get; set; }

        [JsonProperty("interval")]
        public string Interval { get; set; }

        [JsonProperty("interval_type")]
        public string xIntervalType { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("on")]
        public string On { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("period_label")]
        public string PeriodLabel { get; set; }

        [JsonProperty("score")]
        public string Score { get; set; }

        [JsonProperty("score_differential")]
        public string ScoreDifferential { get; set; }

        [JsonProperty("scoreline")]
        public string ScoreLine { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("started_at")]
        public string StartedAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("internet_coverage")]
        public string InternetCoverage { get; set; }

        [JsonProperty("satellite_coverage")]
        public string SatelliteCoverage { get; set; }

        [JsonProperty("television_coverage")]
        public string TelevisionCoverage { get; set; }

        [JsonProperty("temperature")]
        public string Temperature { get; set; }

        [JsonProperty("temperature_unit")]
        public string TemperatureUnit { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("weather_conditions")]
        public string WeatherConditions { get; set; }

        [JsonProperty("wind_direction")]
        public string WindDirection { get; set; }

        [JsonProperty("wind_speed")]
        public string WindSpeed { get; set; }

        [JsonProperty("wind_speed_unit")]
        public string WindSpeedUnit { get; set; }

        [JsonProperty("home_team_id")]
        public string HomeTeamId { get; set; }

        [JsonProperty("away_team_id")]
        public string AwayTeamId { get; set; }

        [JsonProperty("winning_team_id")]
        public string WinningTeamId { get; set; }

        [JsonProperty("season_id")]
        public string SeasonId { get; set; }

        [JsonProperty("venue_id")]
        public string VenueId { get; set; }

        [JsonProperty("official_ids")]
        public List<string> OfficalIds { get; set; }

    }
}
