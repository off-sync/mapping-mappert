/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using NUnit.Framework;

using OffSync.Mapping.Mappert.MapperBuilders;
using OffSync.Mapping.Mappert.MappingRules;

namespace OffSync.Mapping.Mappert.Tests.MappingRules
{
    [TestFixture]
    public class AbstractMappingRuleBuilderTest
    {
        public class MappingRuleBuilder :
            AbstractMappingRuleBuilder
        {
            public MappingRuleBuilder(MappingRule mappingRule) :
                base(mappingRule)
            {
            }
        }

        [Test]
        public void ConstructorShouldCheckPreConditions()
        {
            Assert.That(
                () => new MappingRuleBuilder(null),
                Throws.ArgumentNullException);
        }
    }
}
