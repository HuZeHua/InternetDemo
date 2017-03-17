
using CarManager.Core.Domain;

namespace CarManager.Service
{
    public interface IICarService
    {
        Car GetCar(int carID);

        void UpdateCar(Car car);

        void CreateCar(Car car);

        void DeleteCar(Car car);

        IPagedList<Car> GetCars(int pageNumber, int pageSize);
    }
}