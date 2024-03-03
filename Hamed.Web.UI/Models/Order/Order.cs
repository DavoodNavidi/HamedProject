using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hamed.Web.UI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderAmount { get; set; }
        public string UserId { get; set; }
    }
}
