using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystemAPI
{
    public interface IEntityPrimaryProperties
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

    }
}
