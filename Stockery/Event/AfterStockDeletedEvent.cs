using Prism.Events;

namespace Stockery.Event
{
    public    class AfterStockDeletedEvent:PubSubEvent<int>
    {
    }
}
