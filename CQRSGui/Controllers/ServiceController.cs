using System;
using System.Web.Mvc;
using MediatR;
using SimpleCQRS;

namespace CQRSGui.Controllers
{
    [HandleError]
    public class ServiceController : Controller
    {
        private IMediator _mediator;
        private ReadModelFacade _readmodel;

        public ServiceController()
        {
            _mediator = ServiceLocator.Mediator;
            _readmodel = new ReadModelFacade();
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
            _mediator.Send(new CreateInventoryItem(Guid.NewGuid(), name));

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
            _mediator.Send(new RenameInventoryItem(id, name, version));

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
            _mediator.Send(new DeactivateInventoryItem(id, version));

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
            _mediator.Send(new CheckInItemsToInventory(id, number, version));

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
            _mediator.Send(new RemoveItemsFromInventory(id, number, version));

            return RedirectToAction("Index");
        }
    }
}
