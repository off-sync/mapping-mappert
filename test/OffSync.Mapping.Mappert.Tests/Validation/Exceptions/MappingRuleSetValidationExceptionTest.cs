/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using NUnit.Framework;

using OffSync.Mapping.Mappert.Validation.Exceptions;

namespace OffSync.Mapping.Mappert.Tests.Validation.Exceptions
{
    [TestFixture]
    public class MappingRuleSetValidationExceptionTest
    {
        private string _message;

        private InvalidOperationException _exception;

        private MappingRuleSetValidationException _sut;

        [SetUp]
        public void SetUp()
        {
            _message = nameof(_message);

            _exception = new InvalidOperationException(nameof(_message));

            _sut = new MappingRuleSetValidationException(
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

            var exception = (MappingRuleSetValidationException)formatter.Deserialize(stream);

            Assert.That(
                exception.Message,
                Is.EqualTo(_message));

            Assert.That(
                exception.InnerException,
                Is.TypeOf<InvalidOperationException>());
        }
    }
}
