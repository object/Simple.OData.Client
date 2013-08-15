﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Simple.OData.Client.Tests
{
    using Entry = System.Collections.Generic.Dictionary<string, object>;

    public class FunctionTests : TestBase
    {
        [Fact]
        public void FunctionWithString()
        {
            var result = _client.ExecuteFunction<int>("ParseInt", new Entry() { { "number", "1" } });
            Assert.Equal(1, result);
        }

        [Fact]
        public void FunctionWithIntCollectionSingleElement()
        {
            var result = _client.ExecuteFunction("ReturnIntCollection", new Entry() { { "count", 1 } });
            Assert.Equal(new object[] { 1 }, result.First().First().Value);
        }

        [Fact]
        public void FunctionWithIntCollectionMultipleElements()
        {
            var result = _client.ExecuteFunction("ReturnIntCollection", new Entry() { { "count", 3 } });
            Assert.Equal(new object[] { 1, 2, 3 }, result.First().First().Value);
        }

        [Fact]
        public void FunctionWithLong()
        {
            var result = _client.ExecuteFunction<long>("PassThroughLong", new Entry() { { "number", 1L } });
            Assert.Equal(1L, result);
        }

        [Fact]
        public void FunctionWithDateTime()
        {
            var dateTime = new DateTime(2013, 1, 1, 12, 13, 14);
            var result = _client.ExecuteFunction<DateTime>("PassThroughDateTime", new Entry() { { "dateTime", dateTime } });
            Assert.Equal(dateTime.ToLocalTime(), result);
        }

        [Fact]
        public void FunctionWithGuid()
        {
            var guid = Guid.NewGuid();
            var result = _client.ExecuteFunction<Guid>("PassThroughGuid", new Entry() { { "guid", guid } });
            Assert.Equal(guid, result);
        }

        [Fact]
        public void FunctionWithComplexTypeCollectionSingleElement()
        {
            var result = _client.ExecuteFunction("ReturnAddressCollection", new Entry() { { "count", 1 } });
            Assert.Equal("Oslo", ((result.First().First().Value as object[])[0] as IDictionary<string, object>)["City"]);
            Assert.Equal("Norway", ((result.First().First().Value as object[])[0] as IDictionary<string, object>)["Country"]);
        }

        [Fact]
        public void FunctionWithComplexTypeCollectionMultipleElements()
        {
            var result = _client.ExecuteFunction("ReturnAddressCollection", new Entry() { { "count", 3 } });
            Assert.Equal("Oslo", ((result.First().First().Value as object[])[0] as IDictionary<string, object>)["City"]);
            Assert.Equal("Norway", ((result.First().First().Value as object[])[0] as IDictionary<string, object>)["Country"]);
            Assert.Equal("Oslo", ((result.First().First().Value as object[])[1] as IDictionary<string, object>)["City"]);
            Assert.Equal("Oslo", ((result.First().First().Value as object[])[2] as IDictionary<string, object>)["City"]);
        }
    }
}
