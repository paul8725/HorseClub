using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HorseClub.Models;

namespace HorseClub.Data
{
    public class HorseClubContext : DbContext
    {
        public HorseClubContext (DbContextOptions<HorseClubContext> options)
            : base(options)
        {
        }
        public DbSet<Breeds> Breeds { get; set; }
        public DbSet<YourHorses> YourHorses { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Login> Login { get; set; }
    }
}
