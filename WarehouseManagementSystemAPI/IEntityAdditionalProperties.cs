using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystemAPI
{
    public interface IEntityAdditionalProperties
    {
        int Quantity { get; set; }
        decimal UnitValue { get; set; }
    }
}
