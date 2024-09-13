using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Porto.Models;
using Porto.Repositorio;

namespace Porto.Controllers
{
    public class Containers : Controller
    {
        public IContainerRepository _repository;
        public IClientRepository _repositoryClient;

        public Containers(IContainerRepository repository, IClientRepository repositoryCliente)
        {
            _repository = repository;
            _repositoryClient = repositoryCliente;
        }

        public IActionResult Index()
        {
            var containersList = _repository.ListAll();
            return View(containersList);
        }

        public IActionResult Create()
        {
            var clientsList = _repositoryClient.ListAll();
            ViewBag.Clientes = clientsList.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            return View();
        }

        public IActionResult Edit(int Id)
        {
            var clientsList = _repositoryClient.ListAll();
            ViewBag.Clientes = clientsList.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });
            Container container = _repository.GetById(Id);
            return View(container);
        }

        public IActionResult Delete(int Id)
        {
            try
            {
                _repository.Delete(Id);
                TempData["SuccessMessage"] = "Container excluído com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Create(Container container)
        {
            try
            {
                if (!ModelState.IsValid || container.Type.IsNullOrEmpty())
                {
                    var clientsList = _repositoryClient.ListAll();
                    ViewBag.Clientes = clientsList.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();
                    ViewData["WarningMessage"] = "Campos obrigatórios não preenchidos!";
                    return View(container);
                }

                var check = CheckContainerNumber(container.Number);

                if (check == false)
                {
                    ModelState.AddModelError("Number", "O número do container precisa ter 11 caracteres, dentre elas 4 letras e 7 números");

                    var listaClientes = _repositoryClient.ListAll();
                    ViewBag.Clientes = listaClientes.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });

                    return View();
                }

                _repository.Add(container);
                TempData["SuccessMessage"] = "Container criado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(Container container)
        {
            try
            {
                if (!ModelState.IsValid || container.Type.IsNullOrEmpty())
                {
                    var clientsList = _repositoryClient.ListAll();
                    ViewBag.Clientes = clientsList.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();
                    ViewData["WarningMessage"] = "Campos obrigatórios não preenchidos!";
                    return View(container);
                }

                var check = CheckContainerNumber(container.Number);

                if (check == false)
                {
                    ModelState.AddModelError("Number", "O número do container precisa ter 11 caracteres, dentre elas 4 letras e 7 números");

                    var clientsList = _repositoryClient.ListAll();
                    ViewBag.Clientes = clientsList.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });

                    return View("Edit");
                }

                _repository.Edit(container);
                TempData["SuccessMessage"] = "Container editado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public bool CheckContainerNumber(string containerNumber)
        {
            int letters = 0;
            int numbers = 0;

            if (containerNumber.Length < 11 || containerNumber.Length > 11)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < containerNumber.Length; i++)
                {
                    int result;
                    if (int.TryParse(containerNumber[i].ToString(), out result))
                    {
                        numbers++;
                    }
                    else
                    {
                        letters++;
                    }
                }
                if (letters == 4 && numbers == 7)
                    return true;
            }

            return false;
        }

    }
}
