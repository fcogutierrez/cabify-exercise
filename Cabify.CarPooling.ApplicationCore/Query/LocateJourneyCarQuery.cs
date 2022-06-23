namespace Cabify.CarPooling.Application.Query
{
    public sealed class LocateJourneyCarQuery
        : IQuery
    {
        public LocateJourneyCarQuery(int journeyId)
        {
            JourneyId = journeyId;
        }

        public int JourneyId { get; }
    }
}
