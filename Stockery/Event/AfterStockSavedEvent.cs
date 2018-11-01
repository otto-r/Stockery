using Prism.Events;

namespace Stockery.Event
{
    public class AfterStockSavedEvent : PubSubEvent<AfterStockSavedEventArgs>
    {

    }

    public class AfterStockSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }

    }
}
