using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {

        //declarar dependencias:
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;


        //cont p injetar dependencia:
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()//controlador
        {
            //implementar a chama de sellerService.FindAll que retorna uma lista de seller
            var list = _sellerService.FindAll();//model

            //lista vai ser passada como arg p ser gerado uma lista 
            return View(list);//view
        }

        public IActionResult Create()
        {
            //carregar os depts
            var departments = _departmentService.FindAll();//busca do bd tds depts

            var viewModel = new SellerFormViewModel { Departments = departments };// inicia com a lista de depts buscada acima
            return View(viewModel);
        }

        //criar a ação create que recebe o obj vendendor que veio da requisição:

        [HttpPost]//indica que essa ação é uma ação de POST
        [ValidateAntiForgeryToken]//previnir que a app sofra ataques csrf(qnd alguem aproveita a sua sessão de autentic e envia dados maliciosos)
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);//inseriu o vendedor
            return RedirectToAction(nameof(Index));//redirecionar a requisição p index(que mostra a tela principal do crud de vendedores )
        }

    }
}
/*mvc acontecendo nas linhas:
 *  public IActionResult Index()//controlador
 *  var list = _sellerService.FindAll();//model
 *  return View(list);//view
 */