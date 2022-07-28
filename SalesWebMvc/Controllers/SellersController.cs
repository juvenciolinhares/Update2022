using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //por padrao o gerador scaffold criou uma pagina index
        public IActionResult Index()
        {
            return View();
        }
    }
}
