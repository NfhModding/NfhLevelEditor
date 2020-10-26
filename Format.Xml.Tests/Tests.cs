using Format.Xml.Attributes;
using Format.Xml.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Format.Xml.Tests
{
    internal static class Str
    {
        public static string Sanitize(this string str)
        {
            return str.Trim().Replace("\r\n", "\n").Replace("\r", "\n");
        }

        public static void AreEqual(string s1, string s2)
        {
            Assert.AreEqual(s1.Sanitize(), s2.Sanitize());
        }
    }

    class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class PositionSerializer : TypedValueSerializer<Position>
    {
        public override string SerializeTyped(Position value)
        {
            if (value == null)
            {
                return null;
            }
            return $"{value.X}/{value.Y}";
        }

        public override Position DeserializeTyped(string value)
        {
            var parts = value.Split('/');
            return new Position
            {
                X = int.Parse(parts[0]),
                Y = int.Parse(parts[1]),
            };
        }
    }

    [TestClass]
    public class TestEmpty
    {
        class Empty { }

        [TestMethod]
        public void Serialize()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Empty());
            Str.AreEqual(xml, "<?xml version=\"1.0\"?>\n<Empty/>\n");
        }

        [TestMethod]
        public void Deserialize()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            serializer.Deserialize<Empty>("<?xml version=\"1.0\"?>\n<Empty/>\n");
        }

        [TestMethod]
        public void DeserializeWithComment()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            serializer.Deserialize<Empty>("<?xml version=\"1.0\"?>\n\t<!-- Hello there\n comment -->\n<Empty/>\n");
        }
    }

    [TestClass]
    public class TestNullables
    {
        class Foo
        {
            public string Name { get; set; }
            public Bar Bar { get; set; }
        }

        class Bar
        {
            public int Min { get; set; }
            public int Max { get; set; }
        }

        [TestMethod]
        public void SerializeNonNull()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Name = "Hello",
                Bar = new Bar
                {
                    Min = 12,
                    Max = 34,
                }
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo>
    <Name>Hello</Name>
    <Bar>
        <Min>12</Min>
        <Max>34</Max>
    </Bar>
</Foo>");
        }

        [TestMethod]
        public void DeserializeNonNull()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo>
    <Name>Bye</Name>
    <Bar>
        <Min>56</Min>
        <Max>78</Max>
    </Bar>
</Foo>");

            Assert.AreEqual(v.Name, "Bye");
            Assert.AreEqual(v.Bar.Min, 56);
            Assert.AreEqual(v.Bar.Max, 78);
        }

        [TestMethod]
        public void SerializeOneNull()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Name = null,
                Bar = new Bar
                {
                    Min = 12,
                    Max = 34,
                }
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo>
    <Bar>
        <Min>12</Min>
        <Max>34</Max>
    </Bar>
</Foo>");
        }

        [TestMethod]
        public void DeserializeOneNull()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(@"<?xml version=""1.0""?>
<Foo>
    <Bar>
        <Min>12</Min>
        <Max>34</Max>
    </Bar>
</Foo>");
            Assert.IsNull(v.Name);
            Assert.AreEqual(v.Bar.Min, 12);
            Assert.AreEqual(v.Bar.Max, 34);
        }

        [TestMethod]
        public void SerializeAllNull()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Name = null,
                Bar = null,
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo/>
");
        }

        [TestMethod]
        public void DeserializeAllNull()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(@"<?xml version=""1.0""?>
<Foo/>");
            Assert.IsNull(v.Name);
            Assert.IsNull(v.Bar);
        }
    }

    [TestClass]
    public class TestNoRoot
    {
        [XmlRoot(null)]
        class Foo
        {
            public string Name { get; set; }
            public Bar Bar { get; set; }
        }

        class Bar
        {
            public int Min { get; set; }
            public int Max { get; set; }
        }

        [TestMethod]
        public void Serialize()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Name = "Hello",
                Bar = new Bar
                {
                    Min = 12,
                    Max = 34,
                }
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Name>Hello</Name>
<Bar>
    <Min>12</Min>
    <Max>34</Max>
</Bar>");
        }

        [TestMethod]
        public void Deserialize()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Name>Bye</Name>
<Bar>
    <Min>56</Min>
    <Max>78</Max>
