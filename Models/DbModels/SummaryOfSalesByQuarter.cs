using System;
using System.Collections.Generic;

namespace Lab_10_DB_SQL_ORM.Models.DbModels
{
    public partial class SummaryOfSalesByQuarter
    {
        public DateTime? ShippedDate { get; set; }
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
