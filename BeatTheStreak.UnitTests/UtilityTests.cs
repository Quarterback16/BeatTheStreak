using BeatTheStreak.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeatTheStreak.UnitTests
{
	[TestClass]
	public class UtilityTests
	{
		[TestMethod]
		public void Fip_Calculation_Works()
		{
			var result = Utility.FIP(
				homeRunsAllowed: 0.0M,
				strikeOuts: 5.0M,
				walksAllowed: 1.0M,
				battersHitByPitch: 0.0M,
				inningsPitched: 9.0M);
			Assert.AreEqual(2.38M, result);
		}

		[TestMethod]
		public void FixIp_WithWholeInnings_Works()
		{
			var result = Utility.FixIp(9.0M);
			Assert.AreEqual(9.0M, result);
		}

		[TestMethod]
		public void FixIp_WithOneThirdInnings_Works()
		{
			var result = Utility.FixIp(6.1M);
			Assert.AreEqual(6.333M, result);
		}

		[TestMethod]
		public void FixIp_WithTwoThirdInnings_Works()
		{
			var result = Utility.FixIp(5.2M);
			Assert.AreEqual(5.666M, result);
		}
	}
}