</Bar>");

            Assert.AreEqual(v.Name, "Bye");
            Assert.AreEqual(v.Bar.Min, 56);
            Assert.AreEqual(v.Bar.Max, 78);
        }
    }

    [TestClass]
    public class TestEscapes
    {
        class Foo
        {
            [XmlAttribute]
            public string Str1 { get; set; }
            [XmlElement]
            public string Str2 { get; set; }
        }

        [TestMethod]
        public void Serialize()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Str1 = "Hello\nThere \"quotes\"!",
                Str2 = "1 < 2 & 3 > 2",
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo Str1=""Hello&#xA;There &quot;quotes&quot;!"">
    <Str2>1 &lt; 2 &amp; 3 &gt; 2</Str2>
</Foo>");
        }

        [TestMethod]
        public void Deserialize()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo Str1=""Hello&#xA;There &quot;quotes&quot;!"">
    <Str2>1 &lt; 2 &amp; 3 &gt; 2</Str2>
</Foo>");

            Assert.AreEqual(v.Str1, "Hello\nThere \"quotes\"!");
            Assert.AreEqual(v.Str2, "1 < 2 & 3 > 2");
        }
    }

    [TestClass]
    public class TestCustomNames
    {
        [XmlRoot("my_foo")]
        class Foo
        {
            [XmlElement("the_name")]
            public string Name { get; set; }
            [XmlElement("some_sub")]
            public Bar Bar { get; set; }
        }

        class Bar
        {
            [XmlElement("minimum")]
            public int Min { get; set; }
            public int Max { get; set; }
        }

        [TestMethod]
        public void Serialize()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Name = "Hello",
                Bar = new Bar
                {
                    Min = 12,
                    Max = 34,
                }
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<my_foo>
    <the_name>Hello</the_name>
    <some_sub>
        <minimum>12</minimum>
        <Max>34</Max>
    </some_sub>
</my_foo>");
        }

        [TestMethod]
        public void Deserialize()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<my_foo>
    <the_name>Bye</the_name>
    <some_sub>
        <minimum>56</minimum>
        <Max>78</Max>
    </some_sub>
</my_foo>");

            Assert.AreEqual(v.Name, "Bye");
            Assert.AreEqual(v.Bar.Min, 56);
            Assert.AreEqual(v.Bar.Max, 78);
        }
    }

    [TestClass]
    public class TestAttributes
    {
        class Foo
        {
            [XmlAttribute]
            public string Name { get; set; }
            [XmlAttribute("id")]
            public string Id { get; set; }
            [XmlElement("some_sub")]
            public Bar Bar { get; set; }
        }

        class Bar
        {
            [XmlAttribute("minimum")]
            public int Min { get; set; }
            public int Max { get; set; }
        }

        [TestMethod]
        public void Serialize()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Name = "Hello",
                Id = "some id",
                Bar = new Bar
                {
                    Min = 12,
                    Max = 34,
                }
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo Name=""Hello"" id=""some id"">
    <some_sub minimum=""12"">
        <Max>34</Max>
    </some_sub>
</Foo>");
        }

        [TestMethod]
        public void Deserialize()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo Name=""Bye"" id=""another id"">
    <some_sub minimum=""56"">
        <Max>78</Max>
    </some_sub>
</Foo>");

            Assert.AreEqual(v.Name, "Bye");
            Assert.AreEqual(v.Id, "another id");
            Assert.AreEqual(v.Bar.Min, 56);
            Assert.AreEqual(v.Bar.Max, 78);
        }

        [TestMethod]
        public void SerializeOneNull()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Name = "Hello",
                Id = null,
                Bar = new Bar
                {
                    Min = 12,
                    Max = 34,
                }
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo Name=""Hello"">
    <some_sub minimum=""12"">
        <Max>34</Max>
    </some_sub>
</Foo>");
        }

        [TestMethod]
        public void DeserializeOneNull()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo Name=""Bye"">
    <some_sub minimum=""56"">
        <Max>78</Max>
    </some_sub>
</Foo>");

            Assert.AreEqual(v.Name, "Bye");
            Assert.IsNull(v.Id);
            Assert.AreEqual(v.Bar.Min, 56);
            Assert.AreEqual(v.Bar.Max, 78);
        }
    }

    [TestClass]
    public class TestNestedLists
    {
        class Foo
        {
            [XmlArray("MyList")]
            [XmlArrayItem("MyElement")]
            public List<Bar> Bars { get; set; }
        }

        class Bar
        {
            [XmlAttribute("hp")]
            public int Health { get; set; }
        }

        [TestMethod]
        public void SerializeNullList()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo());
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo/>");
        }

        [TestMethod]
        public void DeserializeNullList()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo/>");
            Assert.IsNull(v.Bars);
        }

        [TestMethod]
        public void SerializeEmptyList()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Bars = new List<Bar>(),
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo>
    <MyList/>
