using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using t3.Models;

    public class t3Context : DbContext
    {
        public t3Context (DbContextOptions<t3Context> options)
            : base(options)
        {
        }

        public DbSet<t3.Models.Game> Game { get; set; }

        public DbSet<t3.Models.Player> Player { get; set; }
    }
