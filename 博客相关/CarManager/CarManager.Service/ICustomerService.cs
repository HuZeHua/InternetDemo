
using CarManager.Core.Domain;

namespace CarManager.Service
{
    public interface IICustomerService
    {
        Customer GetCustomer(int customerID);

        void UpdateCustomer(Customer customer);

        void CreateCustomer(Customer customer);

        void DeleteCustomer(Customer customer);

        IPagedList<Customer> GetCustomers(int pageNumber, int pageSize);
    }
}