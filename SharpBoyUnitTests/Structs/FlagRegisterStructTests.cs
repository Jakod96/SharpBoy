using SharpBoy.Structs;

namespace SharpBoyUnitTests.Structs;

public class FlagRegisterStructTests
{
    [Fact]
    public void AsByte_ConvertFromRegisterAndBack_IsEqual()
    {
        //Arrange
        var registerStruct = new FlagRegisterStruct(0b10100000);
        var registerByte = registerStruct.AsByte();
        
        //Act
        var newRegisterStruct = new FlagRegisterStruct(registerByte);
        
        //Assert
        Assert.Equal(registerStruct, newRegisterStruct);
    }
}