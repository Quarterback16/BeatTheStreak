using System;

namespace BeatTheStreak.Tests
{
	internal static class TestHelper
	{
		internal static string FileName(
			string folder, 
			string report, 
			int week)
		{
			var path = @"w:\medialists\dropbox\MLB\2019\";
			return $@"{path}\{folder}\{report}-Week-{week:0#}.htm";
		}

		internal static string FileName(
			string folder,
			string report)     {
			var path = @"w:\medialists\dropbox\MLB\2019\";
			return $@"{path}\{folder}\{report}.htm";
		}
	}
}