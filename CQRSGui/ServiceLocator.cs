using SimpleCQRS;

namespace CQRSGui
{
    public static class ServiceLocator
    {
        public static MediatR.Mediator Mediator { get; set; }
        public static Repository<InventoryItem> InventoryRepository { get; set; }
    }
}