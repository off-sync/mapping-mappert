/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Reflection;

using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.MappingRules
{
    public sealed partial class MappingRule
    {
        public MappingRule WithSourceItems(
            PropertyInfo sourceProperty,
            Type sourceItemsType)
        {
            #region Pre-conditions
            if (sourceItemsType == null)
            {
                throw new ArgumentNullException(nameof(sourceItemsType));
            }
            #endregion

            if (SourceItemsType != null)
            {
                throw new InvalidOperationException(
                    $"{nameof(SourceItemsType)} can only be set once");
            }

            SourceItemsType = sourceItemsType;

            return WithSource(sourceProperty);
        }

        public MappingRule WithSource(
            PropertyInfo sourceProperty)
        {
            #region Pre-conditions
            if (sourceProperty == null)
            {
                throw new ArgumentNullException(nameof(sourceProperty));
            }
            #endregion

            _sourceProperties.Add(sourceProperty);

            return this;
        }

        public MappingRule WithTargetItems(
            PropertyInfo targetProperty,
            Type targetItemsType)
        {
            #region Pre-conditions
            if (targetItemsType == null)
            {
                throw new ArgumentNullException(nameof(targetItemsType));
            }
            #endregion

            if (TargetItemsType != null)
            {
                throw new InvalidOperationException(
                    $"{nameof(TargetItemsType)} can only be set once");
            }

            TargetItemsType = targetItemsType;

            return WithTarget(targetProperty);
        }

        public MappingRule WithTarget(
            PropertyInfo targetProperty)
        {
            #region Pre-conditions
            if (targetProperty == null)
            {
                throw new ArgumentNullException(nameof(targetProperty));
            }
            #endregion

            _targetProperties.Add(targetProperty);

            return this;
        }

        public MappingRule WithBuilder(
            Delegate builder)
        {
            #region Pre-conditions
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            #endregion

            Builder = builder;

            return this;
        }

        public MappingRule WithType(
            MappingRuleTypes type)
        {
            Type = type;

            return this;
        }
    }
}
