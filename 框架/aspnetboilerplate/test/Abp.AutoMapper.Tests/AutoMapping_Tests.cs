﻿using System.Collections.Generic;
using AutoMapper;
using Shouldly;
using Xunit;

namespace Abp.AutoMapper.Tests
{
    public class AutoMapping_Tests
    {
        private static IMapper _mapper;

        static AutoMapping_Tests()
        {
            var config = new MapperConfiguration(configuration =>
            {
                configuration.CreateAbpAttributeMaps(typeof(MyClass1));
                configuration.CreateAbpAttributeMaps(typeof(MyClass2));
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Map_Null_Tests()
        {
            MyClass1 obj1 = null;
            var obj2 = _mapper.Map<MyClass2>(obj1);
            obj2.ShouldBe(null);
        }

        [Fact]
        public void Map_Null_Existing_Object_Tests()
        {
            MyClass1 obj1 = null;

            var obj2 = new MyClass2 { TestProp = "before map" };
            _mapper.Map(obj1, obj2);
            obj2.TestProp.ShouldBe("before map");
        }

        [Fact]
        public void MapTo_Tests()
        {
            var obj1 = new MyClass1 { TestProp = "Test value" };

            var obj2 = _mapper.Map<MyClass2>(obj1);
            obj2.TestProp.ShouldBe("Test value");

            var obj3 = _mapper.Map<MyClass3>(obj1);
            obj3.TestProp.ShouldBe("Test value");
        }

        [Fact]
        public void MapTo_Existing_Object_Tests()
        {
            var obj1 = new MyClass1 { TestProp = "Test value" };

            var obj2 = new MyClass2();
            _mapper.Map(obj1, obj2);
            obj2.TestProp.ShouldBe("Test value");

            var obj3 = new MyClass3();
            _mapper.Map(obj2, obj3);
            obj3.TestProp.ShouldBe("Test value");
        }

        [Fact]
        public void MapFrom_Tests()
        {
            var obj2 = new MyClass2 { TestProp = "Test value" };

            var obj1 = _mapper.Map<MyClass1>(obj2);
            obj1.TestProp.ShouldBe("Test value");
        }

        [Fact]
        public void MapTo_Collection_Tests()
        {
            var list1 = new List<MyClass1>
                        {
                            new MyClass1 {TestProp = "Test value 1"},
                            new MyClass1 {TestProp = "Test value 2"}
                        };

            var list2 = _mapper.Map<List<MyClass2>>(list1);
            list2.Count.ShouldBe(2);
            list2[0].TestProp.ShouldBe("Test value 1");
            list2[1].TestProp.ShouldBe("Test value 2");
        }

        [Fact]
        public void Map_Should_Set_Null_Existing_Object_Tests()
        {
            MyClass1 obj1 = new MyClass1 { TestProp = null };
            var obj2 = new MyClass2 { TestProp = "before map" };
            _mapper.Map(obj1, obj2);
            obj2.TestProp.ShouldBe(null);
        }

        [Fact]
        public void Should_Map_Nullable_Value_To_Null_If_It_Is_Null_On_Source()
        {
            var obj1 = new MyClass1();
            var obj2 = _mapper.Map<MyClass2>(obj1);
            obj2.NullableValue.ShouldBeNull();
        }

        [Fact]
        public void Should_Map_Nullable_Value_To__Not_Null_If_It_Is__Not_Null_On_Source()
        {
            var obj1 = new MyClass1 { NullableValue = 42 };
            var obj2 = _mapper.Map<MyClass2>(obj1);
            obj2.NullableValue.ShouldBe(42);
        }

        [AutoMap(typeof(MyClass2), typeof(MyClass3))]
        private class MyClass1
        {
            public string TestProp { get; set; }

            public long? NullableValue { get; set; }
        }

        [AutoMapTo(typeof(MyClass3))]
        private class MyClass2
        {
            public string TestProp { get; set; }

            public long? NullableValue { get; set; }
        }

        private class MyClass3
        {
            public string TestProp { get; set; }
        }
    }
}
