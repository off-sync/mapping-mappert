/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;

using NUnit.Framework;

using OffSync.Mapping.Mappert.Practises;
using OffSync.Mapping.Practises;

namespace OffSync.Mapping.Mappert.Tests
{
    [TestFixture]
    public partial class MapperTest
    {
        public class SourceNode
        {
            public SourceNode Root { get; set; }

            public SourceNode Parent { get; set; }

            public ICollection<SourceNode> SubNodes { get; set; } = new List<SourceNode>();

            public int Id { get; set; }

            public override string ToString() => $"Source#{Id}";
        }

        public class TargetNode
        {
            public TargetNode Root { get; set; }

            public TargetNode Parent { get; set; }

            public IList<TargetNode> SubNodes { get; set; } = new List<TargetNode>();

            public int Id { get; set; }
            public override string ToString() => $"Target#{Id}";
        }

        [Test]
        [TestCaseSource(nameof(MappingDelegateBuilders))]
        public void ShouldSupportTrees(
            IMappingDelegateBuilder mappingDelegateBuilder)
        {
            var source = new SourceNode()
            {
                Id = 1,
            };

            var node1 = new SourceNode()
            {
                Id = 2,
                Parent = source,
                Root = source,
            };

            source.SubNodes.Add(node1);

            var node2 = new SourceNode()
            {
                Id = 3,
                Parent = source,
                Root = source,
            };

            source.SubNodes.Add(node2);

            var sub1 = new SourceNode()
            {
                Id = 4,
                Parent = node1,
                Root = source,
            };

            node1.SubNodes.Add(sub1);

#pragma warning disable S4143 // Collection elements should not be replaced unconditionally
            node1.SubNodes.Add(sub1);
#pragma warning restore S4143 // Collection elements should not be replaced unconditionally

            var sub2 = new SourceNode()
            {
                Id = 5,
                Parent = node2,
                Root = source,
            };

            node2.SubNodes.Add(sub2);

            var sut = new Mapper<SourceNode, TargetNode>(
                b =>
                {
                    b.WithMappingDelegateBuilder(mappingDelegateBuilder);
                });

            var target = sut.MapRoot(source);

            Assert.That(
                target.SubNodes[0].Parent,
                Is.SameAs(target));

            Assert.That(
                target.SubNodes[0].Root,
                Is.SameAs(target));

            Assert.That(
                target.SubNodes[0].SubNodes[0].Root,
                Is.SameAs(target));

            Assert.That(
                target.SubNodes[0].SubNodes[0].Parent,
                Is.SameAs(target.SubNodes[0]));

            Assert.That(
                target.SubNodes[0].SubNodes[0],
                Is.SameAs(target.SubNodes[0].SubNodes[1]));

            source.Id = 10;

            var target2 = sut.MapRoot(source);

            Assert.That(
                target2,
                Is.Not.SameAs(target));

            Assert.That(
                target2.Id,
                Is.EqualTo(10));
        }
    }
}
