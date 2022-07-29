using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();//acessa a fonte de dados de vendedores e converte p lista
        }

        public void Insert(Seller obj)
        {
            
            _context.Add(obj);// inserir esse obj(funcionario) no bd
            _context.SaveChanges();//confirma/salva essa inserção
        }

        //retorna um vendendo que possui esse id ou retonar nulo, caso n exista 
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj=> obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);//encontrar/pegar obj
            _context.Seller.Remove(obj);//removeu o bj do dbset
            _context.SaveChanges();//confirmaçãoi da operação

        }
    }
}
