using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using OffSync.Mapping.Mappert.Common;
using OffSync.Mapping.Mappert.Interfaces;

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
        private readonly IMappingDelegateBuilder _mappingDelegateBuilder;

        public IReadOnlyList<Type> TargetTypes => _targetTypes;

        public Delegate Builder { get; private set; }

        public FieldInfo[] BuilderValueTupleFields { get; private set; }

        public MappingStrategies MappingStrategy { get; private set; } = MappingStrategies.MapToValue;

        public MappingRule()
        {
        }

        public MappingRule(
            IMappingDelegateBuilder mappingDelegateBuilder)
        {
            _mappingDelegateBuilder = mappingDelegateBuilder;
        }

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

            if (MappingStrategy == MappingStrategies.MapToValue) // FIXME support all strategies
            {
                var builderType = BuilderUtil.GetBuilderType(
                    _sourceProperties.ToArray(),
                    _targetProperties.ToArray(),
                    builder);

                if (builderType == BuilderTypes.ValueTuple)
                {
                    var returnType = builder.Method.ReturnType;

                    BuilderValueTupleFields = Enumerable
                        .Range(1, _targetProperties.Count)
                        .Select(i => returnType.GetField($"Item{i}"))
                        .ToArray();
                }
            }

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
