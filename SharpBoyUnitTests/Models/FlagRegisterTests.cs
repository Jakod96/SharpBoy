using SharpBoy.Cpu.Models;

namespace SharpBoyUnitTests.Models;

public class FlagRegisterTests
{
    [Fact]
    public void AsByte_ConvertFromRegisterAndBack_IsEqual()
    {
        //Arrange
        var registerStruct = new FlagRegister(0b10100000);
        var registerByte = registerStruct.AsByte();
        
        //Act
        var newRegisterStruct = new FlagRegister(registerByte);
        
        //Assert
        Assert.Equal(registerStruct, newRegisterStruct);
    }
}