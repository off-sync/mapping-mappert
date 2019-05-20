﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using OffSync.Mapping.Mappert.Practises;
using OffSync.Mapping.Mappert.Practises.Validation;

namespace OffSync.Mapping.Mappert.MapperBuilders
{
    public partial interface IMapperBuilder<TSource, TTarget>
    {
        IMapperBuilder<TSource, TTarget> WithMappingDelegateBuilder(
            IMappingDelegateBuilder mappingDelegateBuilder);

        IMapperBuilder<TSource, TTarget> WithValidator(
            IMappingValidator validator);

        MappingItemsRuleBuilder<TFrom, TTarget> MapItems<TFrom>(
            Expression<Func<TSource, IEnumerable<TFrom>>> from);

        void IgnoreSource<TFrom>(
            Expression<Func<TSource, TFrom>> from);

        void IgnoreTarget<TTo>(
            Expression<Func<TTarget, TTo>> to);
    }
}
