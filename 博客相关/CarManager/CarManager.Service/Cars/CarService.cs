using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarManager.Core.Domain;
using CarManager.Core.Data;
using CarManager.Core.Cache;
using CarManager.Core.Paging;

namespace CarManager.Service.Cars
{
    public class CarService : ICarService
    {
        private readonly IRepository<Car> carRepository;

        private readonly ICacheManager cacheManager;

        private const string CarsCacheKey = nameof(CarService) + nameof(Car);

        public CarService(IRepository<Car> carRepository, ICacheManager cacheManager)
        {
            this.carRepository = carRepository;
            this.cacheManager = cacheManager;
        }

        public void CreateCar(Car car)
        {
            carRepository.Insert(car);
        }

        public void DeleteCar(Car car)
        {
            carRepository.Delete(car);
        }

        public IPagedList<Car> GetCars(string keyword, int pageNumber, int pageSize)
        {
            var list = carRepository.Table;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                list = list.Where(c => c.Name.Contains(keyword.Trim()));
            }
            return list.ToPagedList(m => m.ID, pageNumber, pageSize);
        }

        public void UpdateCar(Car car)
        {
            carRepository.Update(car);
        }
    }
}
