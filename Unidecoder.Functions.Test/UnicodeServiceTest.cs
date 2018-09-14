
namespace Unidecoder.Functions.Test
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnicodeServiceTest
    {
        private Unidecoder.Functions.Services.UnicodeService unicodeService;

        [TestInitialize]
        public void Initialize()
        {
            this.unicodeService = new Services.UnicodeService();
        }

        [TestMethod]
        public void ListCharacters_EmptyInput_ReturnsEmptyList()
        {
            var result = this.unicodeService.ListCharacters("");

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void ListCharacters_SimpleInput_ReturnsList()
        {
            var result = this.unicodeService.ListCharacters("a");

            result.Should().NotBeNull();
            result[0].Character.Should().Be("a");
        }

        [TestMethod]
        public void ListCharacters_EmojiInput_ReturnsSingleChar()
        {
            string input = "🐱";
            var result = this.unicodeService.ListCharacters(input);

            input.Length.Should().Be(2, because: "this codepoint requires 2 16-bit chars");
            result.Count.Should().Be(1, because: "those two chars represent a single character");
        }

        [TestMethod]
        public void GetAllBlocks_ReturnsAll()
        {
            var result = this.unicodeService.GetAllCategories();

            result.Count.Should().BeGreaterThan(20);
        }
    }
}
