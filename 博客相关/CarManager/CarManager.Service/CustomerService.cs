
using CarManager.Core.Domain;

namespace CarManager.Service
{
    public class ICustomerService : IICustomerService
    {
        private readonly IRepository<Customer> customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public void CreateCustomer(Customer customer)
        {
            customerRepository.Insert(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            customerRepository.Delete(customer);
        }

        public Customer GetCustomer(int customerID)
        {
            return customerRepository.GetById(customerID);
        }

        public IPagedList<Customer> GetCustomers(int pageNumber, int pageSize)
        {
            return customerRepository.Table.ToPagedList(m => m.ID, pageNumber, pageSize);
        }

        public void UpdateCustomer(Customer customer)
        {
            customerRepository.Update(customer);
        }
    }
}