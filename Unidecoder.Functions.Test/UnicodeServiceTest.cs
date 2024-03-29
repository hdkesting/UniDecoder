﻿
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
        public void GetAllCategories_ReturnsAll()
        {
            var result = this.unicodeService.GetAllCategories();

            result.Count.Should().BeGreaterThan(20);
        }

        [TestMethod]
        public void GetAllBlocks_ReturnsAll()
        {
            var result = this.unicodeService.GetAllBlocks();

            result.Count.Should().BeGreaterThan(20);
        }

        [TestMethod]
        public void FindByName_NoInput_FindsNothing()
        {
            var result = this.unicodeService.FindByName("");

            result.Count.Should().Be(0);
        }

        [TestMethod]
        public void FindByName_SearchForStar_ReturnsSome()
        {
            var result = this.unicodeService.FindByName("star");

            result.Count.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void FindByName_SearchForStarWithExclusion_ReturnsLess()
        {
            var result = this.unicodeService.FindByName("star");
            var result2 = this.unicodeService.FindByName("star -egypt");
            var result3 = this.unicodeService.FindByName("star egypt");

            result2.Count.Should().BeGreaterThan(0);
            result2.Count.Should().BeLessThan(result.Count);
            (result2.Count + result3.Count).Should().Be(result.Count);
        }

        [TestMethod]
        public void FindAroundValue_SearchFor30_Returns16()
        {
            var result = this.unicodeService.FindAroundValue(30);

            result.Count.Should().Be(16);
        }

        [TestMethod]
        public void FindAroundValue_SearchFor0_Returns8()
        {
            var result = this.unicodeService.FindAroundValue(0);

            result.Count.Should().Be(8);
        }

        [TestMethod]
        public void GetCharacterCount_ReturnsMany()
        {
            var result = this.unicodeService.GetTotalCharacterCount();

            result.Should().BeGreaterThan(100_000);
        }

        [TestMethod]
        public void GetCharactersOfBlock_BasicLatin_ReturnsMany()
        {
            var result = this.unicodeService.GetCharactersOfBlock("Basic Latin");

            result.Count.Should().Be(128);
        }

        [TestMethod]
        public void GetCharactersOfBlock_Unknown_ReturnsNone()
        {
            var result = this.unicodeService.GetCharactersOfBlock("Unknown block");

            result.Count.Should().Be(0);
        }

        [TestMethod]
        public void GetCharactersOfCategory_Unknown_ReturnsNone()
        {
            var result = this.unicodeService.GetCharactersOfCategory("Unknown category");
            result.Count.Should().Be(0);
        }

        [TestMethod]
        public void GetCharactersOfCategory_OtherNumber_ReturnsMany()
        {
            var result = this.unicodeService.GetCharactersOfCategory("other number");
            result.Count.Should().BeGreaterThan(50);
        }

        [TestMethod]
        public void GetVersion_ShouldReturnValue()
        {
            var result = this.unicodeService.GetUnicodeVersion();
            result.Should().Be(new System.Version("12.1.0"));
        }
    }
}