</Foo>");
        }

        [TestMethod]
        public void DeserializeEmptyList()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo>
    <MyList/>
</Foo>");
            Assert.AreEqual(v.Bars.Count, 0);
        }

        [TestMethod]
        public void Serialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Bars = new List<Bar>
                {
                    new Bar{ Health = 123 },
                    new Bar{ Health = 667 },
                    new Bar{ Health = 954 },
                },
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo>
    <MyList>
        <MyElement hp=""123""/>
        <MyElement hp=""667""/>
        <MyElement hp=""954""/>
    </MyList>
</Foo>");
        }

        [TestMethod]
        public void Deserialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo>
    <MyList>
        <MyElement hp=""34""/>
        <MyElement hp=""96""/>
    </MyList>
</Foo>");
            Assert.AreEqual(v.Bars.Count, 2);
            Assert.AreEqual(v.Bars[0].Health, 34);
            Assert.AreEqual(v.Bars[1].Health, 96);
        }
    }

    [TestClass]
    public class TestInlineLists
    {
        class Foo
        {
            [XmlElement("bar")]
            public List<Bar> Bars { get; set; }
        }

        class Bar
        {
            [XmlAttribute("hp")]
            public int Health { get; set; }
        }

        [TestMethod]
        public void SerializeNullList()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo());
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo/>");
        }

        [TestMethod]
        public void DeserializeNullList()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo/>");
            Assert.IsNull(v.Bars);
        }

        [TestMethod]
        public void SerializeEmptyList()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Bars = new List<Bar>(),
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo/>");
        }

        [TestMethod]
        public void DeserializeEmptyList()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo/>");
            Assert.IsNull(v.Bars);
        }

        [TestMethod]
        public void Serialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Bars = new List<Bar>
                {
                    new Bar{ Health = 123 },
                    new Bar{ Health = 667 },
                    new Bar{ Health = 954 },
                },
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo>
    <bar hp=""123""/>
    <bar hp=""667""/>
    <bar hp=""954""/>
</Foo>");
        }

        [TestMethod]
        public void Deserialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo>
    <bar hp=""34""/>
    <bar hp=""96""/>
</Foo>");
            Assert.AreEqual(v.Bars.Count, 2);
            Assert.AreEqual(v.Bars[0].Health, 34);
            Assert.AreEqual(v.Bars[1].Health, 96);
        }
    }

    [TestClass]
    public class TestMultipleInlineLists
    {
        class Foo
        {
            [XmlElement("bar")]
            public List<Bar> Bars { get; set; }
            [XmlElement("qux")]
            public List<Qux> Quxes { get; set; }
        }

        class Bar
        {
            [XmlAttribute("hp")]
            public int Health { get; set; }
        }

        class Qux
        {
            [XmlAttribute("name")]
            public string Name { get; set; }
        }

        [TestMethod]
        public void SerializeOnlyFirst()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Bars = new List<Bar>
                {
                    new Bar{ Health = 33 },
                    new Bar{ Health = 46 },
                }
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo>
    <bar hp=""33""/>
    <bar hp=""46""/>
</Foo>");
        }

        [TestMethod]
        public void DeserializeOnlyFirst()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo>
    <bar hp=""33""/>
    <bar hp=""46""/>
</Foo>");
            Assert.AreEqual(v.Bars.Count, 2);
            Assert.AreEqual(v.Bars[0].Health, 33);
            Assert.AreEqual(v.Bars[1].Health, 46);
            Assert.IsNull(v.Quxes);
        }

        [TestMethod]
        public void SerializeOnlySecond()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Quxes = new List<Qux>
                {
                    new Qux{ Name = "Hello there" },
                    new Qux{ Name = "Foo bar baz" },
                    new Qux{ },
                    new Qux{ Name = "fizz buzz" },
                }
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo>
    <qux name=""Hello there""/>
    <qux name=""Foo bar baz""/>
    <qux/>
    <qux name=""fizz buzz""/>
