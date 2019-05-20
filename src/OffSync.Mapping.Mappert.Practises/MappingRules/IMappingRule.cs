using System;
using System.Collections.Generic;
using System.Reflection;

namespace OffSync.Mapping.Mappert.Practises.MappingRules
{
    public interface IMappingRule
    {
        IReadOnlyList<PropertyInfo> SourceProperties { get; }

        Type SourceItemsType { get; }

        IReadOnlyList<PropertyInfo> TargetProperties { get; }

        Type TargetItemsType { get; }

        MappingRuleTypes Type { get; }

        Delegate Builder { get; }
    }
}
