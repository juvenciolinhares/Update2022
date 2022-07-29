using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public async Task<IActionResult> Index()//controlador
        {
            //implementar a chama de sellerService.FindAll que retorna uma lista de seller
            var list = await _sellerService.FindAllAsync();//model

            //lista vai ser passada como arg p ser gerado uma lista 
            return View(list);//view
        }

        public async Task<IActionResult> Create()
        {
            //carregar os depts
            var departments = await _departmentService.FindAllAsync();//busca do bd tds depts

            var viewModel = new SellerFormViewModel { Departments = departments };// inicia com a lista de depts buscada acima
            return View(viewModel);
        }

        //criar a ação create que recebe o obj vendendor que veio da requisição:

        [HttpPost]//indica que essa ação é uma ação de POST
        [ValidateAntiForgeryToken]//previnir que a app sofra ataques csrf(qnd alguem aproveita a sua sessão de autentic e envia dados maliciosos)
        public async Task<IActionResult> Create(Seller seller)
        {
            //testar se o seller é valido:
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);//enquanto o usuario n preencher direito o formulario isso se repete
            }
            await _sellerService.InsertAsync(seller);//inseriu o vendedor
            return RedirectToAction(nameof(Index));//redirecionar a requisição p index(que mostra a tela principal do crud de vendedores )
        }

        //abrir uma tela de confirmação de delete, mas não deleta ainda
        public async Task<IActionResult> Delete(int? id)//o ? significa que é opcional
        {
            //1°testa se o id é nulo
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });//id nao fornecido
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });//id nao encontrado
            }
            return View(obj);
        }


        //ação de deletar seller:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));//redirecionar p tela inicial de listagem do CRUD

        }

        //ação detalhes:
        public async Task<IActionResult> Details(int? id)
        {
            //1°testa se o id é nulo
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        //ação edit: abre a tela de edição do vendedor:
        public async Task<IActionResult> Edit(int? id)
        {
            //testendo se o id é igual a nulo:
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            //testar se o id existe no bd:
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            //abrir a tela de edição:
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        //ação edit para o método post:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            //testar se o seller é valido:
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);//enquanto o usuario n preencher direito o formulario isso se repete
            }
            //testando se o id do parametro é diferente do id do vendedor
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });//id nao corresponde
            }
            try
            {
               await _sellerService.UpdateAsync(seller);//atualização
                return RedirectToAction(nameof(Index));//redirecionar a requisição p a pagina inicial do crud(index)
            }
            catch (ApplicationException e)//ApplicationException: genérico, pega todas as exceções
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        //ação de erro recebendo uma msg como parametro que retorna a view de erro
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier //pega o id interno da requisição

            };
            return View(viewModel);
        }

    }
}
/*mvc acontecendo nas linhas:
 *  public IActionResult Index()//controlador
 *  var list = _sellerService.FindAll();//model
 *  return View(list);//view
 */