</Foo>");
        }

        [TestMethod]
        public void DeserializeOnlySecond()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo>
    <qux name=""Bye there""/>
    <qux name=""buzz buzz""/>
</Foo>");
            Assert.IsNull(v.Bars);
            Assert.AreEqual(v.Quxes.Count, 2);
            Assert.AreEqual(v.Quxes[0].Name, "Bye there");
            Assert.AreEqual(v.Quxes[1].Name, "buzz buzz");
        }

        [TestMethod]
        public void SerializeSequenced()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Bars = new List<Bar>
                {
                    new Bar{ Health = 42 },
                    new Bar{ Health = 77 },
                    new Bar{ Health = 89 },
                },
                Quxes = new List<Qux>
                {
                    new Qux{ Name = "Hello there" },
                    new Qux{ Name = "fizz buzz" },
                }
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo>
    <bar hp=""42""/>
    <bar hp=""77""/>
    <bar hp=""89""/>
    <qux name=""Hello there""/>
    <qux name=""fizz buzz""/>
</Foo>");
        }

        [TestMethod]
        public void DeserializeSequenced()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo>
    <bar hp=""42""/>
    <bar hp=""77""/>
    <bar hp=""89""/>
    <qux name=""Hello there""/>
    <qux name=""fizz buzz""/>
</Foo>");
            Assert.AreEqual(v.Bars.Count, 3);
            Assert.AreEqual(v.Bars[0].Health, 42);
            Assert.AreEqual(v.Bars[1].Health, 77);
            Assert.AreEqual(v.Bars[2].Health, 89);
            Assert.AreEqual(v.Quxes.Count, 2);
            Assert.AreEqual(v.Quxes[0].Name, "Hello there");
            Assert.AreEqual(v.Quxes[1].Name, "fizz buzz");
        }

        [TestMethod]
        public void DeserializeIntertwined()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo>
    <bar hp=""42""/>
    <qux name=""Hello there""/>
    <bar hp=""89""/>
    <bar hp=""77""/>
    <qux name=""fizz buzz""/>
