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
    public class GamesController : ControllerBase
    {
        private readonly t3Context _context;

        public GamesController(t3Context context)
        {
            _context = context;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<IActionResult> GetGame([FromQuery]string gameID)
        {
            if (gameID != null)
            {
                var q_game = from m in _context.Game select m;

                if (!String.IsNullOrEmpty(gameID))
                {
                    q_game = q_game.Where(s => s.gameID.Contains(gameID));
                }

                Debug.WriteLine(gameID);
                var game = _context.Game.Find(gameID);
                Debug.WriteLine(game);
                if (game != null)
                {
                    if (game.gameID == gameID)
                    {
                        return Ok(game);
                    } else
                    {
                        return Ok();
                    }
                } else
                {
                    return Ok();
                }
            } else
            {
                var game = _context.Game.ToList(); ;
                return Ok(game);
            }
        }

        //// GET: api/Games/5
        //[HttpGet("{gameID}")]
        //public async Task<IActionResult> GetGame([FromRoute] string gameID)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var game = await _context.Game.FindAsync(gameID);

        //    if (game.gameID != gameID)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(game);
        //}

        // PUT: api/Games/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame([FromRoute] string id, [FromBody] Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != game.gameID)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Games
        [HttpPost]
        public async Task<IActionResult> PostGame([FromBody] Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Game.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = game.gameID }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Game.Remove(game);
            await _context.SaveChangesAsync();

            return Ok(game);
        }

        private bool GameExists(string id)
        {
            return _context.Game.Any(e => e.gameID == id);
        }
    }
}