using System;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

using OffSync.Mapping.Mappert.Common;

namespace OffSync.Mapping.Mappert.Tests.Common
{
    [TestFixture]
    public class BuilderTypesTests
    {
        [Test]
        public void GetBuilderTypeShouldCheckPreConditions()
        {
            var sourceProperties = new PropertyInfo[] { typeof(SourceModel).GetProperty(nameof(SourceModel.Id)) };

            var targetProperties = new PropertyInfo[] { typeof(TargetModel).GetProperty(nameof(TargetModel.Id)) };

            Func<int, int> intBuilder = i => i;

            Assert.That(
                () => BuilderUtil.GetBuilderType(
                    null,
                    targetProperties,
                    intBuilder),
                Throws.ArgumentNullException);

            Assert.That(
                () => BuilderUtil.GetBuilderType(
                    sourceProperties,
                    null,
                    intBuilder),
                Throws.ArgumentNullException);

            Assert.That(
                () => BuilderUtil.GetBuilderType(
                    sourceProperties,
                    new PropertyInfo[0],
                    intBuilder),
                Throws.ArgumentException);

            Assert.That(
                () => BuilderUtil.GetBuilderType(
                    sourceProperties,
                    targetProperties,
                    null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void GetBuilderTypeChecksSourcePropertiesVersusParameters()
        {
            Func<int, int> intBuilder = i => i;

            var builderType = BuilderUtil.GetBuilderType(
                new PropertyInfo[] { typeof(SourceModel).GetProperty(nameof(SourceModel.Id)) },
                new PropertyInfo[] { typeof(TargetModel).GetProperty(nameof(TargetModel.Id)) },
                intBuilder);

            Assert.That(
                builderType,
                Is.EqualTo(BuilderTypes.SingleValue));

            Func<int, int, int> tooManyParamsBuilder = (i, j) => i + j;

            Assert.That(
                () => BuilderUtil.GetBuilderType(
                    new PropertyInfo[] { typeof(SourceModel).GetProperty(nameof(SourceModel.Id)) },
                    new PropertyInfo[] { typeof(TargetModel).GetProperty(nameof(TargetModel.Id)) },
                    tooManyParamsBuilder),
                Throws.ArgumentException);

            Func<string, int> wrongTypeBuilder = s => s.GetHashCode();

            Assert.That(
                () => BuilderUtil.GetBuilderType(
                    new PropertyInfo[] { typeof(SourceModel).GetProperty(nameof(SourceModel.Id)) },
                    new PropertyInfo[] { typeof(TargetModel).GetProperty(nameof(TargetModel.Id)) },
                    wrongTypeBuilder),
                Throws.ArgumentException);
        }

        [Test]
        public void GetBuilderTypeChecksTargetPropertiesVersusReturnType()
        {
            Func<int, string> wrongTypeBuilder1 = i => i.ToString();

            Assert.That(
                () => BuilderUtil.GetBuilderType(
                    new PropertyInfo[] { typeof(SourceModel).GetProperty(nameof(SourceModel.Id)) },
                    new PropertyInfo[] { typeof(TargetModel).GetProperty(nameof(TargetModel.Id)) },
                    wrongTypeBuilder1),
                Throws.ArgumentException);

            Func<int, (string, int)> wrongTypeBuilder2 = i => (i.ToString(), i);

            Assert.That(
                () => BuilderUtil.GetBuilderType(
                    new PropertyInfo[] { typeof(SourceModel).GetProperty(nameof(SourceModel.Id)) },
                    new PropertyInfo[] {
                        typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)),
                        typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)),
                    },
                    wrongTypeBuilder2),
                Throws.ArgumentException);

            Func<int, (string, string)> valueTupleBuilder = i => (i.ToString(), i.ToString());

            Assert.That(
                BuilderUtil.GetBuilderType(
                    new PropertyInfo[] { typeof(SourceModel).GetProperty(nameof(SourceModel.Id)) },
                    new PropertyInfo[] {
                        typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)),
                        typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)),
                    },
                    valueTupleBuilder),
                Is.EqualTo(BuilderTypes.ValueTuple));

            Func<int, object[]> objectArrayBuilder = i => Enumerable.Range(0, i).Cast<object>().ToArray();

            Assert.That(
                BuilderUtil.GetBuilderType(
                    new PropertyInfo[] { typeof(SourceModel).GetProperty(nameof(SourceModel.Id)) },
                    new PropertyInfo[] {
                        typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)),
                        typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)),
                    },
                    objectArrayBuilder),
                Is.EqualTo(BuilderTypes.ObjectArray));
        }
    }
}
