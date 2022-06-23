using Cabify.CarPooling.Application.Dtos;
using Cabify.CarPooling.Application.Exceptions;
using Cabify.CarPooling.Application.Interfaces;
using Cabify.CarPooling.Application.Query;
using Cabify.CarPooling.Domain.Entities;
using Cabify.CarPooling.Infra.Persistence;
using System.Threading.Tasks;

namespace Cabify.CarPooling.Application.Handlers.QueryHandlers
{
    public sealed class LocateJourneyCarQueryHandler
        : IQueryHandler<LocateJourneyCarQuery, CarDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LocateJourneyCarQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CarDto> Handle(LocateJourneyCarQuery query)
        {
            var journey = await _unitOfWork.FindOne<Journey>(j => j.Id == query.JourneyId, j => j.Car);

            if (journey == null || journey.IsDropped)
            {
                throw new JourneyNotFoundException();
            }

            if (journey.Car == null)
            {
                throw new JourneyWithUnassignedCarException();
            }

            var carDto =
                new CarDto
                {
                    Id = journey.Car.Id,
                    Seats = journey.Car.Seats
                };

            return carDto;
        }
    }
}