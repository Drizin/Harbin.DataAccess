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
    [Table("PersonPhone", Schema = "Person")]
    public partial class PersonPhone
    {
        #region Members
        [Key]
        [Required]
        public int BusinessEntityId { get; set; }
        [Key]
        [Required]
        public string PhoneNumber { get; set; }
        [Key]
        [Required]
        public int PhoneNumberTypeId { get; set; }
        public DateTime ModifiedDate { get; set; }
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
            PersonPhone other = obj as PersonPhone;
            if (other == null) return false;

            if (BusinessEntityId != other.BusinessEntityId)
                return false;
            if (ModifiedDate != other.ModifiedDate)
                return false;
            if (PhoneNumber != other.PhoneNumber)
                return false;
            if (PhoneNumberTypeId != other.PhoneNumberTypeId)
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (BusinessEntityId == default(int) ? 0 : BusinessEntityId.GetHashCode());
                hash = hash * 23 + (ModifiedDate == default(DateTime) ? 0 : ModifiedDate.GetHashCode());
                hash = hash * 23 + (PhoneNumber == null ? 0 : PhoneNumber.GetHashCode());
                hash = hash * 23 + (PhoneNumberTypeId == default(int) ? 0 : PhoneNumberTypeId.GetHashCode());
                return hash;
            }
        }
        public static bool operator ==(PersonPhone left, PersonPhone right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PersonPhone left, PersonPhone right)
        {
            return !Equals(left, right);
        }

        #endregion Equals/GetHashCode
    }
}
