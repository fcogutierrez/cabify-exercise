using Cabify.CarPooling.Application.Commands;
using Cabify.CarPooling.Application.Interfaces;
using Cabify.CarPooling.Domain.Entities;
using Cabify.CarPooling.Infra.Persistence;
using System.Linq;
using System.Threading.Tasks;

namespace Cabify.CarPooling.Application.Handlers.CommandHandlers
{
    public sealed class AddJourneyCommandHandler
        : ICommandHandler<AddJourneyCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddJourneyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AddJourneyCommand command)
        {
            int? carId = default;

            var availableCar = await GetAvailableCar(command.People);
            if (availableCar != null)
            {
                availableCar.TakeSeats(command.People);
                carId = availableCar.Id;
            }

            var journey = Journey.Create(command.Id, command.People, carId);
            await _unitOfWork.Add(journey);

            await _unitOfWork.SaveChanges();
        }

        private async Task<Car> GetAvailableCar(int people)
        {
            var availableCars = await _unitOfWork.Find<Car>(c => c.FreeSeats >= people);
            var availableCar = availableCars.OrderBy(c => c.LastUpdatedAt).FirstOrDefault();

            return availableCar;
        }
    }
}
