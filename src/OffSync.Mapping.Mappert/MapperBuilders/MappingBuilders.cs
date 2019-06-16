/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

using OffSync.Mapping.Mappert.MappingRules;

namespace OffSync.Mapping.Mappert.MapperBuilders
{
    public partial interface IMapperBuilder<TSource, TTarget>
    {

        MappingRuleBuilderFrom1<TFrom1, TTarget> Map<TFrom1>(
            Expression<Func<TSource, TFrom1>> from1);

        MappingRuleBuilderFrom2<TFrom1, TFrom2, TTarget> Map<TFrom1, TFrom2>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2);

        MappingRuleBuilderFrom3<TFrom1, TFrom2, TFrom3, TTarget> Map<TFrom1, TFrom2, TFrom3>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2,
            Expression<Func<TSource, TFrom3>> from3);

        MappingRuleBuilderFrom4<TFrom1, TFrom2, TFrom3, TFrom4, TTarget> Map<TFrom1, TFrom2, TFrom3, TFrom4>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2,
            Expression<Func<TSource, TFrom3>> from3,
            Expression<Func<TSource, TFrom4>> from4);

        MappingRuleBuilderFrom5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTarget> Map<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2,
            Expression<Func<TSource, TFrom3>> from3,
            Expression<Func<TSource, TFrom4>> from4,
            Expression<Func<TSource, TFrom5>> from5);

        MappingRuleBuilderFrom6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTarget> Map<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2,
            Expression<Func<TSource, TFrom3>> from3,
            Expression<Func<TSource, TFrom4>> from4,
            Expression<Func<TSource, TFrom5>> from5,
            Expression<Func<TSource, TFrom6>> from6);

        MappingRuleBuilderFrom7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTarget> Map<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2,
            Expression<Func<TSource, TFrom3>> from3,
            Expression<Func<TSource, TFrom4>> from4,
            Expression<Func<TSource, TFrom5>> from5,
            Expression<Func<TSource, TFrom6>> from6,
            Expression<Func<TSource, TFrom7>> from7);
    }

    public partial class MapperBuilder<TSource, TTarget> :
        IMapperBuilder<TSource, TTarget>
    {

        [ExcludeFromCodeCoverage]
        MappingRuleBuilderFrom1<TFrom1, TTarget> IMapperBuilder<TSource, TTarget>.Map<TFrom1>(
            Expression<Func<TSource, TFrom1>> from1)
        {
            var mappingRule = AddMappingRule()
                .WithSource(from1);

            return new MappingRuleBuilderFrom1<TFrom1, TTarget>(mappingRule);
        }

        [ExcludeFromCodeCoverage]
        protected MappingRuleBuilderFrom1<TFrom1, TTarget> Map<TFrom1>(
            Expression<Func<TSource, TFrom1>> from1)
            => ((IMapperBuilder<TSource, TTarget>)this).Map(
                from1);


        [ExcludeFromCodeCoverage]
        MappingRuleBuilderFrom2<TFrom1, TFrom2, TTarget> IMapperBuilder<TSource, TTarget>.Map<TFrom1, TFrom2>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2)
        {
            var mappingRule = AddMappingRule()
                .WithSource(from1)
                .WithSource(from2);

            return new MappingRuleBuilderFrom2<TFrom1, TFrom2, TTarget>(mappingRule);
        }

        [ExcludeFromCodeCoverage]
        protected MappingRuleBuilderFrom2<TFrom1, TFrom2, TTarget> Map<TFrom1, TFrom2>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2)
            => ((IMapperBuilder<TSource, TTarget>)this).Map(
                from1,
                from2);


        [ExcludeFromCodeCoverage]
        MappingRuleBuilderFrom3<TFrom1, TFrom2, TFrom3, TTarget> IMapperBuilder<TSource, TTarget>.Map<TFrom1, TFrom2, TFrom3>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2,
            Expression<Func<TSource, TFrom3>> from3)
        {
            var mappingRule = AddMappingRule()
                .WithSource(from1)
                .WithSource(from2)
                .WithSource(from3);

            return new MappingRuleBuilderFrom3<TFrom1, TFrom2, TFrom3, TTarget>(mappingRule);
        }

        [ExcludeFromCodeCoverage]
        protected MappingRuleBuilderFrom3<TFrom1, TFrom2, TFrom3, TTarget> Map<TFrom1, TFrom2, TFrom3>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2,
            Expression<Func<TSource, TFrom3>> from3)
            => ((IMapperBuilder<TSource, TTarget>)this).Map(
                from1,
                from2,
                from3);


        [ExcludeFromCodeCoverage]
        MappingRuleBuilderFrom4<TFrom1, TFrom2, TFrom3, TFrom4, TTarget> IMapperBuilder<TSource, TTarget>.Map<TFrom1, TFrom2, TFrom3, TFrom4>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2,
            Expression<Func<TSource, TFrom3>> from3,
            Expression<Func<TSource, TFrom4>> from4)
        {
            var mappingRule = AddMappingRule()
                .WithSource(from1)
                .WithSource(from2)
                .WithSource(from3)
                .WithSource(from4);

            return new MappingRuleBuilderFrom4<TFrom1, TFrom2, TFrom3, TFrom4, TTarget>(mappingRule);
        }

        [ExcludeFromCodeCoverage]
        protected MappingRuleBuilderFrom4<TFrom1, TFrom2, TFrom3, TFrom4, TTarget> Map<TFrom1, TFrom2, TFrom3, TFrom4>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2,
            Expression<Func<TSource, TFrom3>> from3,
            Expression<Func<TSource, TFrom4>> from4)
            => ((IMapperBuilder<TSource, TTarget>)this).Map(
                from1,
                from2,
                from3,
                from4);


        [ExcludeFromCodeCoverage]
        MappingRuleBuilderFrom5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTarget> IMapperBuilder<TSource, TTarget>.Map<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2,
            Expression<Func<TSource, TFrom3>> from3,
            Expression<Func<TSource, TFrom4>> from4,
            Expression<Func<TSource, TFrom5>> from5)
        {
            var mappingRule = AddMappingRule()
                .WithSource(from1)
                .WithSource(from2)
                .WithSource(from3)
                .WithSource(from4)
                .WithSource(from5);

            return new MappingRuleBuilderFrom5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTarget>(mappingRule);
        }

        [ExcludeFromCodeCoverage]
        protected MappingRuleBuilderFrom5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTarget> Map<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2,
            Expression<Func<TSource, TFrom3>> from3,
            Expression<Func<TSource, TFrom4>> from4,
            Expression<Func<TSource, TFrom5>> from5)
            => ((IMapperBuilder<TSource, TTarget>)this).Map(
                from1,
                from2,
                from3,
                from4,
                from5);


        [ExcludeFromCodeCoverage]
        MappingRuleBuilderFrom6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTarget> IMapperBuilder<TSource, TTarget>.Map<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2,
            Expression<Func<TSource, TFrom3>> from3,
            Expression<Func<TSource, TFrom4>> from4,
            Expression<Func<TSource, TFrom5>> from5,
            Expression<Func<TSource, TFrom6>> from6)
        {
            var mappingRule = AddMappingRule()
                .WithSource(from1)
                .WithSource(from2)
                .WithSource(from3)
                .WithSource(from4)
                .WithSource(from5)
                .WithSource(from6);

            return new MappingRuleBuilderFrom6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTarget>(mappingRule);
        }

        [ExcludeFromCodeCoverage]
        protected MappingRuleBuilderFrom6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTarget> Map<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2,
            Expression<Func<TSource, TFrom3>> from3,
            Expression<Func<TSource, TFrom4>> from4,
            Expression<Func<TSource, TFrom5>> from5,
            Expression<Func<TSource, TFrom6>> from6)
            => ((IMapperBuilder<TSource, TTarget>)this).Map(
                from1,
                from2,
                from3,
                from4,
                from5,
                from6);


        [ExcludeFromCodeCoverage]
        MappingRuleBuilderFrom7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTarget> IMapperBuilder<TSource, TTarget>.Map<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2,
            Expression<Func<TSource, TFrom3>> from3,
            Expression<Func<TSource, TFrom4>> from4,
            Expression<Func<TSource, TFrom5>> from5,
            Expression<Func<TSource, TFrom6>> from6,
            Expression<Func<TSource, TFrom7>> from7)
        {
            var mappingRule = AddMappingRule()
                .WithSource(from1)
                .WithSource(from2)
                .WithSource(from3)
                .WithSource(from4)
                .WithSource(from5)
                .WithSource(from6)
                .WithSource(from7);

            return new MappingRuleBuilderFrom7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTarget>(mappingRule);
        }

        [ExcludeFromCodeCoverage]
        protected MappingRuleBuilderFrom7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTarget> Map<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2,
            Expression<Func<TSource, TFrom3>> from3,
            Expression<Func<TSource, TFrom4>> from4,
            Expression<Func<TSource, TFrom5>> from5,
            Expression<Func<TSource, TFrom6>> from6,
            Expression<Func<TSource, TFrom7>> from7)
            => ((IMapperBuilder<TSource, TTarget>)this).Map(
                from1,
                from2,
                from3,
                from4,
                from5,
                from6,
                from7);

    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom1<TFrom1, TTarget> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom1(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public MappingRuleBuilderFrom1To1<TFrom1, TTo1> To<TTo1>(
            Expression<Func<TTarget, TTo1>> to1)
        {
            _mappingRule
                .WithTarget(to1);

            return new MappingRuleBuilderFrom1To1<TFrom1, TTo1>(_mappingRule);
        }

        public MappingRuleBuilderFrom1To2<TFrom1, TTo1, TTo2> To<TTo1, TTo2>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2);

            return new MappingRuleBuilderFrom1To2<TFrom1, TTo1, TTo2>(_mappingRule);
        }

        public MappingRuleBuilderFrom1To3<TFrom1, TTo1, TTo2, TTo3> To<TTo1, TTo2, TTo3>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3);

            return new MappingRuleBuilderFrom1To3<TFrom1, TTo1, TTo2, TTo3>(_mappingRule);
        }

        public MappingRuleBuilderFrom1To4<TFrom1, TTo1, TTo2, TTo3, TTo4> To<TTo1, TTo2, TTo3, TTo4>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4);

            return new MappingRuleBuilderFrom1To4<TFrom1, TTo1, TTo2, TTo3, TTo4>(_mappingRule);
        }

        public MappingRuleBuilderFrom1To5<TFrom1, TTo1, TTo2, TTo3, TTo4, TTo5> To<TTo1, TTo2, TTo3, TTo4, TTo5>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5);

            return new MappingRuleBuilderFrom1To5<TFrom1, TTo1, TTo2, TTo3, TTo4, TTo5>(_mappingRule);
        }

        public MappingRuleBuilderFrom1To6<TFrom1, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> To<TTo1, TTo2, TTo3, TTo4, TTo5, TTo6>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5,
            Expression<Func<TTarget, TTo6>> to6)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5)
                .WithTarget(to6);

            return new MappingRuleBuilderFrom1To6<TFrom1, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6>(_mappingRule);
        }

        public MappingRuleBuilderFrom1To7<TFrom1, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> To<TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5,
            Expression<Func<TTarget, TTo6>> to6,
            Expression<Func<TTarget, TTo7>> to7)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5)
                .WithTarget(to6)
                .WithTarget(to7);

            return new MappingRuleBuilderFrom1To7<TFrom1, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7>(_mappingRule);
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom1To1<TFrom1, TTo1> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom1To1(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate TTo1 BuilderDelegate(
            TFrom1 from1);

        public MappingRuleBuilderFrom1To1<TFrom1, TTo1> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom1To2<TFrom1, TTo1, TTo2> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom1To2(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2) BuilderDelegate(
            TFrom1 from1);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1);

        public MappingRuleBuilderFrom1To2<TFrom1, TTo1, TTo2> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom1To2<TFrom1, TTo1, TTo2> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom1To3<TFrom1, TTo1, TTo2, TTo3> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom1To3(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3) BuilderDelegate(
            TFrom1 from1);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1);

        public MappingRuleBuilderFrom1To3<TFrom1, TTo1, TTo2, TTo3> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom1To3<TFrom1, TTo1, TTo2, TTo3> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom1To4<TFrom1, TTo1, TTo2, TTo3, TTo4> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom1To4(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4) BuilderDelegate(
            TFrom1 from1);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1);

        public MappingRuleBuilderFrom1To4<TFrom1, TTo1, TTo2, TTo3, TTo4> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom1To4<TFrom1, TTo1, TTo2, TTo3, TTo4> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom1To5<TFrom1, TTo1, TTo2, TTo3, TTo4, TTo5> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom1To5(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5) BuilderDelegate(
            TFrom1 from1);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1);

        public MappingRuleBuilderFrom1To5<TFrom1, TTo1, TTo2, TTo3, TTo4, TTo5> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom1To5<TFrom1, TTo1, TTo2, TTo3, TTo4, TTo5> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom1To6<TFrom1, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom1To6(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5, TTo6) BuilderDelegate(
            TFrom1 from1);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1);

        public MappingRuleBuilderFrom1To6<TFrom1, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom1To6<TFrom1, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom1To7<TFrom1, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom1To7(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7) BuilderDelegate(
            TFrom1 from1);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1);

        public MappingRuleBuilderFrom1To7<TFrom1, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom1To7<TFrom1, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom2<TFrom1, TFrom2, TTarget> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom2(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public MappingRuleBuilderFrom2To1<TFrom1, TFrom2, TTo1> To<TTo1>(
            Expression<Func<TTarget, TTo1>> to1)
        {
            _mappingRule
                .WithTarget(to1);

            return new MappingRuleBuilderFrom2To1<TFrom1, TFrom2, TTo1>(_mappingRule);
        }

        public MappingRuleBuilderFrom2To2<TFrom1, TFrom2, TTo1, TTo2> To<TTo1, TTo2>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2);

            return new MappingRuleBuilderFrom2To2<TFrom1, TFrom2, TTo1, TTo2>(_mappingRule);
        }

        public MappingRuleBuilderFrom2To3<TFrom1, TFrom2, TTo1, TTo2, TTo3> To<TTo1, TTo2, TTo3>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3);

            return new MappingRuleBuilderFrom2To3<TFrom1, TFrom2, TTo1, TTo2, TTo3>(_mappingRule);
        }

        public MappingRuleBuilderFrom2To4<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4> To<TTo1, TTo2, TTo3, TTo4>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4);

            return new MappingRuleBuilderFrom2To4<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4>(_mappingRule);
        }

        public MappingRuleBuilderFrom2To5<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4, TTo5> To<TTo1, TTo2, TTo3, TTo4, TTo5>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5);

            return new MappingRuleBuilderFrom2To5<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4, TTo5>(_mappingRule);
        }

        public MappingRuleBuilderFrom2To6<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> To<TTo1, TTo2, TTo3, TTo4, TTo5, TTo6>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5,
            Expression<Func<TTarget, TTo6>> to6)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5)
                .WithTarget(to6);

            return new MappingRuleBuilderFrom2To6<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6>(_mappingRule);
        }

        public MappingRuleBuilderFrom2To7<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> To<TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5,
            Expression<Func<TTarget, TTo6>> to6,
            Expression<Func<TTarget, TTo7>> to7)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5)
                .WithTarget(to6)
                .WithTarget(to7);

            return new MappingRuleBuilderFrom2To7<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7>(_mappingRule);
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom2To1<TFrom1, TFrom2, TTo1> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom2To1(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate TTo1 BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2);

        public MappingRuleBuilderFrom2To1<TFrom1, TFrom2, TTo1> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom2To2<TFrom1, TFrom2, TTo1, TTo2> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom2To2(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2);

        public MappingRuleBuilderFrom2To2<TFrom1, TFrom2, TTo1, TTo2> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom2To2<TFrom1, TFrom2, TTo1, TTo2> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom2To3<TFrom1, TFrom2, TTo1, TTo2, TTo3> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom2To3(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2);

        public MappingRuleBuilderFrom2To3<TFrom1, TFrom2, TTo1, TTo2, TTo3> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom2To3<TFrom1, TFrom2, TTo1, TTo2, TTo3> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom2To4<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom2To4(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2);

        public MappingRuleBuilderFrom2To4<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom2To4<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom2To5<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4, TTo5> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom2To5(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2);

        public MappingRuleBuilderFrom2To5<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4, TTo5> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom2To5<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4, TTo5> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom2To6<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom2To6(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5, TTo6) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2);

        public MappingRuleBuilderFrom2To6<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom2To6<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom2To7<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom2To7(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2);

        public MappingRuleBuilderFrom2To7<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom2To7<TFrom1, TFrom2, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom3<TFrom1, TFrom2, TFrom3, TTarget> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom3(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public MappingRuleBuilderFrom3To1<TFrom1, TFrom2, TFrom3, TTo1> To<TTo1>(
            Expression<Func<TTarget, TTo1>> to1)
        {
            _mappingRule
                .WithTarget(to1);

            return new MappingRuleBuilderFrom3To1<TFrom1, TFrom2, TFrom3, TTo1>(_mappingRule);
        }

        public MappingRuleBuilderFrom3To2<TFrom1, TFrom2, TFrom3, TTo1, TTo2> To<TTo1, TTo2>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2);

            return new MappingRuleBuilderFrom3To2<TFrom1, TFrom2, TFrom3, TTo1, TTo2>(_mappingRule);
        }

        public MappingRuleBuilderFrom3To3<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3> To<TTo1, TTo2, TTo3>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3);

            return new MappingRuleBuilderFrom3To3<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3>(_mappingRule);
        }

        public MappingRuleBuilderFrom3To4<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4> To<TTo1, TTo2, TTo3, TTo4>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4);

            return new MappingRuleBuilderFrom3To4<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4>(_mappingRule);
        }

        public MappingRuleBuilderFrom3To5<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4, TTo5> To<TTo1, TTo2, TTo3, TTo4, TTo5>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5);

            return new MappingRuleBuilderFrom3To5<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4, TTo5>(_mappingRule);
        }

        public MappingRuleBuilderFrom3To6<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> To<TTo1, TTo2, TTo3, TTo4, TTo5, TTo6>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5,
            Expression<Func<TTarget, TTo6>> to6)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5)
                .WithTarget(to6);

            return new MappingRuleBuilderFrom3To6<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6>(_mappingRule);
        }

        public MappingRuleBuilderFrom3To7<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> To<TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5,
            Expression<Func<TTarget, TTo6>> to6,
            Expression<Func<TTarget, TTo7>> to7)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5)
                .WithTarget(to6)
                .WithTarget(to7);

            return new MappingRuleBuilderFrom3To7<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7>(_mappingRule);
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom3To1<TFrom1, TFrom2, TFrom3, TTo1> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom3To1(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate TTo1 BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3);

        public MappingRuleBuilderFrom3To1<TFrom1, TFrom2, TFrom3, TTo1> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom3To2<TFrom1, TFrom2, TFrom3, TTo1, TTo2> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom3To2(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3);

        public MappingRuleBuilderFrom3To2<TFrom1, TFrom2, TFrom3, TTo1, TTo2> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom3To2<TFrom1, TFrom2, TFrom3, TTo1, TTo2> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom3To3<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom3To3(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3);

        public MappingRuleBuilderFrom3To3<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom3To3<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom3To4<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom3To4(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3);

        public MappingRuleBuilderFrom3To4<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom3To4<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom3To5<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4, TTo5> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom3To5(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3);

        public MappingRuleBuilderFrom3To5<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4, TTo5> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom3To5<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4, TTo5> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom3To6<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom3To6(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5, TTo6) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3);

        public MappingRuleBuilderFrom3To6<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom3To6<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom3To7<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom3To7(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3);

        public MappingRuleBuilderFrom3To7<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom3To7<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom4<TFrom1, TFrom2, TFrom3, TFrom4, TTarget> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom4(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public MappingRuleBuilderFrom4To1<TFrom1, TFrom2, TFrom3, TFrom4, TTo1> To<TTo1>(
            Expression<Func<TTarget, TTo1>> to1)
        {
            _mappingRule
                .WithTarget(to1);

            return new MappingRuleBuilderFrom4To1<TFrom1, TFrom2, TFrom3, TFrom4, TTo1>(_mappingRule);
        }

        public MappingRuleBuilderFrom4To2<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2> To<TTo1, TTo2>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2);

            return new MappingRuleBuilderFrom4To2<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2>(_mappingRule);
        }

        public MappingRuleBuilderFrom4To3<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3> To<TTo1, TTo2, TTo3>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3);

            return new MappingRuleBuilderFrom4To3<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3>(_mappingRule);
        }

        public MappingRuleBuilderFrom4To4<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4> To<TTo1, TTo2, TTo3, TTo4>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4);

            return new MappingRuleBuilderFrom4To4<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4>(_mappingRule);
        }

        public MappingRuleBuilderFrom4To5<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4, TTo5> To<TTo1, TTo2, TTo3, TTo4, TTo5>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5);

            return new MappingRuleBuilderFrom4To5<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4, TTo5>(_mappingRule);
        }

        public MappingRuleBuilderFrom4To6<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> To<TTo1, TTo2, TTo3, TTo4, TTo5, TTo6>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5,
            Expression<Func<TTarget, TTo6>> to6)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5)
                .WithTarget(to6);

            return new MappingRuleBuilderFrom4To6<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6>(_mappingRule);
        }

        public MappingRuleBuilderFrom4To7<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> To<TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5,
            Expression<Func<TTarget, TTo6>> to6,
            Expression<Func<TTarget, TTo7>> to7)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5)
                .WithTarget(to6)
                .WithTarget(to7);

            return new MappingRuleBuilderFrom4To7<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7>(_mappingRule);
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom4To1<TFrom1, TFrom2, TFrom3, TFrom4, TTo1> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom4To1(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate TTo1 BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4);

        public MappingRuleBuilderFrom4To1<TFrom1, TFrom2, TFrom3, TFrom4, TTo1> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom4To2<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom4To2(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4);

        public MappingRuleBuilderFrom4To2<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom4To2<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom4To3<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom4To3(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4);

        public MappingRuleBuilderFrom4To3<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom4To3<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom4To4<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom4To4(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4);

        public MappingRuleBuilderFrom4To4<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom4To4<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom4To5<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4, TTo5> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom4To5(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4);

        public MappingRuleBuilderFrom4To5<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4, TTo5> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom4To5<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4, TTo5> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom4To6<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom4To6(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5, TTo6) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4);

        public MappingRuleBuilderFrom4To6<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom4To6<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom4To7<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom4To7(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4);

        public MappingRuleBuilderFrom4To7<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom4To7<TFrom1, TFrom2, TFrom3, TFrom4, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTarget> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom5(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public MappingRuleBuilderFrom5To1<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1> To<TTo1>(
            Expression<Func<TTarget, TTo1>> to1)
        {
            _mappingRule
                .WithTarget(to1);

            return new MappingRuleBuilderFrom5To1<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1>(_mappingRule);
        }

        public MappingRuleBuilderFrom5To2<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2> To<TTo1, TTo2>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2);

            return new MappingRuleBuilderFrom5To2<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2>(_mappingRule);
        }

        public MappingRuleBuilderFrom5To3<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3> To<TTo1, TTo2, TTo3>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3);

            return new MappingRuleBuilderFrom5To3<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3>(_mappingRule);
        }

        public MappingRuleBuilderFrom5To4<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4> To<TTo1, TTo2, TTo3, TTo4>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4);

            return new MappingRuleBuilderFrom5To4<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4>(_mappingRule);
        }

        public MappingRuleBuilderFrom5To5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4, TTo5> To<TTo1, TTo2, TTo3, TTo4, TTo5>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5);

            return new MappingRuleBuilderFrom5To5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4, TTo5>(_mappingRule);
        }

        public MappingRuleBuilderFrom5To6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> To<TTo1, TTo2, TTo3, TTo4, TTo5, TTo6>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5,
            Expression<Func<TTarget, TTo6>> to6)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5)
                .WithTarget(to6);

            return new MappingRuleBuilderFrom5To6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6>(_mappingRule);
        }

        public MappingRuleBuilderFrom5To7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> To<TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5,
            Expression<Func<TTarget, TTo6>> to6,
            Expression<Func<TTarget, TTo7>> to7)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5)
                .WithTarget(to6)
                .WithTarget(to7);

            return new MappingRuleBuilderFrom5To7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7>(_mappingRule);
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom5To1<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom5To1(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate TTo1 BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5);

        public MappingRuleBuilderFrom5To1<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom5To2<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom5To2(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5);

        public MappingRuleBuilderFrom5To2<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom5To2<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom5To3<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom5To3(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5);

        public MappingRuleBuilderFrom5To3<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom5To3<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom5To4<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom5To4(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5);

        public MappingRuleBuilderFrom5To4<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom5To4<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom5To5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4, TTo5> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom5To5(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5);

        public MappingRuleBuilderFrom5To5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4, TTo5> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom5To5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4, TTo5> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom5To6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom5To6(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5, TTo6) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5);

        public MappingRuleBuilderFrom5To6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom5To6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom5To7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom5To7(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5);

        public MappingRuleBuilderFrom5To7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom5To7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTarget> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom6(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public MappingRuleBuilderFrom6To1<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1> To<TTo1>(
            Expression<Func<TTarget, TTo1>> to1)
        {
            _mappingRule
                .WithTarget(to1);

            return new MappingRuleBuilderFrom6To1<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1>(_mappingRule);
        }

        public MappingRuleBuilderFrom6To2<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2> To<TTo1, TTo2>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2);

            return new MappingRuleBuilderFrom6To2<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2>(_mappingRule);
        }

        public MappingRuleBuilderFrom6To3<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3> To<TTo1, TTo2, TTo3>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3);

            return new MappingRuleBuilderFrom6To3<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3>(_mappingRule);
        }

        public MappingRuleBuilderFrom6To4<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4> To<TTo1, TTo2, TTo3, TTo4>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4);

            return new MappingRuleBuilderFrom6To4<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4>(_mappingRule);
        }

        public MappingRuleBuilderFrom6To5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4, TTo5> To<TTo1, TTo2, TTo3, TTo4, TTo5>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5);

            return new MappingRuleBuilderFrom6To5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4, TTo5>(_mappingRule);
        }

        public MappingRuleBuilderFrom6To6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> To<TTo1, TTo2, TTo3, TTo4, TTo5, TTo6>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5,
            Expression<Func<TTarget, TTo6>> to6)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5)
                .WithTarget(to6);

            return new MappingRuleBuilderFrom6To6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6>(_mappingRule);
        }

        public MappingRuleBuilderFrom6To7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> To<TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5,
            Expression<Func<TTarget, TTo6>> to6,
            Expression<Func<TTarget, TTo7>> to7)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5)
                .WithTarget(to6)
                .WithTarget(to7);

            return new MappingRuleBuilderFrom6To7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7>(_mappingRule);
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom6To1<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom6To1(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate TTo1 BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6);

        public MappingRuleBuilderFrom6To1<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom6To2<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom6To2(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6);

        public MappingRuleBuilderFrom6To2<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom6To2<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom6To3<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom6To3(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6);

        public MappingRuleBuilderFrom6To3<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom6To3<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom6To4<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom6To4(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6);

        public MappingRuleBuilderFrom6To4<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom6To4<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom6To5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4, TTo5> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom6To5(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6);

        public MappingRuleBuilderFrom6To5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4, TTo5> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom6To5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4, TTo5> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom6To6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom6To6(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5, TTo6) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6);

        public MappingRuleBuilderFrom6To6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom6To6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom6To7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom6To7(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6);

        public MappingRuleBuilderFrom6To7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom6To7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTarget> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom7(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public MappingRuleBuilderFrom7To1<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1> To<TTo1>(
            Expression<Func<TTarget, TTo1>> to1)
        {
            _mappingRule
                .WithTarget(to1);

            return new MappingRuleBuilderFrom7To1<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1>(_mappingRule);
        }

        public MappingRuleBuilderFrom7To2<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2> To<TTo1, TTo2>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2);

            return new MappingRuleBuilderFrom7To2<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2>(_mappingRule);
        }

        public MappingRuleBuilderFrom7To3<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3> To<TTo1, TTo2, TTo3>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3);

            return new MappingRuleBuilderFrom7To3<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3>(_mappingRule);
        }

        public MappingRuleBuilderFrom7To4<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4> To<TTo1, TTo2, TTo3, TTo4>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4);

            return new MappingRuleBuilderFrom7To4<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4>(_mappingRule);
        }

        public MappingRuleBuilderFrom7To5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4, TTo5> To<TTo1, TTo2, TTo3, TTo4, TTo5>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5);

            return new MappingRuleBuilderFrom7To5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4, TTo5>(_mappingRule);
        }

        public MappingRuleBuilderFrom7To6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> To<TTo1, TTo2, TTo3, TTo4, TTo5, TTo6>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5,
            Expression<Func<TTarget, TTo6>> to6)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5)
                .WithTarget(to6);

            return new MappingRuleBuilderFrom7To6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6>(_mappingRule);
        }

        public MappingRuleBuilderFrom7To7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> To<TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7>(
            Expression<Func<TTarget, TTo1>> to1,
            Expression<Func<TTarget, TTo2>> to2,
            Expression<Func<TTarget, TTo3>> to3,
            Expression<Func<TTarget, TTo4>> to4,
            Expression<Func<TTarget, TTo5>> to5,
            Expression<Func<TTarget, TTo6>> to6,
            Expression<Func<TTarget, TTo7>> to7)
        {
            _mappingRule
                .WithTarget(to1)
                .WithTarget(to2)
                .WithTarget(to3)
                .WithTarget(to4)
                .WithTarget(to5)
                .WithTarget(to6)
                .WithTarget(to7);

            return new MappingRuleBuilderFrom7To7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7>(_mappingRule);
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom7To1<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom7To1(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate TTo1 BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6,
            TFrom7 from7);

        public MappingRuleBuilderFrom7To1<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom7To2<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom7To2(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6,
            TFrom7 from7);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6,
            TFrom7 from7);

        public MappingRuleBuilderFrom7To2<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom7To2<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom7To3<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom7To3(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6,
            TFrom7 from7);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6,
            TFrom7 from7);

        public MappingRuleBuilderFrom7To3<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom7To3<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom7To4<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom7To4(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6,
            TFrom7 from7);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6,
            TFrom7 from7);

        public MappingRuleBuilderFrom7To4<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom7To4<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom7To5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4, TTo5> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom7To5(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6,
            TFrom7 from7);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6,
            TFrom7 from7);

        public MappingRuleBuilderFrom7To5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4, TTo5> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom7To5<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4, TTo5> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom7To6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom7To6(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5, TTo6) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6,
            TFrom7 from7);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6,
            TFrom7 from7);

        public MappingRuleBuilderFrom7To6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom7To6<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class MappingRuleBuilderFrom7To7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom7To7(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public delegate (TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7) BuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6,
            TFrom7 from7);

        public delegate object[] ArrayBuilderDelegate(
            TFrom1 from1,
            TFrom2 from2,
            TFrom3 from3,
            TFrom4 from4,
            TFrom5 from5,
            TFrom6 from6,
            TFrom7 from7);

        public MappingRuleBuilderFrom7To7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

        public MappingRuleBuilderFrom7To7<TFrom1, TFrom2, TFrom3, TFrom4, TFrom5, TFrom6, TFrom7, TTo1, TTo2, TTo3, TTo4, TTo5, TTo6, TTo7> Using(
            ArrayBuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }
    }

}