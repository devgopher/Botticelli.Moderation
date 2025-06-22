using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Botticelli.Moderation.Integration.Interfaces;
using Botticelli.Shared.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Botticelli.Moderation.Filters.Tests;

[TestFixture]
public class FilterChainTests
{
    [SetUp]
    public void SetUp()
    {
        _filterMock1 = new Mock<IFilter>();
        _filterMock2 = new Mock<IFilter>();
    }

    private Mock<IFilter> _filterMock1;
    private Mock<IFilter> _filterMock2;
    private FilterChain _filterChain;

    [Test]
    public async Task FilterMessageAsync_AllFiltersPass_ReturnsPassedResult()
    {
        // Arrange
        var message = new Message { Uid = "123" };
        _filterMock1.Setup(f => f.FilterMessageAsync(message, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FilterResult
            {
                Passed = true,
                MessageId = message.Uid,
                Message = message
            });
        _filterMock2.Setup(f => f.FilterMessageAsync(message, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FilterResult
            {
                Passed = true,
                MessageId = message.Uid,
                Message = message
            });

        _filterChain = new FilterChain(new List<IFilter> { _filterMock1.Object, _filterMock2.Object });

        // Act
        var result = await _filterChain.FilterMessageAsync(message);

        // Assert
        result.Passed.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task FilterMessageAsync_OneFilterFails_ReturnsFailedResult()
    {
        // Arrange
        var message = new Message { Uid = "123" };
        _filterMock1.Setup(f => f.FilterMessageAsync(message, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FilterResult
            {
                Passed = true,
                MessageId = message.Uid,
                Message = message
            });
        _filterMock2.Setup(f => f.FilterMessageAsync(message, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FilterResult
            {
                Passed = false,
                Errors =
                [
                    "Filter 2 failed"
                ],
                MessageId = message.Uid,
                Message = message
            });

        _filterChain = new FilterChain(new List<IFilter> { _filterMock1.Object, _filterMock2.Object });

        // Act
        var result = await _filterChain.FilterMessageAsync(message);

        // Assert
        result.Passed.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Should().Contain("Filter 2 failed");
    }

    [Test]
    public async Task FilterMessageAsync_NoFilters_ReturnsErrorResult()
    {
        // Arrange
        var message = new Message { Uid = "123" };
        _filterChain = new FilterChain(new List<IFilter>());

        // Act
        var result = await _filterChain.FilterMessageAsync(message);

        // Assert
        result.Passed.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Should().Contain("No filters in a chain!");
    }

    [Test]
    public async Task FilterMessageAsync_ExceptionThrown_ReturnsErrorResult()
    {
        // Arrange
        var message = new Message { Uid = "123" };
        _filterMock1.Setup(f => f.FilterMessageAsync(message, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Unexpected error"));

        _filterChain = new FilterChain(new List<IFilter> { _filterMock1.Object });

        // Act
        var result = await _filterChain.FilterMessageAsync(message);

        // Assert
        result.Passed.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Should().Contain("Unexpected error");
    }
}