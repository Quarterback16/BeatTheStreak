using Application.Outputs;
using Application.Pickers;
using Application.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
            var selection = new Selection();
            var result = _sut.Likes(
                new Selection
                {
                    GameDate = new System.DateTime( 2018, 5, 1 )
                } 
                );
            Assert.IsTrue(result, "batter has not appeared in last 3 games");
        }
    }
}
