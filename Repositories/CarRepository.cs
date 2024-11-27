using System.Collections.Generic;
using System.Linq;
using CarRentalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddCar(Car car)
        {
            _context.Cars.Add(car);
        }

        public Car GetCarById(Guid carId)
        {
            return _context.Cars.Find(carId);
        }

        public List<Car> GetAvailableCars()
        {
            return _context.Cars.Where(car => car.IsAvailable).ToList();
        }

        public void UpdateCarAvailability(Guid carId, bool isAvailable)
        {
            var car = _context.Cars.Find(carId);
            if (car != null)
            {
                car.IsAvailable = isAvailable;
                _context.SaveChanges();
            }
        }

        public void DeleteCar(Guid carId) 
        {
            var car = _context.Cars.Find(carId);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }
        }
    }
}
