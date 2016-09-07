using MediatR;
using System;

namespace SimpleCQRS
{
    public class InventoryCommandHandlers: 
        IRequestHandler<CreateInventoryItem, Unit>,
        IRequestHandler<DeactivateInventoryItem, Unit>,
        IRequestHandler<RemoveItemsFromInventory, Unit>,
        IRequestHandler<CheckInItemsToInventory, Unit>,
        IRequestHandler<RenameInventoryItem, Unit>
    {
        private readonly IRepository<InventoryItem> _repository;

        public InventoryCommandHandlers(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public Unit Handle(CreateInventoryItem message)
        {
            var item = new InventoryItem(message.InventoryItemId, message.Name);
            _repository.Save(item, -1);

            return Unit.Value;
        }

        public Unit Handle(DeactivateInventoryItem message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.Deactivate();
            _repository.Save(item, message.OriginalVersion);
            
            return Unit.Value;
        }

        public Unit Handle(RemoveItemsFromInventory message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.Remove(message.Count);
            _repository.Save(item, message.OriginalVersion);

            return Unit.Value;
        }

        public Unit Handle(CheckInItemsToInventory message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.CheckIn(message.Count);
            _repository.Save(item, message.OriginalVersion);

            return Unit.Value;
        }

        public Unit Handle(RenameInventoryItem message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.ChangeName(message.NewName);
            _repository.Save(item, message.OriginalVersion);

            return Unit.Value;
        }
    }
}
