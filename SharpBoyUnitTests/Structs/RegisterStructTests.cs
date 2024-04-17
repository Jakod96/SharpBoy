using SharpBoy.Structs;

namespace SharpBoyUnitTests.Structs;

public class RegisterStructTests
{
    [Fact]
    public void BC_SetValueToBothRegisters_SetsCorrectValue()
    {
        var registers = new RegisterStruct();
        registers.BC = 0b1010_1111_0011_1100;
        
        Assert.Equal(0b1010_1111, registers.B);
        Assert.Equal(0b0011_1100, registers.C);
    }
    
    [Fact]
    public void DE_SetValueToBothRegisters_SetsCorrectValue()
    {
        var registers = new RegisterStruct();
        registers.DE = 0b1010_1111_0011_1100;
        
        Assert.Equal(0b1010_1111, registers.D);
        Assert.Equal(0b0011_1100, registers.E);
    }
}