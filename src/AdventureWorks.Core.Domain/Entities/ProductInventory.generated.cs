﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AdventureWorks.Core.Domain.Entities
{
    [Table("ProductInventory", Schema = "Production")]
    public partial class ProductInventory
    {
        #region Members
        [Key]
        public int ProductId { get; set; }
        [Key]
        public short LocationId { get; set; }
        public byte Bin { get; set; }
        public DateTime ModifiedDate { get; set; }
        public short Quantity { get; set; }
        public Guid Rowguid { get; set; }
        public string Shelf { get; set; }
        #endregion Members

        #region Equals/GetHashCode
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            ProductInventory other = obj as ProductInventory;
            if (other == null) return false;

            if (Bin != other.Bin)
                return false;
            if (LocationId != other.LocationId)
                return false;
            if (ModifiedDate != other.ModifiedDate)
                return false;
            if (ProductId != other.ProductId)
                return false;
            if (Quantity != other.Quantity)
                return false;
            if (Rowguid != other.Rowguid)
                return false;
            if (Shelf != other.Shelf)
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (Bin == default(byte) ? 0 : Bin.GetHashCode());
                hash = hash * 23 + (LocationId == default(short) ? 0 : LocationId.GetHashCode());
                hash = hash * 23 + (ModifiedDate == default(DateTime) ? 0 : ModifiedDate.GetHashCode());
                hash = hash * 23 + (ProductId == default(int) ? 0 : ProductId.GetHashCode());
                hash = hash * 23 + (Quantity == default(short) ? 0 : Quantity.GetHashCode());
                hash = hash * 23 + (Rowguid == default(Guid) ? 0 : Rowguid.GetHashCode());
                hash = hash * 23 + (Shelf == null ? 0 : Shelf.GetHashCode());
                return hash;
            }
        }
        public static bool operator ==(ProductInventory left, ProductInventory right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProductInventory left, ProductInventory right)
        {
            return !Equals(left, right);
        }

        #endregion Equals/GetHashCode
    }
}
