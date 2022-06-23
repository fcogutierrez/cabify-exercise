namespace Cabify.CarPooling.Application.Commands
{
    public sealed class AddJourneyCommand
        : ICommand
    {
        public AddJourneyCommand(int id, int people)
        {
            Id = id;
            People = people;
        }

        public int Id { get; }
        public int People { get; }
    }
}
