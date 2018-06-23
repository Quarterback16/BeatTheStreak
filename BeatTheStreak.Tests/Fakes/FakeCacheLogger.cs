using Cache.Interfaces;
using System;

namespace BeatTheStreak.Tests.Fakes
{
	public class FakeCacheLogger : ILog
	{
		public void Debug(string message)
		{
			Console.WriteLine($"debug:{message}");
		}

		public void Error(string message)
		{
			Console.WriteLine($"error:{message}");
		}

		public void ErrorException(string message, Exception ex)
		{
			Console.WriteLine($"errorEx:{message} {ex.Message}");
		}

		public void Info(string message)
		{
			Console.WriteLine($"info:{message}");
		}

		public void Trace(string message)
		{
			Console.WriteLine($"trace:{message}");
		}

		public void Warning(string message)
		{
			Console.WriteLine($"warn:{message}");
		}
	}
}
