/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

using OffSync.Mapping.Mappert.Practises.Common;

namespace OffSync.Mapping.Mappert.Practises.Configuration
{
    [AttributeUsage(
        AttributeTargets.Assembly,
        AllowMultiple = true)]
    public sealed class RegisterMappingDelegateBuilderAttribute :
        Attribute
    {
        public Type BuilderType { get; }

        public int Preference { get; set; } = 0;

        public RegisterMappingDelegateBuilderAttribute(
            Type builderType)
        {
            #region Pre-conditions
            ConfigurationUtil.EnsureValidMappingDelegateBuilderType(builderType);
            #endregion

            BuilderType = builderType;
        }
    }
}
