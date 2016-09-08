using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCQRS
{
    public class InventoryItemService
    {
        private ReadModelFacade _readmodel;
        private readonly IRepository<InventoryItem> _repository;

        public InventoryItemService(ReadModelFacade readmodel, IRepository<InventoryItem> repository)
        {
            _readmodel = readmodel;
            _repository = repository;
        }
    }
}
