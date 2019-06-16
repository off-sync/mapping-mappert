/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.Builders;
using OffSync.Mapping.Mappert.Tests.Models;

namespace OffSync.Mapping.Mappert.Practises.Tests.Builders
{
    [TestFixture]
    public class BuildersUtilTest
    {
        [Test]
        public void GetBuilderTypeShouldCheckPreConditions()
        {
            Assert.That(
                () => BuildersUtil.GetBuilderType(null),
                Throws.ArgumentNullException);

            var sourceProperty = typeof(SourceModel).GetProperty(nameof(SourceModel.Id));

            var targetProperty = typeof(TargetModel).GetProperty(nameof(TargetModel.Id));

            Func<int, int> intBuilder = i => i;

            Assert.That(
                () => BuildersUtil.GetBuilderType(
                    new MappingRule()
                        .WithTarget(targetProperty)
                        .WithBuilder(intBuilder)),
                Throws.ArgumentException);

            Assert.That(
                () => BuildersUtil.GetBuilderType(
                    new MappingRule()
                        .WithSource(sourceProperty)
                        .WithBuilder(intBuilder)),
                Throws.ArgumentException);
        }

        [Test]
        public void GetBuilderTypeChecksSourcePropertiesVersusParameters()
        {
            var mappingRule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id)));

            Func<int, int> intBuilder = i => i;

            var builderType = BuildersUtil.GetBuilderType(
                mappingRule.WithBuilder(intBuilder));

            Assert.That(
                builderType,
                Is.EqualTo(BuilderTypes.SingleValue));

            Func<int, int, int> tooManyParamsBuilder = (i, j) => i + j;

            Assert.That(
                () => BuildersUtil.GetBuilderType(
                    mappingRule.WithBuilder(tooManyParamsBuilder)),
                Throws.ArgumentException);

            Func<string, int> wrongTypeBuilder = s => s.GetHashCode();

            Assert.That(
                () => BuildersUtil.GetBuilderType(
                    mappingRule.WithBuilder(wrongTypeBuilder)),
                Throws.ArgumentException);
        }

        [Test]
        public void GetBuilderTypeChecksTargetPropertiesVersusReturnType()
        {
            var mappingRule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Id)));

            Func<int, string> wrongTypeBuilder1 = i => i.ToString();

            Assert.That(
                () => BuildersUtil.GetBuilderType(
                    mappingRule.WithBuilder(wrongTypeBuilder1)),
                Throws.ArgumentException);

            mappingRule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Id)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)));

            Func<int, (string, int)> wrongTypeBuilder2 = i => (i.ToString(), i);

            Assert.That(
                () => BuildersUtil.GetBuilderType(
                    mappingRule.WithBuilder(wrongTypeBuilder2)),
                Throws.ArgumentException);

            Func<int, (string, string)> valueTupleBuilder = i => (i.ToString(), i.ToString());

            Assert.That(
                BuildersUtil.GetBuilderType(
                    mappingRule.WithBuilder(valueTupleBuilder)),
                Is.EqualTo(BuilderTypes.ValueTuple));

            Func<int, object[]> objectArrayBuilder = i => Enumerable.Range(0, i).Cast<object>().ToArray();

            Assert.That(
                BuildersUtil.GetBuilderType(
                    mappingRule.WithBuilder(objectArrayBuilder)),
                Is.EqualTo(BuilderTypes.ObjectArray));
        }
    }
}
