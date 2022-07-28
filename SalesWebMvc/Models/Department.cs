using System.Collections.Generic;
using System;
using System.Linq;
namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();//implementação de departamento com seller

        //construtor vazio por conta do framework
        public Department()
        {

        }

        //construtor com argumentos, mas sem coleções
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        //adicionar um vendedor
        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double totalSales(DateTime initial, DateTime final)
        {
            /*implementar utilizando o link
             * 1°: exporta link;
             * 2° pegar a lista de vendedores
             * 3° soma/filtrar(wherer) o total de vendas de cada vendedor nesse intervalo de datas:
             */
            return Sellers.Sum(seller => seller.totalSales(initial, final));//pega cada vend, chamar o totalsales do vend e soma p tds 
        }
    }
}
