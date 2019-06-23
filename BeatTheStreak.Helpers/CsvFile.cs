using System;
using System.IO;
using System.Text;

namespace BeatTheStreak.Helpers
{
	public class CsvFile
	{
		private const string K_Delimiter = ",";

		public CsvFile(string baseFolder)
		{
			BaseFolder = baseFolder;
			Header = "GameDate,PlayerSlug,Hits,AtBats,Runs,HomeRuns,TotalBases,RunsBattedIn,Walks,StrikeOuts,NetSteals";
		}

		public bool StartNew { get; set; }

		public string FilePath { get; set; }

		public string Header { get; set; }

		public string BaseFolder { get; set; }

		public Result WriteHeader()
		{
			try
			{
				File.WriteAllText(
					path: FileName(),
					contents: Header + Environment.NewLine);
				return Result.Ok();
			}
			catch (Exception ex)
			{
				return Result.Fail(ex.Message);
			}
		}

		public Result Exists(string csvName)
		{
			FilePath = csvName;
			Result result;
			if (File.Exists(FileName()))
				result = Result.Ok();
			else
				result = Result.Fail(
					message: $"File {FileName()} does not exist");
			return result;
		}

		private string FileName()
		{
			if (string.IsNullOrEmpty(BaseFolder))
				BaseFolder = @"d:\temp\csv";
			return $"{BaseFolder}{FilePath}.csv";
		}

		public Result ClearFile()
		{
			try
			{
				File.Delete(FileName());
				return Result.Ok();
			}
			catch (Exception ex)
			{
				return Result.Fail(ex.Message);
			}
		}

		public void AppendLine(string line)
		{
			File.AppendAllText(
				path: FilePath,
				contents: line);
		}

		public Result AppendLine(string[] columns)
		{
			try
			{
				var line = CommaDelimit(columns);
				File.AppendAllText(
					path: FileName(),
					contents: line + Environment.NewLine);
				return Result.Ok();
			}
			catch (Exception ex)
			{
				return Result.Fail(ex.Message);
			}
		}

		private string CommaDelimit(string[] columns)
		{
			var sb = new StringBuilder();
			foreach (var item in columns)
			{
				sb.Append(item);
				sb.Append(K_Delimiter);
			}
			return sb.ToString();
		}

		public Result Create()
		{
			if (string.IsNullOrEmpty(Header))
				return Result.Fail("No Header defined");
			if (StartNew)
			{
				ClearFile();
			}
			try
			{
				Utility.EnsureDirectory(BaseFolder);
				WriteHeader();
				return Result.Ok();
			}
			catch (Exception ex)
			{
				return Result.Fail(ex.Message);
			}
		}

		public Result Exists()
		{
			return Exists(FilePath);
		}
	}
}
