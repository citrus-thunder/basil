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
			// Note: for Weedle, make it a 1 in 30 chance he says this before rolling the dice.
			return ReplyAsync(@"`Rolls around on the floor`");
		}
	}
}