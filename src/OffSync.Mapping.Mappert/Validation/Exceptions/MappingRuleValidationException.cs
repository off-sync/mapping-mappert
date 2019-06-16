/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Runtime.Serialization;

using OffSync.Mapping.Mappert.MappingRules;

namespace OffSync.Mapping.Mappert.Validation.Exceptions
{
    [Serializable]
    public sealed class MappingRuleValidationException :
        Exception
    {
        public MappingRule MappingRule { get; private set; }

        public MappingRuleValidationException(
            MappingRule mappingRule,
            string message,
            Exception exception) :
            base(message, exception)
        {
            MappingRule = mappingRule;
        }

        private MappingRuleValidationException(
            SerializationInfo info,
            StreamingContext context) :
            base(info, context)
        {
            MappingRule = (MappingRule)info.GetValue(
                nameof(MappingRule),
                typeof(MappingRule));
        }

        public override void GetObjectData(
            SerializationInfo info,
            StreamingContext context)
        {
            base.GetObjectData(
                info,
                context);

            info.AddValue(
                nameof(MappingRule),
                MappingRule);
        }
    }
}
