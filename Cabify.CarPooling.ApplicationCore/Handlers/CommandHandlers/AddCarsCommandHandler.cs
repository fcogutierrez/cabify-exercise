using Cabify.CarPooling.Application.Commands;
using Cabify.CarPooling.Application.Interfaces;
using Cabify.CarPooling.Domain.Entities;
using Cabify.CarPooling.Infra.Persistence;
using System.Linq;
using System.Threading.Tasks;

namespace Cabify.CarPooling.Application.Handlers.CommandHandlers
{
    public sealed class AddCarsCommandHandler
        : ICommandHandler<AddCarsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCarsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AddCarsCommand command)
        {
            _unitOfWork.DeleteAll<Car>();
            _unitOfWork.DeleteAll<Journey>();

            var cars = command.Cars.Select(c => Car.Create(c.Id, c.Seats));
            await _unitOfWork.AddRange(cars);

            await _unitOfWork.SaveChanges();
        }
    }
}
