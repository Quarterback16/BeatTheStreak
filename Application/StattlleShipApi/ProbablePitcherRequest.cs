using Newtonsoft.Json;
using System.Collections.Generic;
using Domain;
using System.Net;
using System.IO;
using System.Diagnostics;
using System;
using Application.StattlleShipApi.Model;
using Application.StattlleShipApi;

namespace BeatTheStreak
{
    public class ProbablePitcherRequest : BaseApiRequest
    {
        public List<PlayerDto> Players { get; set; }

        public List<Pitcher> Submit(DateTime queryDate)
        {
            var result = new List<Pitcher>();
            var httpWebRequest = CreateRequest(
                url: "https://api.stattleship.com/baseball/mlb/probable_pitchers?season_id=mlb-2018&on=2018-04-26");
            var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var dto = JsonConvert.DeserializeObject<ProbablePitchersDto>(
                    streamReader.ReadToEnd());

                Players = dto.Players;

                Console.WriteLine(dto);
                Debug.Write(dto);
                foreach (var item in dto.Pitchers)
                {
                    result.Add(MapDtoToPitcher(item));
                };
            }
            return result;
        }

        private Pitcher MapDtoToPitcher(PitcherDto dto)
        {
            var pitcher = new Pitcher
            {
                Name = GetName(dto.PlayerId)
            };
            return pitcher;
        }

        private string GetName(string playerId)
        {
            string name = "???";
            foreach (var p in Players)
            {
                if (p.Id == playerId)
                {
                    name = $"{p.FirstName} {p.LastName}";
                    break;
                }
            }
            return name;
        }
    }
}
