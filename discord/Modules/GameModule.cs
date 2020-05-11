using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Discord;
using Discord.Commands;
using Basil;
using Basil.DiscordClient.Services;

//using CardSharp;
//using RollSharp;

namespace Basil.DiscordClient.Modules
{
	public class GameModule : ModuleBase<SocketCommandContext>
	{
		[Command("roll")]
		public async Task RollAsync([Remainder] string text)
		{

			var validate = @"[0-9]{1,2}d(f|[0-9]{1,2})*\s*([+-]\s*[0-9]{1,2})*";

			var isValid = Regex.Match(text, validate);

			if (!isValid.Success)
			{
				await ReplyAsync(@"That doesn't look quite right.");
				return;
			}

			if (DateTime.Now.TimeOfDay.Seconds % 30 == 0)
			{
				await ReplyAsync(@"`Rolls around on the floor`");
			}

			var args = Regex.Split(text, @"([df\+-])");

			int.TryParse(args[0].Trim(), out int count);
			bool fateDie = args[2].Trim() == "f";
			int dieSides = 6;

			if (!fateDie)
			{
				int.TryParse(args[2].Trim(), out dieSides);
			}

			bool modsExist = args.Length > 3;

			bool addMod = false;

			int modValue = 0;

			string modString = "";

			if (modsExist)
			{
				int.TryParse(args[4].Trim(), out modValue);
				addMod = args[3].Trim() == "+";
				modString = $" {args[3].Trim()} {modValue}";
			}

			int modType = 0;

			if (modsExist)
			{
				if (addMod)
				{
					modType = 1;
				}
				else
				{
					modType = -1;
				}
			}

			string typeString = fateDie ? "f" : $"{dieSides}";

			//await ReplyAsync($"Rolling {count}d{typeString}{modString}");

			await ReplyAsync(Bot.GetRoll(count, dieSides, modType, modValue));
		}
	}
}