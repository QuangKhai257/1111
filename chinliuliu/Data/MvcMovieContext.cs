using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using chinliuliu.Models;

namespace MvcMovie.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<chinliuliu.Models.Lophoc> Lophoc { get; set; } = default!;

        public DbSet<chinliuliu.Models.Sinhvien> Sinhvien { get; set; } = default!;
    }
}
