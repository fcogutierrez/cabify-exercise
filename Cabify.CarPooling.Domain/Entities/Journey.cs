using System;

namespace Cabify.CarPooling.Domain.Entities
{
    public sealed class Journey
        : BaseEntity
    {
        public int People { get; private set; }
        public bool IsDropped { get; private set; }
        public DateTime RequestedAt { get; private set; }
        public int? CarId { get; private set; }
        public Car Car { get; private set; }

        public static Journey Create(int id, int people, int? carId)
        {
            if (people < 1 || people > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(people));
            }

            var journey =
                new Journey
                {
                    Id = id,
                    People = people,
                    RequestedAt = DateTime.UtcNow,
                    CarId = carId
                };

            return journey;
        }

        public void AssignCar(int carId)
        {
            CarId = carId;
        }

        public void Drop()
        {
            IsDropped = true;
        }
    }
}