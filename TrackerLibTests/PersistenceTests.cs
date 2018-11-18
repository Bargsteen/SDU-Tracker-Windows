using System;
using System.Linq;
using Microsoft.Data.Sqlite;
using TrackerLib.Implementations;
using TrackerLib.Interfaces;
using Xunit;

namespace TrackerLibTests
{
    public class PersistenceTests : IDisposable
    {
        private readonly IPersistence _persistence;
        private readonly SqliteConnection _connection;

        public PersistenceTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            _persistence = new Persistence(_connection);
        }

        public void Dispose()
        {
            _connection.Close();
        }

        [Fact]
        public void FetchAppUsages__EmptyDatabase__FetchesNone()
        {
            // Act
            var appUsages = _persistence.FetchAppUsages(upTo: 1);

            // Assert
            Assert.Empty(appUsages);
        }

        [Fact]
        public void FetchDeviceUsages__EmptyDatabase__FetchesNone()
        {
            // Act
            var deviceUsages = _persistence.FetchDeviceUsages(upTo: 1);

            // Assert
            Assert.Empty(deviceUsages);
        }

        [Fact]
        public void FetchDeviceUsages__DbHasSingleDeviceUsage__FetchesSingle()
        {
            // Arrange
            var deviceUsage = TestHelper.MakeTestDeviceUsage();
            _persistence.Save(deviceUsage);

            // Act
            var fetchedDeviceUsages = _persistence.FetchDeviceUsages(upTo: 10);

            // Assert
            Assert.True(fetchedDeviceUsages.Count() == 1);
            Assert.Equal(deviceUsage.ParticipantIdentifier, fetchedDeviceUsages.First()?.ParticipantIdentifier);
        }

        [Fact]
        public void FetchAppUsages__DbHasSingleAppUsage__FetchesSingle()
        {
            // Arrange
            var appUsage = TestHelper.MakeTestAppUsage();
            _persistence.Save(appUsage);

            // Act
            var fetchedAppUsages = _persistence.FetchAppUsages(upTo: 10);

            // Assert
            Assert.True(fetchedAppUsages.Count() == 1);
            Assert.Equal(appUsage.ParticipantIdentifier, fetchedAppUsages.First()?.ParticipantIdentifier);
        }

        [Fact]
        public void FetchAppUsages__DbHasMultipleAppUsages__FetchesMultipleOrderedByDate()
        {
            // Arrange
            var appUsageNew = TestHelper.MakeTestAppUsage("TestId1", DateTimeOffset.MaxValue);
            var appUsageOld = TestHelper.MakeTestAppUsage("TestId2", DateTimeOffset.MinValue);
            var appUsageNow = TestHelper.MakeTestAppUsage("TestId3", DateTimeOffset.UtcNow);

            _persistence.Save(appUsageNew);
            _persistence.Save(appUsageOld);
            _persistence.Save(appUsageNow);

            // Act
            var fetchedAppUsages = _persistence.FetchAppUsages(upTo: 10);

            // Assert
            Assert.True(fetchedAppUsages.Count() == 3);
            Assert.Equal(appUsageOld.ParticipantIdentifier, fetchedAppUsages.First()?.ParticipantIdentifier);
            Assert.Equal(appUsageNew.ParticipantIdentifier, fetchedAppUsages.Last()?.ParticipantIdentifier);
        }

        [Fact]
        public void FetchDeviceUsages__DbHasMultipleDeviceUsages__FetchesMultipleOrderedByDate()
        {
            // Arrange
            var deviceUsageNew = TestHelper.MakeTestDeviceUsage("TestId1", DateTimeOffset.MaxValue);
            var deviceUsageOld = TestHelper.MakeTestDeviceUsage("TestId2", DateTimeOffset.MinValue);
            var deviceUsageNow = TestHelper.MakeTestDeviceUsage("TestId3", DateTimeOffset.UtcNow);

            _persistence.Save(deviceUsageNew);
            _persistence.Save(deviceUsageOld);
            _persistence.Save(deviceUsageNow);

            // Act
            var fetchedDeviceUsages = _persistence.FetchDeviceUsages(upTo: 10);

            // Assert
            Assert.True(fetchedDeviceUsages.Count() == 3);
            Assert.Equal(deviceUsageOld.ParticipantIdentifier, fetchedDeviceUsages.First()?.ParticipantIdentifier);
            Assert.Equal(deviceUsageNew.ParticipantIdentifier, fetchedDeviceUsages.Last()?.ParticipantIdentifier);
        }

        [Fact]
        public void DeleteUsage__DbHasSingleAppUsage__DbIsEmpty()
        {
            // Arrange
            var appUsage = TestHelper.MakeTestAppUsage();
            _persistence.Save(appUsage);

            // Act
            _persistence.Delete(appUsage);
            var fetchedAppUsages = _persistence.FetchAppUsages(upTo: 10);

            // Assert
            Assert.Empty(fetchedAppUsages);
        }

        [Fact]
        public void DeleteUsage__DbHasSingleDeviceUsage__DbIsEmpty()
        {
            // Arrange
            var deviceUsage = TestHelper.MakeTestDeviceUsage();
            _persistence.Save(deviceUsage);

            // Act
            _persistence.Delete(deviceUsage);
            var fetchedDeviceUsages = _persistence.FetchDeviceUsages(upTo: 10);

            // Assert
            Assert.Empty(fetchedDeviceUsages);
        }
    }
}
