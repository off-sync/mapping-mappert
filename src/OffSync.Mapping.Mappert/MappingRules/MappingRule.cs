using System;
using System.Collections.Generic;
using System.Reflection;

namespace OffSync.Mapping.Mappert.MappingRules
{
    public sealed class MappingRule
    {
        private readonly List<PropertyInfo> _sourceProperties = new List<PropertyInfo>();

        public IReadOnlyList<PropertyInfo> SourceProperties => _sourceProperties;

        private readonly List<Type> _sourceTypes = new List<Type>();

        public IReadOnlyList<Type> SourceTypes => _sourceTypes;

        private readonly List<PropertyInfo> _targetProperties = new List<PropertyInfo>();

        public IReadOnlyList<PropertyInfo> TargetProperties => _targetProperties;

        private readonly List<Type> _targetTypes = new List<Type>();

        public IReadOnlyList<Type> TargetTypes => _targetTypes;

        public Delegate Builder { get; private set; }

        public MappingStrategies MappingStrategy { get; private set; } = MappingStrategies.MapToValue;

        public MappingRule WithSource(
            PropertyInfo sourceProperty,
            Type sourceType = null)
        {
            _sourceProperties.Add(sourceProperty);

            _sourceTypes.Add(sourceType ?? sourceProperty.PropertyType);

            return this;
        }

        public MappingRule WithTarget(
            PropertyInfo targetProperty,
            Type targetType = null)
        {
            _targetProperties.Add(targetProperty);

            _targetTypes.Add(targetType ?? targetProperty.PropertyType);

            return this;
        }

        public MappingRule WithBuilder(
            Delegate builder)
        {
            Builder = builder;

            return this;
        }

        public MappingRule WithStrategy(
            MappingStrategies strategy)
        {
            MappingStrategy = strategy;

            return this;
        }
    }
}
