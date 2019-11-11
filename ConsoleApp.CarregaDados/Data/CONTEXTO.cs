using ConsoleApp.CarregaDados.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.CarregaDados.Data
{
    public class CONTEXTO : DbContext
    {
        public DbSet<USUARIOENRIQUECIMENTO> BIKEPRICE { get; set; }

        // public DbSet<BikePlan> BikePlans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=BRSPO1IDB11\\INTEGRA_ESPELHO;Database=ITURANWEB;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }


    }
}
