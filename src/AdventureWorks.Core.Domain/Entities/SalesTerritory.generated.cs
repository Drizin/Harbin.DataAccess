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
    [Table("SalesTerritory", Schema = "Sales")]
    public partial class SalesTerritory
    {
        #region Members
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TerritoryId { get; set; }
        public decimal CostLastYear { get; set; }
        public decimal CostYtd { get; set; }
        public string CountryRegionCode { get; set; }
        public string Group { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Name { get; set; }
        public Guid Rowguid { get; set; }
        public decimal SalesLastYear { get; set; }
        public decimal SalesYtd { get; set; }
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
            SalesTerritory other = obj as SalesTerritory;
            if (other == null) return false;

            if (CostLastYear != other.CostLastYear)
                return false;
            if (CostYtd != other.CostYtd)
                return false;
            if (CountryRegionCode != other.CountryRegionCode)
                return false;
            if (Group != other.Group)
                return false;
            if (ModifiedDate != other.ModifiedDate)
                return false;
            if (Name != other.Name)
                return false;
            if (Rowguid != other.Rowguid)
                return false;
            if (SalesLastYear != other.SalesLastYear)
                return false;
            if (SalesYtd != other.SalesYtd)
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (CostLastYear == default(decimal) ? 0 : CostLastYear.GetHashCode());
                hash = hash * 23 + (CostYtd == default(decimal) ? 0 : CostYtd.GetHashCode());
                hash = hash * 23 + (CountryRegionCode == null ? 0 : CountryRegionCode.GetHashCode());
                hash = hash * 23 + (Group == null ? 0 : Group.GetHashCode());
                hash = hash * 23 + (ModifiedDate == default(DateTime) ? 0 : ModifiedDate.GetHashCode());
                hash = hash * 23 + (Name == null ? 0 : Name.GetHashCode());
                hash = hash * 23 + (Rowguid == default(Guid) ? 0 : Rowguid.GetHashCode());
                hash = hash * 23 + (SalesLastYear == default(decimal) ? 0 : SalesLastYear.GetHashCode());
                hash = hash * 23 + (SalesYtd == default(decimal) ? 0 : SalesYtd.GetHashCode());
                return hash;
            }
        }
        public static bool operator ==(SalesTerritory left, SalesTerritory right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SalesTerritory left, SalesTerritory right)
        {
            return !Equals(left, right);
        }

        #endregion Equals/GetHashCode
    }
}
