
using CarManager.Core.Domain;

namespace CarManager.Service
{
    public class CarService : IICarService
    {
        private readonly IRepository<Car> carRepository;

        public CarService(IRepository<Car> carRepository)
        {
            this.carRepository = carRepository;
        }

        public void CreateCar(Car car)
        {
            carRepository.Insert(car);
        }

        public void DeleteCar(Car car)
        {
            carRepository.Delete(car);
        }

        public Car GetCar(int carID)
        {
            return carRepository.GetById(carID);
        }

        public IPagedList<Car> GetCars(int pageNumber, int pageSize)
        {
            return carRepository.Table.ToPagedList(m => m.ID, pageNumber, pageSize);
        }

        public void UpdateCar(Car car)
        {
            carRepository.Update(car);
        }
    }
}