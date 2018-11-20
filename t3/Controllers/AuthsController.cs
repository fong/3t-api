using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class AuthsController : ControllerBase
    {
        private readonly t3Context _context;

        public AuthsController(t3Context context)
        {
            _context = context;
        }

        // POST: api/Auths
        [HttpPost]
        public async Task<IActionResult> PostAuth([FromBody] Auth auth)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (auth.playerName != null)
            {
                var q = await _context.Auth.Where(x => x.playerName == auth.playerName).FirstOrDefaultAsync();

                if (q != null)
                {
                    if (q.passcode == auth.passcode) // Correct passcode
                    {
                        return Ok(q);
                    } else // Incorrect passcode
                    {
                        return Ok("Fail");
                    }
                } else // Create account
                {
                    auth.playerID = Guid.NewGuid().ToString();
                    var p = new Player
                    {
                        playerID = auth.playerID,
                        playerName = auth.playerName,
                        mmr = 0,
                        wins = 0,
                        games = 0
                    };

                    _context.Player.Add(p);

                    _context.Auth.Add(auth);
                    await _context.SaveChangesAsync();
                    return Ok(p);
                }
            }
            return Ok();
        }

        private bool AuthExists(string id)
        {
            return _context.Auth.Any(e => e.playerID == id);
        }
    }
}