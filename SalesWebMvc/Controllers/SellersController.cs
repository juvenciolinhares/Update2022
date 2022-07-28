using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {

        //declarar uma dependencia de sellerservice:
        private readonly SellerService _sellerService;


        //cont p injetar dependencia:
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }
        
        public IActionResult Index()//controlador
        {
            //implementar a chama de sellerService.FindAll que retorna uma lista de seller
            var list = _sellerService.FindAll();//model

            //lista vai ser passada como arg p ser gerado uma lista 
            return View(list);//view
        }
    }
}
/*mvc acontecendo nas linhas:
 *  public IActionResult Index()//controlador
 *  var list = _sellerService.FindAll();//model
 *  return View(list);//view
 */