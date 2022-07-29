using System;

namespace SalesWebMvc.Models.ViewModels 
{
    
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; }


        //testar se o id existe(retorna se não é nulo ou vazio):
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}