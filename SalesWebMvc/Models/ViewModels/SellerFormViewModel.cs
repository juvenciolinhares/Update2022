using System.Collections.Generic;

namespace SalesWebMvc.Models.ViewModels
{
    public class SellerFormViewModel
    {
        //add dados necessários p uma tela de cadastro de vendedor
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
