using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BeatTheStreak.UnitTests.Fakes;
using System;
using System.Collections.Generic;
using Domain;
using Application;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;

namespace BeatTheStreak.UnitTests
{
    [TestClass]
    public class DefaultPickerTests
    {
        private DefaultPicker _sut;
        private Mock<ILineupRepository> _mockLineupRepository;
        private Mock<IPitcherRepository> _mockPitcherRepository;

        [TestInitialize]
        public void Setup()
        {
            _mockLineupRepository = new Mock<ILineupRepository>();
            _mockPitcherRepository = new Mock<IPitcherRepository>();
            var options = new Dictionary<string, string>
            {
                { Constants.Options.HomePitchersOnly, "on" },
                { Constants.Options.NoDaysOff, "on" },
                { Constants.Options.DaysOffDaysBack, "3" },
            };
            _sut = new DefaultPicker(
                options,
                _mockLineupRepository.Object,
                _mockPitcherRepository.Object);
        }

        [TestMethod]
        public void DefaultPicker_WhenBatterHasNotThreeApps_Rejects()
        {
            _mockLineupRepository
                .Setup(x => x.Submit(It.IsAny<DateTime>(), It.IsAny<string>()))
                .Returns(new LineupViewModel {
                    Lineup = new List<Batter>()
                });  // all lineup request will say no appearance
            string reasonForDislike = string.Empty;
            var result = _sut.Likes(
                new Selection
                {
                    GameDate = new DateTime( 2018, 5, 1 ),
                    Batter = new FakeBatter(),
                    Batter1 = new FakeBatter(.300M),
                    Batter2 = new FakeBatter(.250M),
                    Batter3 = new FakeBatter(.275M)
                },
                out reasonForDislike);
            Assert.IsFalse(
                result, 
                "Picker should return false as batter has not appeared in last 3 games");
        }

        [TestMethod]
        public void DefaultPicker_WhenBatterHasThreeApps_Accepts()
        {
            _mockLineupRepository
                .Setup(x => x.Submit(new DateTime(2018, 4, 30), It.IsAny<string>()))
                .Returns(new LineupViewModel
                {
                    Lineup = new List<Batter>
                    {
                        new FakeBatter()
                    }
                });
            _mockLineupRepository
                .Setup(x => x.Submit(new DateTime(2018, 4, 29), It.IsAny<string>()))
                .Returns(new LineupViewModel
                {
                    Lineup = new List<Batter>
                    {
                        new FakeBatter()
                    }
                });
            _mockLineupRepository
                .Setup(x => x.Submit(new DateTime(2018, 4, 28), It.IsAny<string>()))
                .Returns(new LineupViewModel
                {
                    Lineup = new List<Batter>
                    {
                        new FakeBatter()
                    }
                });
            string reasonForDislike = string.Empty;
            var result = _sut.Likes(
                new Selection
                {
                    GameDate = new DateTime(2018, 5, 1),
                    Batter = new FakeBatter(),
                    Batter1 = new FakeBatter(.300M),
                    Batter2 = new FakeBatter(.250M),
                    Batter3 = new FakeBatter(.275M)
                },
                out reasonForDislike);
            Assert.IsTrue(
                result,
                "Picker should return true as batter has appeared in last 3 days");
        }
    }
}
