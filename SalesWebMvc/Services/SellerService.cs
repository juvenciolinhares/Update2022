using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;  //previnir que a dependencia n pode ser alterada: readonly


        //construtor pra ocorrer a injeção de dependencia:

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        /*operação FindAll p retornar uma lista com tds os vendedores do bd:
         * operação sícrona: vai rodar o acesso ao bd=> _context.Seller.ToList()
         * e a app vai ficar bloqueada esperando a operação terminar.
         */
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();//acessa a fonte de dados de vendedores e converte p lista
        }

        public async Task InsertAsync(Seller obj)
        {

            _context.Add(obj);// inserir esse obj(funcionario) no bd
            await _context.SaveChangesAsync();//confirma/salva essa inserção
        }

        //retorna um vendendo que possui esse id ou retonar nulo, caso n exista 
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);//encontrar/pegar obj
            _context.Seller.Remove(obj);//removeu o bj do dbset
            await _context.SaveChangesAsync();//confirmaçãoi da operação
        }

        //operação update seller:
        public async Task UpdateAsync(Seller obj)
        {
            //testar se o id ja existe
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny)//se o id não existir:
            {
                throw new NotFoundException("Id not found");

            }
            try
            {
                _context.Update(obj);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

        }
    }
}
