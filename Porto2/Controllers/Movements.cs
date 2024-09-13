using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Porto.Models;
using Porto.Repositorio;
using System.ComponentModel;

namespace Porto.Controllers
{
    public class Movements : Controller
    {
        public IMovementsRepository _repository;
        public IContainerRepository _repositoryContainer;

        public Movements(IMovementsRepository repository, IContainerRepository repositoryContainer)
        {
            _repository = repository;
            _repositoryContainer = repositoryContainer;
        }

        public IActionResult Index()
        {
            var movementsList = _repository.ListAll();
            return View(movementsList);
        }

        public IActionResult Create()
        {
            var containersList = _repositoryContainer.ListAll();
            ViewBag.Containers = containersList.Select(x => new SelectListItem() { Text = x.Number?.ToString(), Value = x.Id.ToString() });
            return View();
        }

        public IActionResult Edit(int Id)
        {
            var containersList = _repositoryContainer.ListAll();
            ViewBag.Containers = containersList.Select(x => new SelectListItem() { Text = x.Number?.ToString(), Value = x.Id.ToString() });

            Porto.Models.Movements Movements = _repository.GetById(Id);
            return View(Movements);
        }

        public IActionResult Delete(int Id)
        {
            try
            {
                _repository.Delete(Id);
                TempData["SuccessMessage"] = "Movimentação excluída com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Create(Models.Movements Movements)
        {
            try
            {
                if (!ModelState.IsValid || Movements.MovementType.IsNullOrEmpty())
                {
                    var containersList = _repositoryContainer.ListAll();
                    ViewBag.Containers = containersList.Select(x => new SelectListItem() { Text = x.Number?.ToString(), Value = x.Id.ToString() });
                    ViewData["WarningMessage"] = "Campos obrigatórios não preenchidos!";
                    return View(Movements);
                }

                _repository.Add(Movements);
                TempData["SuccessMessage"] = "Movimentação criada com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(Models.Movements Movements)
        {
            try
            {
                if (!ModelState.IsValid || Movements.MovementType.IsNullOrEmpty())
                {
                    var containersList = _repositoryContainer.ListAll();
                    ViewBag.Containers = containersList.Select(x => new SelectListItem() { Text = x.Number?.ToString(), Value = x.Id.ToString() });
                    ViewData["WarningMessage"] = "Campos obrigatórios não preenchidos!";
                    return View(Movements);
                }

                _repository.Edit(Movements);
                TempData["SuccessMessage"] = "Movimentação editada com sucesso!";
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
