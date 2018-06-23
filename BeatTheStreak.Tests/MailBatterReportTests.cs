using BeatTheStreak.Implementations;
using BeatTheStreak.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Domain;
using BeatTheStreak.Tests.Fakes;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class MailBatterReportTests
	{
		private MailBatterReport sut;

		[TestInitialize]
		public void Setup()
		{
			var configReader = new ConfigReader();
			var mm = new MailMan2(configReader, new FakeLogger());
			sut = new MailBatterReport(mm, logger: null);
		}

		[TestMethod]
		public void TestSendBatterReportMail()
		{
			var batterReport = new BatterReport(
				new System.DateTime(2018, 06, 23))
			{
				Selections = new List<Selection>
				{
					new Selection
					{
						Batter = new Batter
						{
							Name = "Jacob Stallings"
						},
						Pitcher = new Pitcher
						{
							Name = "Carlos Santana"
						}
					}
				}
			};
			sut.MailReport(batterReport);
		}
	}
}
