namespace Cabify.CarPooling.Application.Commands
{
    public sealed class DropOffJourneyCommand
        : ICommand
    {
        public DropOffJourneyCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
