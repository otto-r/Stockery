using Prism.Events;

namespace Stockery.Event
{
    public class AfterBondSavedEvent : PubSubEvent<AfterBondSavedEventArgs>
    {

    }

    public class AfterBondSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }
    }
}
