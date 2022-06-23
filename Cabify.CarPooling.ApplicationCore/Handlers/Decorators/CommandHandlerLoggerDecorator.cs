using Cabify.CarPooling.Application.Commands;
using Cabify.CarPooling.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Cabify.CarPooling.Application.Handlers.Decorators
{
    public sealed class CommandHandlerLoggerDecorator<TCommand>
        : ICommandHandler<TCommand>
        where TCommand: ICommand
    {
        private readonly ICommandHandler<TCommand> _commandHandler;
        private readonly ILogger<CommandHandlerLoggerDecorator<TCommand>> _logger;

        public CommandHandlerLoggerDecorator(
            ICommandHandler<TCommand> commandHandler, 
            ILogger<CommandHandlerLoggerDecorator<TCommand>> logger)
        {
            _commandHandler = commandHandler;
            _logger = logger;
        }

        public async Task Handle(TCommand command)
        {
            _logger.LogInformation($"Processing Command {typeof(TCommand).Name}: {command}");

            try
            {
                await _commandHandler.Handle(command);
                _logger.LogInformation($"Command {typeof(TCommand).Name} was successfully processed");
            }
            catch (Exception exception)
            {
                _logger.LogError($"Unhandled exception when processing Command {typeof(TCommand).Name}: {exception}");
                throw;
            }
        }
    }
}
