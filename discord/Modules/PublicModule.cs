using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Basil.DiscordClient.Services;

namespace Basil.DiscordClient.Modules
{
	// Modules must be public and inherit from an IModuleBase
	public class PublicModule : ModuleBase<SocketCommandContext>
	{
		//private readonly DiscordSocketClient _discord;

		public static string[] Greetings =
		{
			@"Hewo ◕‿◕",
			@"Oh... h-hi",
			@"`purrs`",
			@"`hides`",
			@":dragon:",
			@"`gently nibbles your shoe`",
			@"`mrrps, then falls back asleep`"
		};

		// Dependency Injection will fill this value in for us
		public PictureService PictureService { get; set; }

		[Command("hello")]
		[Alias("hi", "hey")]
		public async Task GreetAsync()
		{
			var rand = new Random();

			await ReplyAsync(Greetings[rand.Next(0, Greetings.Length)]);
		}

		[Command("ping")]
		[Alias("pong")]
		public Task PingAsync()
				=> ReplyAsync("pong!");

		[Command("cat")]
		public async Task CatAsync()
		{
			// Get a stream containing an image of a cat
			var stream = await PictureService.GetCatPictureAsync();
			// Streams must be seeked to their beginning before being uploaded!
			stream.Seek(0, SeekOrigin.Begin);
			await Context.Channel.SendFileAsync(stream, "cat.png");
		}

		// Get info on a user, or the user who invoked the command if one is not specified
		[Command("userinfo")]
		public async Task UserInfoAsync(IUser user = null)
		{
			user = user ?? Context.User;

			await ReplyAsync(user.ToString());
		}

		[Command("makeover")]
		[RequireContext(ContextType.Guild)]
		[RequireUserPermission(GuildPermission.Administrator)]
		public async Task SetAvatar([Remainder] string text)
		{
			try
			{
				var stream = await PictureService.GetImage(text);

				stream.Seek(0, SeekOrigin.Begin);

				await Context.Client.CurrentUser.ModifyAsync(s => s.Avatar = new Optional<Discord.Image?>(new Discord.Image(stream)));
				await ReplyAsync("How do I look?");
			}
			catch (Exception ex)
			{
				await ReplyAsync($"Oops, I didn't find anything there. I found this instead: \n {ex.GetType()} {ex.Message}");
			}
		}

		// [Remainder] takes the rest of the command's arguments as one argument, rather than splitting every space
		[Command("echo")]
		[Alias("say")]
		//[RequireUserPermission(GuildPermission.ManageMessages)]
		public async Task EchoAsync([Remainder] string text)
		{
			// Insert a ZWSP before the text to prevent triggering other bots!
			//await (Context.Channel as ITextChannel).DeleteMessageAsync(Context.Message);
			//var message = await Context.Channel.GetMessagesAsync(1).FlattenAsync();
			//await ((ITextChannel) Context.Channel).DeleteMessagesAsync(message);
			await Context.Channel.DeleteMessageAsync(Context.Message.Id);
			await ReplyAsync('\u200B' + text);
		}


		// 'params' will parse space-separated elements into a list
		[Command("list")]
		public Task ListAsync(params string[] objects)
				=> ReplyAsync("You listed: " + string.Join("; ", objects));

		// Setting a custom ErrorMessage property will help clarify the precondition error
		[Command("guild_only")]
		[RequireContext(ContextType.Guild, ErrorMessage = "Sorry, this command must be ran from within a server, not a DM!")]
		public Task GuildOnlyCommand()
				=> ReplyAsync("Nothing to see here!");
	}
}