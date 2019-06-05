using System.Collections.Generic;

using NUnit.Framework;

using OffSync.Mapping.Mappert.Practises;

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
        //[Ignore("until recursive mapping is supported")]
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

            var target = sut.Map(source);

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
        }
    }
}
