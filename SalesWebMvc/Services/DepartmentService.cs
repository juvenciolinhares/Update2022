using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;


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
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();//retorna ordenado
        }
    }
}
