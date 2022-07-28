using System;
using SalesWebMvc.Models.Enums;
using System.Collections.Generic;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
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
