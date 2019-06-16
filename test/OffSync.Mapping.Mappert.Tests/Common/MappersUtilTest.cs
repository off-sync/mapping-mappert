/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using NUnit.Framework;

using OffSync.Mapping.Mappert.Common;
using OffSync.Mapping.Mappert.Tests.Models;
using OffSync.Mapping.Practises;

namespace OffSync.Mapping.Mappert.Tests.Common
{
    [TestFixture]
    public class MappersUtilTest
    {
        public class TargetModelWithoutParameterlessConstructor
        {
            public TargetModelWithoutParameterlessConstructor(
                string param)
            {
            }
        }

        public class MultipleMapper :
            IMapper<SourceModel, TargetModel>,
            IMapper<SourceModel, TargetModelWithoutParameterlessConstructor>
        {
            public TargetModel Map(SourceModel source) => throw new System.NotImplementedException();

            TargetModelWithoutParameterlessConstructor IMapper<SourceModel, TargetModelWithoutParameterlessConstructor>.Map(SourceModel source) => throw new System.NotImplementedException();
        }

        [Test]
        public void CreateAutoMapperShouldCheckPreConditions()
        {
            Assert.That(
                () => MappersUtil.CreateAutoMapper(
                    null,
                    typeof(TargetModel)),
                Throws.ArgumentNullException);

            Assert.That(
                () => MappersUtil.CreateAutoMapper(
                    typeof(SourceModel),
                    null),
                Throws.ArgumentNullException);

            Assert.That(
                () => MappersUtil.CreateAutoMapper(
                    typeof(SourceModel),
                    typeof(TargetModelWithoutParameterlessConstructor)),
                Throws.ArgumentException);
        }

        [Test]
        public void CreateMapperDelegateShouldCheckPreConditions()
        {
            Assert.That(
                () => MappersUtil.CreateMapperDelegate(null),
                Throws.ArgumentNullException);

            Assert.That(
                () => MappersUtil.CreateMapperDelegate(new TargetModel()),
                Throws.ArgumentException);

            Assert.That(
                () => MappersUtil.CreateMapperDelegate(new MultipleMapper()),
                Throws.ArgumentException);
        }
    }
}
