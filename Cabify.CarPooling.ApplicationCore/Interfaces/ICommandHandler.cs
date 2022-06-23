using Cabify.CarPooling.Application.Commands;
using System.Threading.Tasks;

namespace Cabify.CarPooling.Application.Interfaces
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}
