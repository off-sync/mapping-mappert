/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Runtime.Serialization;

namespace OffSync.Mapping.Mappert.Validation.Exceptions
{
    [Serializable]
    public sealed class MappingRuleSetValidationException :
        Exception
    {
        public MappingRuleSetValidationException(
            string message,
            Exception innerException) :
            base(message, innerException)
        {
        }

        private MappingRuleSetValidationException(
            SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
    }
}
