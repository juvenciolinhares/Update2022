using System;
using SalesWebMvc.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }

        //formatar a data:
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        //formatar amount:
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double Amount { get; set; }
        public SaleStatus Status { get; set; }
        public Seller Seller { get; set; } //cada salesrecord tem um vendedor(seller)

        //construtor vazio por conta do framework
        public SalesRecord()
        {

        }

        //construtor com argumentos, mas sem coleções(essas informações vao aparecer no bd)
        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
