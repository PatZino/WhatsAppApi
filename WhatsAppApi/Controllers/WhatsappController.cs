using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using WhatsAppApi.Models;

namespace WhatsAppApi.Controllers
{

    /**
     * 	https://www.waboxapp.com/assets/doc/waboxapp-API-v3.pdf
     * 	https://www.waboxapp.com/terms
     * 	https://www.waboxapp.com/pricing
     * 	Limit of 100 free messages every month
     * **/

    [Route("api/[controller]")]
	[ApiController]
	public class WhatsappController : ControllerBase
	{
        private static readonly HttpClient client = new HttpClient();
        private readonly WaboxappSettings _waboxappSettings;

        public WhatsappController(IOptions<WaboxappSettings> waboxappSettings)
        {
            _waboxappSettings = waboxappSettings.Value;
        }

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

        // POST api/<WhatsappController>/sendmessage
        [HttpPost("sendmessage")]
		public async Task<IActionResult> SendMessage(SendMessage model)
		{
			var message = new
			{
				token = _waboxappSettings.ApiKey,
				uid = model.ConfiguredPhoneNumberOnWaboxapp,
				to = model.To,
				custom_uid = Guid.NewGuid().ToString(),
				text = model.Text
			};

			var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

			var response = await client.PostAsync(_waboxappSettings.ApiUrl, content);

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
