using Newtonsoft.Json;

namespace Application.StattlleShipApi.Model
{
    public class PlayerDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("active")]
        public string Active { get; set; }

        [JsonProperty("bats")]
        public string Bats { get; set; }

        [JsonProperty("birth_date")]
        public string BirthDate { get; set; }

        [JsonProperty("captain")]
        public string Captain { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("draft_overall_pick")]
        public string DraftOverallPick { get; set; }

        [JsonProperty("draft_round")]
        public string DraftRound { get; set; }

        [JsonProperty("draft_season")]
        public string DraftSeason { get; set; }

        [JsonProperty("draft_team_name")]
        public string DraftTeamName { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("handedness")]
        public string Handedness { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("high_school")]
        public string HighSchool { get; set; }

        [JsonProperty("humanized_salary")]
        public string HumanizedSalary { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("mlbam_id")]
        public string MlbAmId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("position_abbreviation")]
        public string PositionAbbreviation { get; set; }

        [JsonProperty("position_name")]
        public string PositionName { get; set; }

        [JsonProperty("pro_debut")]
        public string ProDebut { get; set; }

        [JsonProperty("salary")]
        public string Salary { get; set; }

        [JsonProperty("salary_currency")]
        public string SalaryCurrency { get; set; }

        [JsonProperty("school")]
        public string School { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("sport")]
        public string Sport { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("uniform_number")]
        public string UniformNumber { get; set; }

        [JsonProperty("unit_of_height")]
        public string UnitOfHeight { get; set; }

        [JsonProperty("unit_of_weight")]
        public string UnitOfWeight { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("years_of_experience")]
        public string YearsOfExperience { get; set; }

        [JsonProperty("league_id")]
        public string Leagueid { get; set; }

        [JsonProperty("playing_position_id")]
        public string PlayingPositionId { get; set; }

        [JsonProperty("team_id")]
        public string TeamId { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} {PositionAbbreviation}";
        }
    }
}
