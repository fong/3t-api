using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using t3.Models;

namespace t3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly t3Context _context;

        public PlayersController(t3Context context)
        {
            _context = context;
        }

        // GET: api/Players/5
        [HttpGet]
        public async Task<IActionResult> GetPlayer([FromQuery] string playerid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (playerid == null)
            {
                return Ok(_context.Player);
            }

            var player = await _context.Player.FindAsync(playerid);

            if (player == null)
            {
                return NotFound();
            } else
            {
                return Ok(player);
            }
        }

        [HttpGet("top")]
        public async Task<IActionResult> GetTopPlayers([FromQuery] string mode)
        {
            if (mode == "mmr")
            {
                var p = _context.Player.OrderByDescending(x => x.mmr).Take(10);
                return Ok(p);
            } else if ( mode == "wins")
            {
                var p = _context.Player.OrderByDescending(x => x.wins).Take(10);
                return Ok(p);
            } else
            {
                return Ok();
            }
        }

        private bool PlayerExists(string id)
        {
            return _context.Player.Any(e => e.playerID == id);
        }
    }
}