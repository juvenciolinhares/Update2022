using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;  //previnir que a dependencia n pode ser alterada: readonly


        //construtor pra ocorrer a injeção de dependencia:

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //metodo que retorna todos os departamentos:
        public async Task<List<Department>> FindAllAsync()//converter p assincrono
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();//retorna ordenado
        }
    }
}
