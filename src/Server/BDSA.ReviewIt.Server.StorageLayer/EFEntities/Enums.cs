using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.StorageLayer.EFEntities
{
    public enum FieldType
    {
        STRING,
        INT,
        BOOL
    }

    public enum ExclusionCondition
    {
        INT_LT, // Less than
        INT_GT, // Greater than
        INT_LE, // Less than or equal
        INT_GE, // Greater than or equal
        INT_EQ, // Equal
        INT_NE, // Not equal
        BOOL,   // Always a check on equality
        STRING_EQ, // Simply .Equals
        STRING_IN // Contains
    }

    public enum ParticipantRole
    {
        RESEARCHER,
        TEAM_MANAGER
    }
}