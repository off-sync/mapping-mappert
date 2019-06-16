/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.MappingRules
{
    [Serializable]
    public sealed partial class MappingRule :
        ISerializable
    {
        private MappingRule(
            SerializationInfo info,
            StreamingContext context)
        {
            _sourceProperties = DeserializeProperties(
                info.GetString(nameof(_sourceProperties)));

            SourceItemsType = DeserializeType(
                info.GetString(nameof(SourceItemsType)));

            _targetProperties = DeserializeProperties(
                info.GetString(nameof(_targetProperties)));

            TargetItemsType = DeserializeType(
                info.GetString(nameof(TargetItemsType)));

            Builder = null; // delegates cannot be serialized

            Type = (MappingRuleTypes)info.GetInt32(nameof(Type));
        }

        public void GetObjectData(
            SerializationInfo info,
            StreamingContext context)
        {
            info.AddValue(
                nameof(_sourceProperties),
                SerializeProperties(_sourceProperties));

            info.AddValue(
                nameof(SourceItemsType),
                SerializeType(SourceItemsType));

            info.AddValue(
                nameof(_targetProperties),
                SerializeProperties(_targetProperties));

            info.AddValue(
                nameof(TargetItemsType),
                SerializeType(TargetItemsType));

            // skip Builder: delegates cannot be serialized

            info.AddValue(
                nameof(Type),
                (int)Type);
        }

        private static string SerializeProperties(
            List<PropertyInfo> properties)
        {
            return string.Join(
                ";",
                properties
                    .Select(pi => $"{SerializeType(pi.DeclaringType)}|{pi.Name}"));
        }

        private static List<PropertyInfo> DeserializeProperties(
            string properties)
        {
            return properties
                .Split(';')
                .Select(s =>
                {
                    var ss = s.Split('|');

                    return DeserializeType(ss[0])
                        .GetProperty(ss[1]);
                })
                .ToList();
        }

        private static string SerializeType(
            Type type)
        {
            return type?.AssemblyQualifiedName;
        }

        private static Type DeserializeType(
            string type)
        {
            if (type == null)
            {
                return null;
            }

            return System.Type.GetType(type);
        }
    }
}
