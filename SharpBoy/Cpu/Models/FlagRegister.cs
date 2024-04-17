namespace SharpBoy.Cpu.Models;

public class FlagRegister
{
    //BitNrs
    private const ushort ZeroFlagBytePosition = 7;
    private const ushort SubtractFlagBytePosition = 6;
    private const ushort HalfCarryBytePosition = 5;
    private const ushort CarryFlagBytePosition = 4;

    public FlagRegister(byte flags)
    {
        Zero = ((flags >> ZeroFlagBytePosition) & 0b1) != 0;
        Subtract = ((flags >> SubtractFlagBytePosition) & 0b1) != 0;
        HalfCarry = ((flags >> HalfCarryBytePosition) & 0b1) != 0;
        Carry = ((flags >> CarryFlagBytePosition) & 0b1) != 0;
    }
    
    public bool Zero { get; set; }
    public bool Subtract { get; set; }
    public bool HalfCarry { get; set; }
    public bool Carry { get; set; }

    public byte AsByte()
    {
        byte flagResultAsByte = 0;
        if (Zero)
        {
            flagResultAsByte |= 1 << ZeroFlagBytePosition;
        }
        
        if (Subtract)
        {
            flagResultAsByte |= 1 << SubtractFlagBytePosition;
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