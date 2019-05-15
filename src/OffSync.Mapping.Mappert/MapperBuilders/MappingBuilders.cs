using System;
using System.Linq.Expressions;

using OffSync.Mapping.Mappert.MappingRules;

namespace OffSync.Mapping.Mappert.MapperBuilders
{
    public partial class MapperBuilder<TSource, TTarget>
    {

        protected MappingRuleBuilderFrom1<TFrom1, TTarget> Map<TFrom1>(
            Expression<Func<TSource, TFrom1>> from1)
        {
            var mappingRule = AddMappingRule()
                .WithSource(from1);

            return new MappingRuleBuilderFrom1<TFrom1, TTarget>(mappingRule);
        }

        protected MappingRuleBuilderFrom2<TFrom1, TFrom2, TTarget> Map<TFrom1, TFrom2>(
            Expression<Func<TSource, TFrom1>> from1,
            Expression<Func<TSource, TFrom2>> from2)
        {
            var mappingRule = AddMappingRule()
                .WithSource(from1)
                .WithSource(from2);

            return new MappingRuleBuilderFrom2<TFrom1, TFrom2, TTarget>(mappingRule);
        }

        protected MappingRuleBuilderFrom3<TFrom1, TFrom2, TFrom3, TTarget> Map<TFrom1, TFrom2, TFrom3>(
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
    }

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
    }

    public class MappingRuleBuilderFrom1To1<TFrom1, TTo1> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom1To1(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }
        public delegate TTo1 BuilderDelegate(TFrom1 from1);

        public MappingRuleBuilderFrom1To1<TFrom1, TTo1> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

    }

    public class MappingRuleBuilderFrom1To2<TFrom1, TTo1, TTo2> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom1To2(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }
        public delegate (TTo1, TTo2) BuilderDelegate(TFrom1 from1);

        public delegate object[] ArrayBuilderDelegate(TFrom1 from1);

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

    public class MappingRuleBuilderFrom1To3<TFrom1, TTo1, TTo2, TTo3> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom1To3(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }
        public delegate (TTo1, TTo2, TTo3) BuilderDelegate(TFrom1 from1);

        public delegate object[] ArrayBuilderDelegate(TFrom1 from1);

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
    }

    public class MappingRuleBuilderFrom2To1<TFrom1, TFrom2, TTo1> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom2To1(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }
        public delegate TTo1 BuilderDelegate(TFrom1 from1, TFrom2 from2);

        public MappingRuleBuilderFrom2To1<TFrom1, TFrom2, TTo1> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

    }

    public class MappingRuleBuilderFrom2To2<TFrom1, TFrom2, TTo1, TTo2> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom2To2(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }
        public delegate (TTo1, TTo2) BuilderDelegate(TFrom1 from1, TFrom2 from2);

        public delegate object[] ArrayBuilderDelegate(TFrom1 from1, TFrom2 from2);

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

    public class MappingRuleBuilderFrom2To3<TFrom1, TFrom2, TTo1, TTo2, TTo3> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom2To3(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }
        public delegate (TTo1, TTo2, TTo3) BuilderDelegate(TFrom1 from1, TFrom2 from2);

        public delegate object[] ArrayBuilderDelegate(TFrom1 from1, TFrom2 from2);

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
    }

    public class MappingRuleBuilderFrom3To1<TFrom1, TFrom2, TFrom3, TTo1> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom3To1(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }
        public delegate TTo1 BuilderDelegate(TFrom1 from1, TFrom2 from2, TFrom3 from3);

        public MappingRuleBuilderFrom3To1<TFrom1, TFrom2, TFrom3, TTo1> Using(
            BuilderDelegate builder)
        {
            _mappingRule.WithBuilder(builder);

            return this;
        }

    }

    public class MappingRuleBuilderFrom3To2<TFrom1, TFrom2, TFrom3, TTo1, TTo2> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom3To2(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }
        public delegate (TTo1, TTo2) BuilderDelegate(TFrom1 from1, TFrom2 from2, TFrom3 from3);

        public delegate object[] ArrayBuilderDelegate(TFrom1 from1, TFrom2 from2, TFrom3 from3);

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

    public class MappingRuleBuilderFrom3To3<TFrom1, TFrom2, TFrom3, TTo1, TTo2, TTo3> :
        AbstractMappingRuleBuilder
    {
        internal MappingRuleBuilderFrom3To3(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }
        public delegate (TTo1, TTo2, TTo3) BuilderDelegate(TFrom1 from1, TFrom2 from2, TFrom3 from3);

        public delegate object[] ArrayBuilderDelegate(TFrom1 from1, TFrom2 from2, TFrom3 from3);

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

}