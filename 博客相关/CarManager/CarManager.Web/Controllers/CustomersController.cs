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
    public class CustomersController : BaseController
    {
        private readonly IMapper mapper;

        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            this.mapper = mapper;
            this.customerService = customerService;
        }

        public ActionResult Index(string keyword, int page = 1)
        {
            IPagedList<Customer> customers = customerService.GetCustomers(keyword, page, 15);
            var customerModels = mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerModel>>(customers);
            var viewModel = new StaticPagedList<CustomerModel>(customerModels, customers.GetMetaData());
            return Request.IsAjaxRequest() ? (ActionResult)PartialView("CustomerListPartial", viewModel) : View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [ModelStateValidFilter, HttpPost]
        public ActionResult Create(CustomerModel model)
        {
            Customer customer = mapper.Map<CustomerModel, Customer>(model);
            customerService.CreateCustomer(customer);
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View(mapper.Map<Customer, CustomerModel>(customerService.GetCustomer(id)));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            customerService.DeleteCustomer(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(mapper.Map<Customer, CustomerModel>(customerService.GetCustomer(id)));
        }

        [ModelStateValidFilter, HttpPost]
        public ActionResult Edit(CustomerModel model)
        {
            Customer customer = mapper.Map<CustomerModel, Customer>(model);
            customerService.UpdateCustomer(customer);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(mapper.Map<Customer, CustomerModel>(customerService.GetCustomer(id)));
        }
	}
}