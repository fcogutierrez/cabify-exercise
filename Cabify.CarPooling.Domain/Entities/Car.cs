using System;
using System.Collections.Generic;
using System.Linq;

namespace Cabify.CarPooling.Domain.Entities
{
    public sealed class Car
        : BaseEntity
    {
        public int Seats { get; private set; }
        public int FreeSeats { get; private set; }
        public DateTime LastUpdatedAt { get; private set; }
        public IEnumerable<Journey> Journeys { get; private set; }

        public static Car Create(int id, int seats)
        {
            if (seats < 4 || seats > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(seats));
            }

            var car =
                new Car
                {
                    Id = id,
                    Seats = seats,
                    FreeSeats = seats,
                    LastUpdatedAt = DateTime.UtcNow,
                    Journeys = Enumerable.Empty<Journey>()
                };

            return car;
        }

        public void TakeSeats(int seatsToTake)
        {
            FreeSeats -= seatsToTake;
            LastUpdatedAt = DateTime.UtcNow;
        }

        public void ReleaseSeats(int seatsToRelease)
        {
            FreeSeats += seatsToRelease;
            LastUpdatedAt = DateTime.UtcNow;
        }
    }
}