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
    public partial class AWBuildVersion
    {
        #region Members
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte SystemInformationId { get; set; }
        [Column("Database Version")]
        public string DatabaseVersion { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime VersionDate { get; set; }
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
            AWBuildVersion other = obj as AWBuildVersion;
            if (other == null) return false;

            if (DatabaseVersion != other.DatabaseVersion)
                return false;
            if (ModifiedDate != other.ModifiedDate)
                return false;
            if (VersionDate != other.VersionDate)
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (DatabaseVersion == null ? 0 : DatabaseVersion.GetHashCode());
                hash = hash * 23 + (ModifiedDate == default(DateTime) ? 0 : ModifiedDate.GetHashCode());
                hash = hash * 23 + (VersionDate == default(DateTime) ? 0 : VersionDate.GetHashCode());
                return hash;
            }
        }
        public static bool operator ==(AWBuildVersion left, AWBuildVersion right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AWBuildVersion left, AWBuildVersion right)
        {
            return !Equals(left, right);
        }

        #endregion Equals/GetHashCode
    }
}
