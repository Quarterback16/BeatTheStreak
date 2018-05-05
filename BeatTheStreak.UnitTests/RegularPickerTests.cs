using Application.Outputs;
using Application.Pickers;
using Application.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BeatTheStreak.UnitTests.Fakes;
using System;
using System.Collections.Generic;
using Domain;

namespace BeatTheStreak.UnitTests
{
    [TestClass]
    public class RegularPickerTests
    {
        private RegularPicker _sut;
        private Mock<ILineupRepository> _mockLineupRepository;

        [TestInitialize]
        public void Setup()
        {
            _mockLineupRepository = new Mock<ILineupRepository>();
            _sut = new RegularPicker(_mockLineupRepository.Object);
        }

        [TestMethod]
        public void RegularPicker_WhenBatterHasNotThreeApps_Rejects()
        {
            _mockLineupRepository
                .Setup(x => x.Submit(It.IsAny<DateTime>(), It.IsAny<string>()))
                .Returns(new LineupViewModel {
                    Lineup = new List<Batter>()
                });
            string reasonForDislike = string.Empty;
            var result = _sut.Likes(
                new Selection
                {
                    GameDate = new System.DateTime( 2018, 5, 1 ),
                    Batter = new FakeBatter()
                },
                out reasonForDislike);
            Assert.IsFalse(
                result, 
                "Picker should return false as batter has not appeared in last 3 games");
        }

        [TestMethod]
        public void RegularPicker_WhenBatterHasThreeApps_Accepts()
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
                    Batter = new FakeBatter()
                },
                out reasonForDislike);
            Assert.IsTrue(
                result,
                "Picker should return true as batter has appeared in last 3 days");
        }
    }
}
