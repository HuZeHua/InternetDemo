using CarManager.Core.Domain;
using CarManager.Core.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Service.Cars
{
    public interface ICarService
    {
        void CreateCar(Car car);

        void UpdateCar(Car car);

        void DeleteCar(Car car);

        IPagedList<Car> GetCars(string keyword,int pageNumber, int pageSize);
    }
}
