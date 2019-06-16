/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using NUnit.Framework;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.MappingRules;
using OffSync.Mapping.Mappert.Tests.Models;
using OffSync.Mapping.Mappert.Validation.Exceptions;

namespace OffSync.Mapping.Mappert.Tests.Validation.Exceptions
{
    [TestFixture]
    public class MappingRuleValidationExceptionTest
    {
        private MappingRule _rule;

        private string _message;

        private InvalidOperationException _exception;

        private MappingRuleValidationException _sut;

        [SetUp]
        public void SetUp()
        {
            _rule = new MappingRule()
                .WithSource(typeof(SourceModel).GetProperty(nameof(SourceModel.Values)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value1)))
                .WithTarget(typeof(TargetModel).GetProperty(nameof(TargetModel.Value2)))
                .WithType(MappingRuleTypes.MapToCollection);

            _message = nameof(_message);

            _exception = new InvalidOperationException(nameof(_message));

            _sut = new MappingRuleValidationException(
                _rule,
                _message,
                _exception);
        }

        [Test]
        public void ShouldSerialize()
        {
            var formatter = new BinaryFormatter();

            var stream = new MemoryStream();

            formatter.Serialize(
                stream,
                _sut);

            stream.Position = 0;

            var exception = (MappingRuleValidationException)formatter.Deserialize(stream);

            Assert.That(
                exception.MappingRule.Type,
                Is.EqualTo(MappingRuleTypes.MapToCollection));

            Assert.That(
                exception.MappingRule.SourceProperties,
                Has.Exactly(1).Items);

            Assert.That(
                exception.MappingRule.TargetProperties,
                Has.Exactly(2).Items);

            Assert.That(
                exception.Message,
                Is.EqualTo(_message));

            Assert.That(
                exception.InnerException,
                Is.TypeOf<InvalidOperationException>());
        }
    }
}
