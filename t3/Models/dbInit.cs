using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace t3.Models
{
    public class dbInit
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new t3Context(
                serviceProvider.GetRequiredService<DbContextOptions<t3Context>>()))
            {
                if (context.Game.Count() > 0)
                {
                    context.Game.RemoveRange(context.Game);
                    context.SaveChanges();
                }

                //if (context.Player.Count() == 0)
                //{
                //    context.Player.AddRange(new Player
                //    {
                //        playerID = "aaaaaa",
                //        playerName = "SundriedTofu",
                //        mmr = 0,
                //        wins = 0,
                //        games = 0
                //    });
                //    context.SaveChanges();
                //}

                //if (context.Auth.Count() == 0)
                //{
                //    context.Auth.AddRange(new Auth
                //    {
                //        playerID = "aaaaaa",
                //        playerName = "SundriedTofu",
                //        passcode = "376989"
                //    });
                //    context.SaveChanges();
                //}
                return;
            }
        }
    }
}