</Foo>");
            Assert.AreEqual(v.Bars.Count, 3);
            Assert.AreEqual(v.Bars[0].Health, 42);
            Assert.AreEqual(v.Bars[1].Health, 89);
            Assert.AreEqual(v.Bars[2].Health, 77);
            Assert.AreEqual(v.Quxes.Count, 2);
            Assert.AreEqual(v.Quxes[0].Name, "Hello there");
            Assert.AreEqual(v.Quxes[1].Name, "fizz buzz");
        }
    }

    [TestClass]
    public class TestCustomValueType
    {
        class Foo
        {
            [XmlAttribute]
            public string Name { get; set; }
            public Position Position { get; set; }
            public Position Size { get; set; }
        }

        [TestMethod]
        public void SerializeNoRegister()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Name = "Hello",
                Position = new Position { X = 15, Y = 30 },
                Size = new Position { X = 60, Y = 75 },
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo Name=""Hello"">
    <Position>
        <X>15</X>
        <Y>30</Y>
    </Position>
    <Size>
        <X>60</X>
        <Y>75</Y>
    </Size>
</Foo>");
        }

        [TestMethod]
        public void SerializeRegister()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();
            serializer.RegisterValue(typeof(Position), new PositionSerializer());

            var xml = serializer.Serialize(new Foo
            {
                Name = "Hello",
                Position = new Position { X = 15, Y = 30 },
                Size = new Position { X = 60, Y = 75 },
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo Name=""Hello"">
    <Position>15/30</Position>
    <Size>60/75</Size>
</Foo>");
        }

        [TestMethod]
        public void DeserializeRegister()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();
            serializer.RegisterValue(typeof(Position), new PositionSerializer());

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo Name=""Qux"">
    <Position>15/30</Position>
    <Size>60/75</Size>
</Foo>");
            Assert.AreEqual(v.Name, "Qux");
            Assert.AreEqual(v.Position.X, 15);
            Assert.AreEqual(v.Position.Y, 30);
            Assert.AreEqual(v.Size.X, 60);
            Assert.AreEqual(v.Size.Y, 75);
        }
    }

    [TestClass]
    public class TestCustomValueTypeAtAttributePosition
    {
        class Foo
        {
            [XmlAttribute]
            public string Name { get; set; }
            [XmlAttribute("position")]
            public Position Position { get; set; }
            [XmlAttribute]
            public Position Size { get; set; }
        }

        [TestMethod]
        public void SerializeNoNull()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();
            serializer.RegisterValue(typeof(Position), new PositionSerializer());

            var xml = serializer.Serialize(new Foo
            {
                Name = "Hello",
                Position = new Position { X = 15, Y = 30 },
                Size = new Position { X = 60, Y = 75 },
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo Name=""Hello"" position=""15/30"" Size=""60/75""/>");
        }

        [TestMethod]
        public void DeserializeNoNull()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();
            serializer.RegisterValue(typeof(Position), new PositionSerializer());

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo Name=""Qux"" position=""34/96"" Size=""3/1"">
</Foo>");
            Assert.AreEqual(v.Name, "Qux");
            Assert.AreEqual(v.Position.X, 34);
            Assert.AreEqual(v.Position.Y, 96);
            Assert.AreEqual(v.Size.X, 3);
            Assert.AreEqual(v.Size.Y, 1);
        }

        [TestMethod]
        public void SerializeOneNull()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();
            serializer.RegisterValue(typeof(Position), new PositionSerializer());

            var xml = serializer.Serialize(new Foo
            {
                Name = "Hello",
                Position = new Position { X = 15, Y = 30 },
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo Name=""Hello"" position=""15/30""/>");
        }

        [TestMethod]
        public void DeserializeOneNull()
        {
            XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();
            serializer.RegisterValue(typeof(Position), new PositionSerializer());

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo Name=""Qux"" position=""34/96""/>");
            Assert.AreEqual(v.Name, "Qux");
            Assert.AreEqual(v.Position.X, 34);
            Assert.AreEqual(v.Position.Y, 96);
            Assert.IsNull(v.Size);
        }
    }

    [TestClass]
    public class TestEnum
    {
        enum Color
        {
            Red,
            Green,
            Blue,
        }

        class Gradient
        {
            [XmlAttribute]
            public Color Background { get; set; }
            public Color From { get; set; }
            public Color To { get; set; }
        }

        [TestMethod]
        public void Serialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Gradient
            {
                Background = Color.Blue,
                From = Color.Green,
                To = Color.Red,
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Gradient Background=""Blue"">
    <From>Green</From>
    <To>Red</To>
</Gradient>");
        }

        [TestMethod]
        public void Deserialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Gradient>(
@"<?xml version=""1.0""?>
<Gradient Background=""Blue"">
    <From>Red</From>
    <To>Green</To>
</Gradient>");
            Assert.AreEqual(v.Background, Color.Blue);
            Assert.AreEqual(v.From, Color.Red);
            Assert.AreEqual(v.To, Color.Green);
        }
    }

    [TestClass]
    public class TestNamedEnum
    {
        enum Animation
        {
            [XmlEnum("loop")]
            Repeated,
            [XmlEnum("oneshot")]
            Once,
            SomethingElse,
        }

        class Anims
        {
            public Animation Anim1 { get; set; }
            [XmlElement("second")]
            public Animation Anim2 { get; set; }
            public Animation Anim3 { get; set; }
            [XmlAttribute("default")]
            public Animation Anim4 { get; set; }
        }

        [TestMethod]
        public void Serialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Anims
            {
                Anim1 = Animation.SomethingElse,
                Anim2 = Animation.Repeated,
                Anim3 = Animation.Once,
                Anim4 = Animation.Repeated,
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Anims default=""loop"">
    <Anim1>SomethingElse</Anim1>
    <second>loop</second>
    <Anim3>oneshot</Anim3>
</Anims>");
        }

        [TestMethod]
        public void Deserialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Anims>(
@"<?xml version=""1.0""?>
<Anims default=""loop"">
    <Anim1>SomethingElse</Anim1>
    <second>loop</second>
    <Anim3>oneshot</Anim3>
</Anims>");
            Assert.AreEqual(v.Anim1, Animation.SomethingElse);
            Assert.AreEqual(v.Anim2, Animation.Repeated);
            Assert.AreEqual(v.Anim3, Animation.Once);
            Assert.AreEqual(v.Anim4, Animation.Repeated);
        }
    }

    [TestClass]
    public class TestPrimitivesInNestedList
    {
        class SomeContainer
        {
            [XmlArray("ints")]
            [XmlArrayItem("i")]
            public List<int> Ints { get; set; }
            [XmlArray("strs")]
            [XmlArrayItem("s")]
            public List<string> Strings { get; set; }
            [XmlArray("ps")]
            [XmlArrayItem("p")]
            public List<Position> Positions { get; set; }
            [XmlArray("lvls")]
            [XmlArrayItem("lvl")]
            public List<Level> Levels { get; set; }
        }

        enum Level
        {
            [XmlEnum("first")]
            One,
            Two,
            [XmlEnum("master")]
            Three,
        }

        [TestMethod]
        public void Serialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();
            serializer.RegisterValue(typeof(Position), new PositionSerializer());

            var xml = serializer.Serialize(new SomeContainer
            {
                Ints = new List<int> { 5, 6, 1 },
                Strings = new List<string> { "hi", "bye" },
                Positions = new List<Position>
                {
                    new Position { X = 12, Y = 34 },
                    new Position { X = 32, Y = 55 },
                },
                Levels = new List<Level> { Level.Three, Level.One, Level.Two },
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<SomeContainer>
    <ints>
        <i>5</i>
        <i>6</i>
        <i>1</i>
    </ints>
    <strs>
        <s>hi</s>
        <s>bye</s>
    </strs>
    <ps>
        <p>12/34</p>
        <p>32/55</p>
    </ps>
    <lvls>
        <lvl>master</lvl>
        <lvl>first</lvl>
        <lvl>Two</lvl>
    </lvls>
</SomeContainer>");
        }

        [TestMethod]
        public void Deserialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();
            serializer.RegisterValue(typeof(Position), new PositionSerializer());

            var v = serializer.Deserialize<SomeContainer>(
@"<?xml version=""1.0""?>
<SomeContainer>
    <ints>
        <i>5</i>
        <i>6</i>
        <i>1</i>
    </ints>
    <strs>
        <s>hi</s>
        <s>bye</s>
    </strs>
    <ps>
        <p>12/34</p>
        <p>32/55</p>
    </ps>
    <lvls>
        <lvl>first</lvl>
        <lvl>master</lvl>
        <lvl>Two</lvl>
    </lvls>
</SomeContainer>");
            Assert.AreEqual(v.Ints.Count, 3);
            Assert.AreEqual(v.Ints[0], 5);
            Assert.AreEqual(v.Ints[1], 6);
            Assert.AreEqual(v.Ints[2], 1);
            Assert.AreEqual(v.Strings.Count, 2);
            Assert.AreEqual(v.Strings[0], "hi");
            Assert.AreEqual(v.Strings[1], "bye");
            Assert.AreEqual(v.Positions.Count, 2);
            Assert.AreEqual(v.Positions[0].X, 12);
            Assert.AreEqual(v.Positions[0].Y, 34);
            Assert.AreEqual(v.Levels.Count, 3);
            Assert.AreEqual(v.Levels[0], Level.One);
            Assert.AreEqual(v.Levels[1], Level.Three);
            Assert.AreEqual(v.Levels[2], Level.Two);
        }
    }

    [TestClass]
    public class TestPrimitivesInInlineList
    {
        class SomeContainer
        {
            [XmlElement("i")]
            public List<int> Ints { get; set; }
            [XmlElement("s")]
            public List<string> Strings { get; set; }
            [XmlElement("p")]
            public List<Position> Positions { get; set; }
            [XmlElement("lvl")]
            public List<Level> Levels { get; set; }
        }

        enum Level
        {
            [XmlEnum("first")]
            One,
            Two,
            [XmlEnum("master")]
            Three,
        }

        [TestMethod]
        public void Serialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();
            serializer.RegisterValue(typeof(Position), new PositionSerializer());

            var xml = serializer.Serialize(new SomeContainer
            {
                Ints = new List<int> { 5, 6, 1 },
                Strings = new List<string> { "hi", "bye" },
                Positions = new List<Position>
                {
                    new Position { X = 12, Y = 34 },
                    new Position { X = 32, Y = 55 },
                },
                Levels = new List<Level> { Level.Three, Level.One, Level.Two },
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<SomeContainer>
    <i>5</i>
    <i>6</i>
    <i>1</i>
    <s>hi</s>
    <s>bye</s>
    <p>12/34</p>
    <p>32/55</p>
    <lvl>master</lvl>
    <lvl>first</lvl>
    <lvl>Two</lvl>
</SomeContainer>");
        }

        [TestMethod]
        public void Deserialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();
            serializer.RegisterValue(typeof(Position), new PositionSerializer());

            var v = serializer.Deserialize<SomeContainer>(
@"<?xml version=""1.0""?>
<SomeContainer>
    <i>5</i>
    <i>6</i>
    <i>1</i>
    <s>hi</s>
    <s>bye</s>
    <p>12/34</p>
    <p>32/55</p>
    <lvl>first</lvl>
    <lvl>master</lvl>
    <lvl>Two</lvl>
</SomeContainer>");
            Assert.AreEqual(v.Ints.Count, 3);
            Assert.AreEqual(v.Ints[0], 5);
            Assert.AreEqual(v.Ints[1], 6);
            Assert.AreEqual(v.Ints[2], 1);
            Assert.AreEqual(v.Strings.Count, 2);
            Assert.AreEqual(v.Strings[0], "hi");
            Assert.AreEqual(v.Strings[1], "bye");
            Assert.AreEqual(v.Positions.Count, 2);
            Assert.AreEqual(v.Positions[0].X, 12);
            Assert.AreEqual(v.Positions[0].Y, 34);
            Assert.AreEqual(v.Levels.Count, 3);
            Assert.AreEqual(v.Levels[0], Level.One);
            Assert.AreEqual(v.Levels[1], Level.Three);
            Assert.AreEqual(v.Levels[2], Level.Two);
        }
    }

    [TestClass]
    public class TestHiddenDefaults
    {
        enum Choice
        {
            First,
            Second,
            Third,
        }

        class Foo
        {
            [XmlHideDefault]
            [XmlAttribute]
            public Choice Choice { get; set; }
            [XmlHideDefault(5)]
            [XmlAttribute]
            public int AttribAmount { get; set; }
            [XmlHideDefault(Choice.Second)]
            public Choice Choice2 { get; set; }
            [XmlHideDefault]
            public int Amount { get; set; }
        }

        [TestMethod]
        public void SerializeNoDefaults()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Choice = Choice.Third,
                AttribAmount = 9,
                Choice2 = Choice.First,
                Amount = 3,
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo Choice=""Third"" AttribAmount=""9"">
    <Choice2>First</Choice2>
    <Amount>3</Amount>
</Foo>");
        }

        [TestMethod]
        public void SerializeSomeDefaults()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Choice = Choice.Third,
                AttribAmount = 5,
                Choice2 = Choice.Second,
                Amount = 3,
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo Choice=""Third"">
    <Amount>3</Amount>
</Foo>");
        }

        [TestMethod]
        public void SerializeAllDefaults()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Choice = Choice.First,
                AttribAmount = 5,
                Choice2 = Choice.Second,
                Amount = 0,
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo/>");
        }
    }

    [TestClass]
    public class TestNoCompoundTagOnNestedList
    {
        class Foo
        {
            [XmlNoCompoundTag]
            [XmlArray("Numbers")]
            [XmlArrayItem("Number")]
            public List<int> Numbers { get; set; }
        }

        [TestMethod]
        public void Serialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Numbers = new List<int>(),
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo>
    <Numbers>
    </Numbers>
</Foo>");
        }
    }

    [TestClass]
    public class TestNoCompoundTagOnRoot
    {
        [XmlNoCompoundTag]
        class Foo
        {
        }

        [TestMethod]
        public void Serialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo());
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo>
</Foo>");
        }
    }

    [TestClass]
    public class TestNoCompoundTagOnElement
    {
        class Foo
        {
            [XmlNoCompoundTag]
            public Bar NoCompound { get; set; }
            public Bar WithCompound { get; set; }
        }

        class Bar { }

        [TestMethod]
        public void Serialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                NoCompound = new Bar(),
                WithCompound = new Bar(),
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo>
    <NoCompound>
    </NoCompound>
    <WithCompound/>
</Foo>");
        }
    }

    [TestClass]
    public class TestNoCompoundTagOnType
    {
        class Foo
        {
            public Bar Compound1 { get; set; }
            public Bar Compound2 { get; set; }
        }

        [XmlNoCompoundTag]
        class Bar { }

        [TestMethod]
        public void Serialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Compound1 = new Bar(),
                Compound2 = new Bar(),
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo>
    <Compound1>
    </Compound1>
    <Compound2>
    </Compound2>
</Foo>");
        }
    }

    [TestClass]
    public class TestOptionalRoot
    {
        [XmlOptionalRoot]
        class Foo
        {
            public Bar Bar1 { get; set; }
            public Bar Bar2 { get; set; }
        }

        class Bar
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        [TestMethod]
        public void Serialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Bar1 = new Bar { X = 34, Y = 64 },
                Bar2 = new Bar { X = 42, Y = 11 },
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<Foo>
    <Bar1>
        <X>34</X>
        <Y>64</Y>
    </Bar1>
    <Bar2>
        <X>42</X>
        <Y>11</Y>
    </Bar2>
</Foo>");
        }

        [TestMethod]
        public void DeserializeWithRoot()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo>
    <Bar1>
        <X>34</X>
        <Y>64</Y>
    </Bar1>
    <Bar2>
        <X>42</X>
        <Y>11</Y>
    </Bar2>
