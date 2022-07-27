using System;

namespace SalesWebMvc.Models.ViewModels 
{
    /*Modelo auxiliar pra povoar as telas*/
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}