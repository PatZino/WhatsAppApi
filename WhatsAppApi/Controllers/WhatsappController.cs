using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WhatsappController : ControllerBase
	{
		// https://www.waboxapp.com/assets/doc/waboxapp-API-v3.pdf
		// https://www.waboxapp.com/terms
		// https://www.waboxapp.com/pricing

		private static readonly HttpClient client = new HttpClient();
		private readonly string apiKey = "your_api_key";
		private readonly string apiUrl = "https://api.waboxapp.com/api/send/chat";

		// GET: api/<WhatsappController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<WhatsappController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<WhatsappController>
		[HttpPost]
		public async Task<IActionResult> SendMessage(string to, string text)
		{
			var message = new
			{
				token = apiKey,
				uid = to,
				to = to,
				custom_uid = Guid.NewGuid().ToString(),
				text = text
			};

			var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

			var response = await client.PostAsync(apiUrl, content);

			if (response.IsSuccessStatusCode)
			{
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		// PUT api/<WhatsappController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<WhatsappController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
