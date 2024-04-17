namespace SharpBoy.Cpu.Models;

public class FlagRegister
{
    //BitNrs
    private const ushort ZeroFlagBytePosition = 7;
    private const ushort NegativeFlagBytePosition = 6;
    private const ushort HalfCarryBytePosition = 5;
    private const ushort CarryFlagBytePosition = 4;

    public FlagRegister(byte flags)
    {
        Zero = ((flags >> ZeroFlagBytePosition) & 0b1) != 0;
        Negative = ((flags >> NegativeFlagBytePosition) & 0b1) != 0;
        HalfCarry = ((flags >> HalfCarryBytePosition) & 0b1) != 0;
        Carry = ((flags >> CarryFlagBytePosition) & 0b1) != 0;
    }
    
    public bool Zero { get; set; }
    public bool Negative { get; set; }
    public bool HalfCarry { get; set; }
    public bool Carry { get; set; }

    public byte AsByte()
    {
        byte flagResultAsByte = 0;
        if (Zero)
        {
            flagResultAsByte |= 1 << ZeroFlagBytePosition;
        }
        
        if (Negative)
        {
            flagResultAsByte |= 1 << NegativeFlagBytePosition;
        }
        
        if (HalfCarry)
        {
            flagResultAsByte |= 1 << HalfCarryBytePosition;
        }
        
        if (Carry)
        {
            flagResultAsByte |= 1 << CarryFlagBytePosition;
        }

        return flagResultAsByte;
    }
    
}