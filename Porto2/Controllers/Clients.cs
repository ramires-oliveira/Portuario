using Microsoft.AspNetCore.Mvc;
using Porto.Models;
using Porto.Repositorio;

namespace Porto.Controllers
{
    public class Clients : Controller
    {
        public IClientRepository _repository;

        public Clients(IClientRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var clientsList = _repository.ListAll();
            return View(clientsList);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int Id)
        {
            Client cliente = _repository.GetById(Id);
            return View(cliente);
        }

        public IActionResult Delete(int Id)
        {
            try
            {
                _repository.Delete(Id);
                TempData["SuccessMessage"] = "Cliente excluído com sucesso!";
                var clientsList = _repository.ListAll();
                return View("Index", clientsList);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                var clientsList = _repository.ListAll();
                return View("Index", clientsList);
            }
        }

        [HttpPost]
        public IActionResult Create(Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(client);
                }

                _repository.Add(client);
                TempData["SuccessMessage"] = "Cliente cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(client);
                }

                _repository.Edit(client);
                TempData["SuccessMessage"] = "Cliente editado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
