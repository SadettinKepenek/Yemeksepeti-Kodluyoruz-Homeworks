using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace Homework_5.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public int PersonID { get; set; }
        public int StoreID { get; set; }
        public int TerritoryID { get; set; }
        public string AccountNumber { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public SalesTerritory SalesTerritory { get; set; }
        public Person Person { get; set; }
    }
}