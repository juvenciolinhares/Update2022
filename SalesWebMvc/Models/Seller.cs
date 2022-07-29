using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        //costumizar/validar o nome
        [Required(ErrorMessage ="{0} required")]//campo obrigatório
        [StringLength(60, MinimumLength = 3, ErrorMessage ="{0} size should be between {2} and {1}")]//qtd de carac do nome
        public string Name { get; set; }


        //custmizar/validar o email:
        [Required(ErrorMessage = "{0} required")]//campo obrigatório
        [EmailAddress(ErrorMessage ="Enter a valid email")]
        [DataType(DataType.EmailAddress)] //email como um link pra ja enviar o email
        public string Email { get; set; }

        //customizar/validar a data:
        [Required(ErrorMessage = "{0} required")]//campo obrigatório
        [Display(Name = "Birth Date")]//Birth Date separado
        [DataType(DataType.Date)]//sem aparecer as horas
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]//dia/mes/ano
        public DateTime BirthDate { get; set; }

        //customizar/validar o salário:
        [Required(ErrorMessage = "{0} required")]//campo obrigatório
        [Range(100.0, 50000.0, ErrorMessage ="{0} nuste be form {1} to {2}")]//salario: minimo 100 e mais 50000
        [Display(Name = "Base salary")]//Base salary separado
        [DisplayFormat(DataFormatString = "{0:F2}")]// duas casas decimais depois do ponto
        public double BaseSalary { get; set; }
        public Department Department { get; set; }//implementar department em seller
        public int DepartmentId { get; set; }// garante a existencia do id
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); //associação de salesrecord com sales

        //construtor vazio por conta do framework
        public Seller()
        {

        }

        //construtor com argumentos, mas sem coleções(essas informações vao aparecer no bd)
        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }


        //metodos p add e remover uma venda
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        //operação que retorna o total de vendas
        public double totalSales(DateTime initial, DateTime final)
        {
            /*implementar utilizando o link]
             * 1°: exporta link;
             * 2° chama colecão Sales(lista de vendas associada ao vendedor)
             * 3° filtra(WHERE) a lista p obter uma nova lista contendo apenas as vendas no intervalo dado no argumento
             * 4° calcular a soma das vendas(amount)
             */
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);

        }
    }
}
