using System.Collections.Generic;
using CarRentalSystem.Models;

namespace CarRentalSystem.Repositories
{
    public interface ICarRepository
    {
        void AddCar(Car car);
        Car GetCarById(Guid carId);
        List<Car> GetAvailableCars();
        void UpdateCarAvailability(Guid carId, bool isAvailable);
        void DeleteCar(Guid carId);
    }

    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        void AddUser(User user);
        User GetUserById(Guid id);
    }
}
