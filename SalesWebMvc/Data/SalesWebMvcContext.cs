using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;


namespace SalesWebMvc.Data
{
    public class SalesWebMvcContext : DbContext//conexão com o bd
    {
        public SalesWebMvcContext (DbContextOptions<SalesWebMvcContext> options)
            : base(options)
        {
        }

        //add um dbset p cada entidade(faz aparecer as informações dos atributos/construtores no bd):
        public DbSet<SalesWebMvc.Models.Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}
