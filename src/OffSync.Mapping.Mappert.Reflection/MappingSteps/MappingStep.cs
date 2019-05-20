using System;
using System.Reflection;

using OffSync.Mapping.Mappert.Practises.Builders;
using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.Reflection.MappingSteps
{
    public sealed class MappingStep
    {
        public PropertyInfo[] SourceProperties { get; set; }

        public PropertyInfo[] TargetProperties { get; set; }

        public MappingRuleTypes MappingRuleType { get; set; }

        public Delegate Builder { get; set; }

        public MethodInfo BuilderInvoke { get; set; }

        public BuilderTypes BuilderType { get; set; }

        public FieldInfo[] ValueTupleFields { get; set; }

        public Type TargetItemType { get; set; }
    }
}
