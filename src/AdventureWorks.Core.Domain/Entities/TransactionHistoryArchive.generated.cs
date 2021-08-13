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
    [Table("TransactionHistoryArchive", Schema = "Production")]
    public partial class TransactionHistoryArchive
    {
        #region Members
        [Key]
        [Required]
        public int TransactionId { get; set; }
        public decimal ActualCost { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int ReferenceOrderId { get; set; }
        public int ReferenceOrderLineId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
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
            TransactionHistoryArchive other = obj as TransactionHistoryArchive;
            if (other == null) return false;

            if (ActualCost != other.ActualCost)
                return false;
            if (ModifiedDate != other.ModifiedDate)
                return false;
            if (ProductId != other.ProductId)
                return false;
            if (Quantity != other.Quantity)
                return false;
            if (ReferenceOrderId != other.ReferenceOrderId)
                return false;
            if (ReferenceOrderLineId != other.ReferenceOrderLineId)
                return false;
            if (TransactionDate != other.TransactionDate)
                return false;
            if (TransactionId != other.TransactionId)
                return false;
            if (TransactionType != other.TransactionType)
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (ActualCost == default(decimal) ? 0 : ActualCost.GetHashCode());
                hash = hash * 23 + (ModifiedDate == default(DateTime) ? 0 : ModifiedDate.GetHashCode());
                hash = hash * 23 + (ProductId == default(int) ? 0 : ProductId.GetHashCode());
                hash = hash * 23 + (Quantity == default(int) ? 0 : Quantity.GetHashCode());
                hash = hash * 23 + (ReferenceOrderId == default(int) ? 0 : ReferenceOrderId.GetHashCode());
                hash = hash * 23 + (ReferenceOrderLineId == default(int) ? 0 : ReferenceOrderLineId.GetHashCode());
                hash = hash * 23 + (TransactionDate == default(DateTime) ? 0 : TransactionDate.GetHashCode());
                hash = hash * 23 + (TransactionId == default(int) ? 0 : TransactionId.GetHashCode());
                hash = hash * 23 + (TransactionType == null ? 0 : TransactionType.GetHashCode());
                return hash;
            }
        }
        public static bool operator ==(TransactionHistoryArchive left, TransactionHistoryArchive right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TransactionHistoryArchive left, TransactionHistoryArchive right)
        {
            return !Equals(left, right);
        }

        #endregion Equals/GetHashCode
    }
}
