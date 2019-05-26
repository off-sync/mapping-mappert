using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Tests.Models;

namespace OffSync.Mapping.Mappert.Tests.MappingRules
{
    public partial class MappingRuleTest
    {
        [Test]
        public void IsSerializable()
        {
            Func<SourceNested, TargetNested> builder = sn => new TargetNested() { Key = sn.Key, Value = sn.Value };

            sut
                .WithSourceItems(typeof(SourceModel).GetProperty(nameof(SourceModel.ItemsArray)), typeof(SourceNested))
                .WithTargetItems(typeof(TargetModel).GetProperty(nameof(TargetModel.ItemsArray)), typeof(TargetNested))
                .WithType(MappingRuleTypes.MapToArray)
                .WithBuilder(builder);

            var formatter = new BinaryFormatter();

            var stream = new MemoryStream();

            formatter.Serialize(
                stream,
                sut);

            stream.Position = 0;

            var rule = (MappingRule)formatter.Deserialize(stream);

            CollectionAssert.AreEqual(
                sut.SourceProperties,
                rule.SourceProperties);
        }
    }
}
