using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.MappingRules
{
    public sealed partial class MappingRule
    {
        private readonly List<PropertyInfo> _sourceProperties = new List<PropertyInfo>();

        private readonly List<PropertyInfo> _targetProperties = new List<PropertyInfo>();

        public override bool Equals(object obj)
        {
            return obj is MappingRule rule &&
                EqualityComparer<List<PropertyInfo>>.Default.Equals(_sourceProperties, rule._sourceProperties) &&
                EqualityComparer<Type>.Default.Equals(SourceItemsType, rule.SourceItemsType) &&
                EqualityComparer<List<PropertyInfo>>.Default.Equals(_targetProperties, rule._targetProperties) &&
                EqualityComparer<Type>.Default.Equals(TargetItemsType, rule.TargetItemsType) &&
                EqualityComparer<Delegate>.Default.Equals(Builder, rule.Builder) &&
                Type == rule.Type;
        }

        public override int GetHashCode()
        {
            var hashCode = 1528579089;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<PropertyInfo>>.Default.GetHashCode(_sourceProperties);
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(SourceItemsType);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<PropertyInfo>>.Default.GetHashCode(_targetProperties);
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(TargetItemsType);
            hashCode = hashCode * -1521134295 + EqualityComparer<Delegate>.Default.GetHashCode(Builder);
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            var sourceNames = string.Join(
                ", ",
                _sourceProperties.Select(pi => pi.Name));

            var targetNames = string.Join(
                ", ",
                _targetProperties.Select(pi => pi.Name));

            var builderIndication = Builder == null ? "" : "*";

            var itemTypes =
                Type == MappingRuleTypes.MapToArray ||
                    Type == MappingRuleTypes.MapToCollection ?
                $" ('{SourceItemsType.Name}' -> '{TargetItemsType.Name}')" :
                "";

            return $"[{Type}{builderIndication}: '{sourceNames}' -> '{targetNames}'{itemTypes}]";
        }
    }
}
