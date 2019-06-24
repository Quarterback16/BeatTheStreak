using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeatTheStreak.Helpers;

namespace BeatTheStreak.UnitTests
{
	[TestClass]
	public class MatplotlibTests
	{
		[TestMethod]
		public void SimplePlotTest()
		{
			int[] weeks = { 6, 7, 8, 9, 10, 11, 12 };
			decimal[] points =
			{
				95.5M,
				92.5M,
				91.0M,
				90.5M,
				84.5M,
				90.5M,
				92.0M
			};
			MatPlotLib.Plot(
				);
		}
	}
}
