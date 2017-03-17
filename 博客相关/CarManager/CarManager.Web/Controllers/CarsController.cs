using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarManager.Core.Domain;

namespace CarManager.Web.Controllers
{
    public class CarsController : BaseController
    {
        private readonly IMapper mapper;

        private readonly ICarService carService;

        public CarController(ICarService carService, IMapper mapper)
        {
            this.mapper = mapper;
            this.carService = carService;
        }

        public ActionResult Index(string keyword, int page = 1)
        {
            IPagedList<Car> cars = carService.GetCars(keyword, page, 15);
            var carModels = mapper.Map<IEnumerable<Car>, IEnumerable<CarModel>>(cars);
            var viewModel = new StaticPagedList<CarModel>(carModels, cars.GetMetaData());
            return Request.IsAjaxRequest() ? (ActionResult)PartialView("CarListPartial", viewModel) : View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [ModelStateValidFilter, HttpPost]
        public ActionResult Create(CarModel model)
        {
            Car car = mapper.Map<CarModel, Car>(model);
            carService.CreateCar(car);
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View(mapper.Map<Car, CarModel>(carService.GetCar(id)));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            carService.DeleteCar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(mapper.Map<Car, CarModel>(carService.GetCar(id)));
        }

        [ModelStateValidFilter, HttpPost]
        public ActionResult Edit(CarModel model)
        {
            Car car = mapper.Map<CarModel, Car>(model);
            carService.UpdateCar(car);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(mapper.Map<Car, CarModel>(carService.GetCar(id)));
        }
	}
}