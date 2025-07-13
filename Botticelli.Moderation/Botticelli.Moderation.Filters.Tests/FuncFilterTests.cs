using Botticelli.Shared.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Botticelli.Moderation.Filters.Tests;

[TestFixture]
public class FilterResultTests
{
    [Test]
    public void Passed_WhenNoErrorsAndPassedIsTrue_ShouldBeTrue()
    {
        // Arrange
        var filterResult = new FilterResult
        {
            MessageId = "123",
            Message = new Message { Uid = "123" }, // Fill required Message property
            Errors = [], // No errors
            Passed = true // Set passed to true
        };

        // Act
        var result = filterResult.Passed;

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void Passed_WhenErrorsPresent_ShouldBeFalse()
    {
        // Arrange
        var filterResult = new FilterResult
        {
            MessageId = "123",
            Message = new Message { Uid = "123" }, // Fill required Message property
            Errors = ["Error 1"], // One error present
            Passed = true // Set passed to true
        };

        // Act
        var result = filterResult.Passed;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void Passed_WhenPassedIsFalse_ShouldBeFalse()
    {
        // Arrange
        var filterResult = new FilterResult
        {
            MessageId = "123",
            Message = new Message { Uid = "123" }, // Fill required Message property
            Errors = [], // No errors
            Passed = false // Set passed to false
        };

        // Act
        var result = filterResult.Passed;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void Errors_ShouldBeInitializedToEmptyArray()
    {
        // Arrange
        var filterResult = new FilterResult
        {
            MessageId = "123",
            Message = new Message { Uid = "123" } // Fill required Message property
        };

        // Act
        var errors = filterResult.Errors;

        // Assert
        errors.Should().BeEmpty();
    }
}