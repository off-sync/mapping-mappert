/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

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

        public MappingRule()
        {
        }

        public override string ToString()
        {
            var sourceNames = string.Join(
                "', '",
                _sourceProperties.Select(pi => pi.Name));

            var targetNames = string.Join(
                "', '",
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
