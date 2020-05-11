using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Discord;

namespace Basil.DiscordClient.Services
{
	public class PictureService
	{
		private readonly HttpClient _http;

		public PictureService(HttpClient http)
				=> _http = http;

		public async Task<Stream> GetCatPictureAsync()
		{
			var resp = await _http.GetAsync("https://cataas.com/cat");
			return await resp.Content.ReadAsStreamAsync();
		}
		/*
		public async Task<Stream> SetAvatar(ISelfUser self, string url)
		{
			var src = await _http.GetAsync(url);
			using (var img = await src.Content.ReadAsStreamAsync())
			{
				var 
			}

			await self.ModifyAsync(s => s.Avatar = src.Content.ReadAsStreamAsync());
		}
		*/
		public async Task<Stream> GetImage(string url)
		{
			var src = await _http.GetAsync(url);
			return await src.Content.ReadAsStreamAsync();
		}
	}
}