using NUnit.Framework;

namespace Day08
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("123456789012", 3, 2, 2)]
        public void NumLayers(string source, int width, int height, int expected)
        {
            //var layers = Program.ParseLayers(source, width, height);
            //Assert.That(layers.Length, Is.EqualTo(expected));
        }
    }
}
