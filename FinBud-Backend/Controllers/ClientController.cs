using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using FinBud_Backend.Dto.Clients;
using FinBud_Backend.Mapping;
using FinBud_Backend.Models;
using FinBud_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinBud_Backend.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService; 
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }   

        [HttpGet]
        public IActionResult getAll() {
            return Ok("I love you so so so much <3 \n\n _,-^-;,-'''''-.\n/_  ` )  `      | \n`-., _,  ;      /\n   )_))_,-_,-/_(\n\n<3 Moodeng <3");
        }

        [HttpGet("private")]
        [Authorize]
        public IActionResult Private()
        {
            // var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            // if (string.IsNullOrEmpty(userId))
            //     return Unauthorized("Invalid user ID.");

            // var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            // if (string.IsNullOrEmpty(userId))
            //     return Unauthorized("Invalid user ID.");
                
            return Ok(new
            {
                Message = "I love you Vanessa!!!"          
            });
        }

        [HttpPost("save")]
        [Authorize]
        public async Task<IActionResult> SaveInfo(CreateClientRequestDto clientDto)
        {
            var client = clientDto.ToClientFromCreateDTO();

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid user ID.");
            client.Id = userId;

            await _clientService.CreateAsync(client);

            return Ok( new 
            {
                Message="User Saved (="
            });
        }
    }
}