</Foo>");
            Assert.AreEqual(v.Bar1.X, 34);
            Assert.AreEqual(v.Bar1.Y, 64);
            Assert.AreEqual(v.Bar2.X, 42);
            Assert.AreEqual(v.Bar2.Y, 11);
        }

        [TestMethod]
        public void DeserializeWithoutRoot()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Bar1>
    <X>34</X>
    <Y>64</Y>
</Bar1>
<Bar2>
    <X>42</X>
    <Y>11</Y>
</Bar2>");
            Assert.AreEqual(v.Bar1.X, 34);
            Assert.AreEqual(v.Bar1.Y, 64);
            Assert.AreEqual(v.Bar2.X, 42);
            Assert.AreEqual(v.Bar2.Y, 11);
        }
    }

    [TestClass]
    public class TestOptionalRootWithCustomName
    {
        [XmlOptionalRoot("some_root")]
        class Foo
        {
            public Bar Bar1 { get; set; }
            public Bar Bar2 { get; set; }
        }

        class Bar
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        [TestMethod]
        public void Serialize()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var xml = serializer.Serialize(new Foo
            {
                Bar1 = new Bar { X = 34, Y = 64 },
                Bar2 = new Bar { X = 42, Y = 11 },
            });
            Str.AreEqual(xml,
@"<?xml version=""1.0""?>
<some_root>
    <Bar1>
        <X>34</X>
        <Y>64</Y>
    </Bar1>
    <Bar2>
        <X>42</X>
        <Y>11</Y>
    </Bar2>
</some_root>");
        }

        [TestMethod]
        public void DeserializeWithRoot()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<some_root>
    <Bar1>
        <X>34</X>
        <Y>64</Y>
    </Bar1>
    <Bar2>
        <X>42</X>
        <Y>11</Y>
    </Bar2>
</some_root>");
            Assert.AreEqual(v.Bar1.X, 34);
            Assert.AreEqual(v.Bar1.Y, 64);
            Assert.AreEqual(v.Bar2.X, 42);
            Assert.AreEqual(v.Bar2.Y, 11);
        }

        [TestMethod]
        public void DeserializeWithoutRoot()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Bar1>
    <X>34</X>
    <Y>64</Y>
