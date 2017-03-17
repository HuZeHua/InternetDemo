using AutoMapper;
using CarManager.Core.Domain;
using CarManager.Service.Cars;
using CarManager.Web.Models.Cars;
using CarManager.WebCore.MVC;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using CarManager.Core.Paging;
using CarManager.Web.Validator;

namespace CarManager.Web.Controllers
{
    public class CarController : BaseController
    {

        private readonly ICarService carService;

        private readonly IMapper mapper;

        private readonly MapperConfiguration mapperConfig;

        public CarController(ICarService carService, IMapper mapper, MapperConfiguration mapperConfig)
        {
            this.carService = carService;
            this.mapper = mapper;
            this.mapperConfig = mapperConfig;
        }


        public ActionResult Index(string keyword, int page = 1)
        {
            ViewBag.keyword = keyword;
            IPagedList<Car> carList = carService.GetCars(keyword, page, 10);
            var viewModel = mapper.Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(carList);
            var pagedViewModel = new StaticPagedList<CarViewModel>(viewModel, carList.GetMetaData());

            return Request.IsAjaxRequest() ? (ActionResult)PartialView("CarListPartial", pagedViewModel) : View(pagedViewModel);
        }

        //创建车，编辑车
        public JsonResult GetCars()
        {
            return Json(new { id = 123, name = "超跑" }, JsonRequestBehavior.AllowGet);
        }

        //创建车
        public ActionResult Create()
        {
            return View();
        }

        //编辑车
        public ActionResult Edit()
        {
            return View();

        }

        [HttpPost, ModelStateValidFilter]
        public ActionResult Create(CarViewModel model)
        {
            Car car = mapper.Map<CarViewModel, Car>(model);
            car.CreateDate = DateTime.Now;
            carService.CreateCar(car);

            return View();
        }
    }
}