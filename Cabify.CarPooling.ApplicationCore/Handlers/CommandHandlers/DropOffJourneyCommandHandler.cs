using Cabify.CarPooling.Application.Commands;
using Cabify.CarPooling.Application.Exceptions;
using Cabify.CarPooling.Application.Interfaces;
using Cabify.CarPooling.Domain.Entities;
using Cabify.CarPooling.Infra.Persistence;
using System.Linq;
using System.Threading.Tasks;

namespace Cabify.CarPooling.Application.Handlers.CommandHandlers
{
    public sealed class DropOffJourneyCommandHandler
        : ICommandHandler<DropOffJourneyCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DropOffJourneyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DropOffJourneyCommand command)
        {
            var journey = await _unitOfWork.FindOne<Journey>(j => j.Id == command.Id, j => j.Car);

            if (journey == null || journey.IsDropped)
            {
                throw new JourneyNotFoundException();
            }

            if (journey.Car != null)
            {
                var car = journey.Car;
                car.ReleaseSeats(journey.People);

                var nextJourney = await GetNextJourneyInQueue(car.FreeSeats);
                if (nextJourney != null)
                {
                    car.TakeSeats(nextJourney.People);
                    nextJourney.AssignCar(car.Id);
                }
            }

            journey.Drop();

            await _unitOfWork.SaveChanges();
        }

        private async Task<Journey> GetNextJourneyInQueue(int freeSeats)
        {
            var journeysInQueue =
                await _unitOfWork
                    .Find<Journey>(j =>
                        !j.CarId.HasValue &&
                        j.IsDropped == false &&
                        j.People <= freeSeats);

            var nextJourney = journeysInQueue.OrderBy(j => j.RequestedAt).FirstOrDefault();

            return nextJourney;
        }
    }
}