</Bar1>
<Bar2>
    <X>42</X>
    <Y>11</Y>
</Bar2>");
            Assert.AreEqual(v.Bar1.X, 34);
            Assert.AreEqual(v.Bar1.Y, 64);
            Assert.AreEqual(v.Bar2.X, 42);
            Assert.AreEqual(v.Bar2.Y, 11);
        }
    }

    [TestClass]
    public class TestAllowCompoundCloseOnType
    {
        class Foo
        {
            public Bar Bar { get; set; }
        }

        [XmlAllowDoubleCompoundClose]
        class Bar
        {
        }

        [TestMethod]
        public void DeserializeWithNoDoubleClose()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo>
    <Bar/>
</Foo>");
        }

        [TestMethod]
        public void DeserializeWithDoubleClose()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo>
    <Bar/>/>
</Foo>");
        }
    }

    [TestClass]
    public class TestAllowCompoundCloseOnElement
    {
        class Foo
        {
            [XmlAllowDoubleCompoundClose]
            public Bar Bar { get; set; }
        }

        class Bar
        {
        }

        [TestMethod]
        public void DeserializeWithNoDoubleClose()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo>
    <Bar/>
</Foo>");
        }

        [TestMethod]
        public void DeserializeWithDoubleClose()
        {
            var serializer = XmlSerializer.WithDefaultSerializers();

            var v = serializer.Deserialize<Foo>(
@"<?xml version=""1.0""?>
<Foo>
    <Bar/>/>
</Foo>");
        }
    }
}
