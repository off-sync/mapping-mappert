using System;
using System.Collections.Generic;
using System.Reflection;

namespace OffSync.Mapping.Mappert.MappingRules
{
    public sealed class MappingRule
    {
        private readonly List<PropertyInfo> _sourceProperties = new List<PropertyInfo>();

        public IReadOnlyList<PropertyInfo> SourceProperties => _sourceProperties;

        private readonly List<PropertyInfo> _targetProperties = new List<PropertyInfo>();

        public IReadOnlyList<PropertyInfo> TargetProperties => _targetProperties;

        public Delegate Builder { get; private set; }

        public MappingRule WithSource(
            PropertyInfo sourceProperty)
        {
            _sourceProperties.Add(sourceProperty);

            return this;
        }

        public MappingRule WithTarget(
            PropertyInfo targetProperty)
        {
            _targetProperties.Add(targetProperty);

            return this;
        }

        public MappingRule WithBuilder(
            Delegate builder)
        {
            Builder = builder;

            return this;
        }
    }
}
