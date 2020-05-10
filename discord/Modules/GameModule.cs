using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Basil.Discord.Services;

using CardSharp;
using RollSharp;

namespace Basil.Discord.Modules
{
	public class GameModule : ModuleBase<SocketCommandContext>
	{
		[Command("roll")]
		public Task RollAsync()
		{
			return ReplyAsync("Rolling!");
		}
	}
}