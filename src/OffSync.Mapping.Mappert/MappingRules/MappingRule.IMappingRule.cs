using System;
using System.Collections.Generic;
using System.Reflection;

using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.MappingRules
{
    public sealed partial class MappingRule :
        IMappingRule
    {
        public IReadOnlyList<PropertyInfo> SourceProperties => _sourceProperties;

        public Type SourceItemsType { get; private set; } = null;

        public IReadOnlyList<PropertyInfo> TargetProperties => _targetProperties;

        public Type TargetItemsType { get; private set; } = null;

        public Delegate Builder { get; private set; } = null;

        public MappingRuleTypes Type { get; private set; } = MappingRuleTypes.MapToValue;
    }
}
