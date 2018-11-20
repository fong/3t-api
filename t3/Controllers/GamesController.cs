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
                var game = await _context.Game.FindAsync(gameID);

                if (game != null)
                {
                    return Ok(game);
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

        // POST: api/Games
        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] Game g)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var game = await _context.Game.FindAsync(g.gameID);

            if (game != null)
            {
                return Ok("game already exists");
            } else
            {
                var new_game = new Game
                {
                    gameID = g.gameID,
                    player1 = g.player1,
                    player2 = null,
                    board = "[0,0,0,0,0,0,0,0,0]",
                    watchers = 0,
                    turn = 0,
                    p1_timestamp = 0,
                    p2_timestamp = 0,
                    winner = 0, // 0 = not started, 1 = player1, 2 = player2, 3 = draw
                };

                _context.Game.Add(new_game);
                await _context.SaveChangesAsync();
                return Ok(new_game);
            }
        }

        // PUT: api/Games/5
        [HttpPut]
        public async Task<IActionResult> PutGame([FromBody] Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                _context.Game.Update(game);

                if (game.winner > 0)
                {
                    var p1 = await _context.Player.FindAsync(game.player1);
                    var p2 = await _context.Player.FindAsync(game.player2);
                    _context.Entry(p1).State = EntityState.Modified;
                    _context.Entry(p2).State = EntityState.Modified;

                    if (game.winner == 1)
                    { 
                        p1.mmr++;
                        p2.mmr--;
                        p1.games++;
                        p2.games++;
                        p1.wins++;
                        _context.Player.Update(p1);
                        _context.Player.Update(p2);
                    } else if (game.winner == 2)
                    {
                        p1.mmr--;
                        p2.mmr++;
                        p1.games++;
                        p2.games++;
                        p2.wins++;
                        _context.Player.Update(p1);
                        _context.Player.Update(p2);
                    } else if (game.winner == 3)
                    {
                        p1.games++;
                        p2.games++;
                        _context.Player.Update(p1);
                        _context.Player.Update(p2);
                    } else if (game.winner == 4)
                    {
                        game.board = "[0,0,0,0,0,0,0,0,0]";
                        game.winner = 0;
                        _context.Game.Update(game);
                    }
                } else
                {
                    if (game.player2 != null)
                    {
                        Random rand = new Random();
                        game.turn = rand.Next(1, 3);
                        _context.Game.Update(game);
                    }
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(game.gameID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/Games/5
        [HttpDelete]
        public async Task<IActionResult> DeleteGame([FromQuery] string id)
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