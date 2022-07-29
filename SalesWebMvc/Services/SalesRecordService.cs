using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;  //previnir que a dependencia n pode ser alterada: readonly


       

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

       
        //operação assincrona que busca os registros de venda por data com as datas minima e maxima opcionais:
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)//? significa opcional
        {
            //encontrar as vendas nesse intervalo de datas:
            var result = from obj in _context.SalesRecord select obj;//constroi o obj iqueryable

            if (minDate.HasValue)//se tem uma data mínima
            {
                result = result.Where(x => x.Date >= minDate.Value);// maior ou igual a data mínima
            }
            if (maxDate.HasValue)// se tem data máxima
            {
                result = result.Where(x => x.Date <= maxDate.Value);//menor ou igual a data maxima
            }

            return await result
                .Include(x => x.Seller)//inclui vendedor
                .Include(x => x.Seller.Department)//inclui departamento
                .OrderByDescending(x => x.Date)//inclui data em ordem decresente
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)//? significa opcional
        {
            //encontrar as vendas nesse intervalo de datas:
            var result = from obj in _context.SalesRecord select obj;//constroi o obj iqueryable

            if (minDate.HasValue)//se tem uma data mínima
            {
                result = result.Where(x => x.Date >= minDate.Value);// maior ou igual a data mínima
            }
            if (maxDate.HasValue)// se tem data máxima
            {
                result = result.Where(x => x.Date <= maxDate.Value);//menor ou igual a data maxima
            }

            return await result
                .Include(x => x.Seller)//inclui vendedor
                .Include(x => x.Seller.Department)//inclui departamento
                .OrderByDescending(x => x.Date)//inclui data em ordem decresente
                .GroupBy(x => x.Seller.Department)//agrupar os resultados
                .ToListAsync();
        }
    }
}


