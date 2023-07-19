using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access
{
    public class Sale
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SellerId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public override string ToString()
        {
            return $"{Id}, {CustomerId}, {SellerId}, {Date}, {Amount}$";
        }
    }
}
