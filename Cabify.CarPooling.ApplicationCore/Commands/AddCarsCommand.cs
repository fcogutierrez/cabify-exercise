using System.Collections.Generic;
using Cabify.CarPooling.Application.Dtos;

namespace Cabify.CarPooling.Application.Commands
{
    public sealed class AddCarsCommand
        : ICommand
    {
        public AddCarsCommand(IEnumerable<CarDto> cars)
        {
            Cars = cars;
        }

        public IEnumerable<CarDto> Cars { get; }
    }
}
