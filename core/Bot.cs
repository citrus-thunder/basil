using System;
using System.Text;

namespace Basil
{
	public static class Bot
	{
		public static string GetRoll(int dieCount, int dieType, int modType = 0, int mod = 0)
		{
			var rand = new Random();
			var modString = "";

			switch (modType)
			{
				default:
				case 0:
					break;
				case 1:
					modString = $" + {mod}";
					break;
				case -1:
					modString = $" - {mod}";
					break;
			}

			int total = 0;

			StringBuilder builder = new StringBuilder();

			for (var i = 0; i < dieCount; i++)
			{
				var roll = rand.Next(0, dieType + 1);
				builder.Append($"[{roll}]");
				total += roll;
			}

			if (modType != 0)
			{
				total += modType > 0 ? mod : -mod;
			}

			return $"{dieCount}d{dieType}{modString}: {builder.ToString()} = {total}";
		}
	}
}
