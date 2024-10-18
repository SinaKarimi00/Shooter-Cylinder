namespace Features.Observer.Application
{
    public interface IListener
    {
        public void ReactionToEvent(IEvent generatedEvent);
    }
}