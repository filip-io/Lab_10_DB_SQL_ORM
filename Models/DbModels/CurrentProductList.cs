﻿using System;
using System.Collections.Generic;

namespace Lab_10_DB_SQL_ORM.Models.DbModels
{
    public partial class CurrentProductList
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
    }
}
