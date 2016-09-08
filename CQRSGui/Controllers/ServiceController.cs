using System;
using System.Web.Mvc;
using MediatR;
using SimpleCQRS;

namespace CQRSGui.Controllers
{
    [HandleError]
    public class ServiceController : Controller
    {
        private ReadModelFacade _readmodel;
        private readonly IRepository<InventoryItem> _repository;
        private readonly InventoryItemService _service;

        public ServiceController()
        {
            _readmodel = new ReadModelFacade();
            _repository = ServiceLocator.InventoryRepository;
            _service = new InventoryItemService(_readmodel, _repository);
        }

        public ActionResult Index()
        {
            ViewData.Model = _readmodel.GetInventoryItems();

            return View();
        }

        public ActionResult Details(Guid id)
        {
            ViewData.Model = _readmodel.GetInventoryItemDetails(id);

            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string name)
        {
            var item = new InventoryItem(Guid.NewGuid(), name);
            _repository.Save(item, -1);

            return RedirectToAction("Index");
        }

        public ActionResult ChangeName(Guid id)
        {
            ViewData.Model = _readmodel.GetInventoryItemDetails(id);

            return View();
        }

        [HttpPost]
        public ActionResult ChangeName(Guid id, string name, int version)
        {
            var item = _repository.GetById(id);
            item.ChangeName(name);
            _repository.Save(item, version);

            return RedirectToAction("Index");
        }

        public ActionResult Deactivate(Guid id)
        {
            ViewData.Model = _readmodel.GetInventoryItemDetails(id);

            return View();
        }

        [HttpPost]
        public ActionResult Deactivate(Guid id, int version)
        {
            var item = _repository.GetById(id);
            item.Deactivate();
            _repository.Save(item, version);

            return RedirectToAction("Index");
        }

        public ActionResult CheckIn(Guid id)
        {
            ViewData.Model = _readmodel.GetInventoryItemDetails(id);

            return View();
        }

        [HttpPost]
        public ActionResult CheckIn(Guid id, int number, int version)
        {
            var item = _repository.GetById(id);
            item.CheckIn(number);
            _repository.Save(item, version);

            return RedirectToAction("Index");
        }

        public ActionResult Remove(Guid id)
        {
            ViewData.Model = _readmodel.GetInventoryItemDetails(id);

            return View();
        }

        [HttpPost]
        public ActionResult Remove(Guid id, int number, int version)
        {
            var item = _repository.GetById(id);
            item.Remove(number);
            _repository.Save(item, version);

            return RedirectToAction("Index");
        }
    }
}
