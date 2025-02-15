using ASpaceGame.CoreComponents.Enums;
using ASpaceGame.CoreComponents.GameClasses.Impl;

namespace ASpaceGame.Tests.GameClasses;

public class ShipFacilityTests
{
    private readonly ShipFacility _shipFacility;

    public ShipFacilityTests()
    {
        _shipFacility = new ShipFacility("Speed Device", FacilityTypesEnum.Device_SublightEngine, 100, 50)
        {
            Count = 1
        };
    }

    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        Assert.Equal("Speed Device", _shipFacility.Name);
        Assert.Equal(FacilityTypesEnum.Device_SublightEngine, _shipFacility.FacilityType);
        Assert.Equal(100, _shipFacility.PowerProvided);
        Assert.Equal(0, _shipFacility.IdlePowerConsumption);
        Assert.Equal(50, _shipFacility.HoldingCapacity);
    }

    [Fact]
    public void Count_ShouldThrowException_WhenValueIsOutOfRange()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _shipFacility.Count = 0);
        Assert.Throws<ArgumentOutOfRangeException>(() => _shipFacility.Count = 2);
    }

    [Fact]
    public void Count_ShouldSetAndGetCorrectValue()
    {
        _shipFacility.Count = 1;
        Assert.Equal(1, _shipFacility.Count);
    }

    [Fact]
    public void CurrentCapacity_ShouldThrowException_WhenFacilityHasNoHoldingCapacity()
    {
        var facilityWithoutCapacity = new ShipFacility("Sensor Array", FacilityTypesEnum.Facilities_SensorArray, 100, null);
        Assert.Throws<InvalidOperationException>(() => facilityWithoutCapacity.CurrentCapacity = 10);
    }

    [Fact]
    public void CurrentCapacity_ShouldSetAndGetCorrectValue()
    {
        _shipFacility.CurrentCapacity = 30;
        Assert.Equal(30, _shipFacility.CurrentCapacity);
    }

    [Fact]
    public void DamageFacility_ShouldDamageFirstUndamagedFacility()
    {
        _shipFacility.Count = 1;
        _shipFacility.DamageFacility();
        Assert.True(_shipFacility.IsDamaged(1));
    }

    [Fact]
    public void RepairFacility_ShouldRepairFirstDamagedFacility()
    {
        _shipFacility.Count = 1;
        _shipFacility.DamageFacility();
        _shipFacility.RepairFacility();
        Assert.False(_shipFacility.IsDamaged(1));
    }

    [Fact]
    public void TestFacility_ShouldReturnTrue_WhenFacilityIsUsable()
    {
        _shipFacility.Count = 1;
        bool result = _shipFacility.TestFacility(50, 200);
        Assert.True(result);
    }

    [Fact]
    public void TestFacility_ShouldReturnFalse_WhenFacilityIsDamaged()
    {
        _shipFacility.Count = 1;
        _shipFacility.DamageFacility();
        bool result = _shipFacility.TestFacility(50, 200);
        Assert.False(result);
    }

    [Fact]
    public void TestFacility_ShouldReturnFalse_WhenInsufficientPower()
    {
        var facility = new ShipFacility("Cloaking Device", FacilityTypesEnum.Defense_CloakingDevice, -100, 50)
        {
            Count = 1
        };
        bool result = facility.TestFacility(500, 200);
        Assert.False(result);
    }
}
