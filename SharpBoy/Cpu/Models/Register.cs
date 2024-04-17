namespace SharpBoy.Cpu.Models;

public class Register
{
    //We represent each register as a 8bit register instead of having 16bits as they actually are.
    public byte A { get; set; }
    public byte B { get; set; } //BC
    public byte C { get; set; } //BC
    public byte D { get; set; } //DE
    public byte E { get; set; } //DE
    public FlagRegister F { get; set; } = new(0);
    public byte H { get; set; } //HL
    public byte L { get; set; } //HL
    public ushort BC {
        get
        {
            ushort bc = 0;
            bc |= (ushort) ( B << 8);
            bc |= C;
            return bc;
        }
        set
        {
            B = (byte)((value & 0xFF00) >> 8);
            C = (byte)(value & 0xFF);
        }
        
    }
    public ushort DE {
        get
        {
            ushort de = 0;
            de |= (ushort) ( D << 8);
            de |= E;
            return de;
        }
        set
        {
            D = (byte)((value & 0xFF00) >> 8);
            E = (byte)(value & 0xFF);
        }
        
    }
    public ushort HL {
        get
        {
            ushort hl = 0;
            hl |= (ushort) ( H << 8);
            hl |= L;
            return hl;
        }
        set
        {
            H = (byte)((value & 0xFF00) >> 8);
            L = (byte)(value & 0xFF);
        }
    }
    
    public byte AddBytes(byte a, byte b, out bool carry, int carryValue = 0)
    {
        int sum = a + b + carryValue;
        carry = sum > byte.MaxValue;
        byte result = (byte)sum;
        
        F.Zero = result == 0;
        F.Negative = false;
        F.Carry = carry;
        
        return result;
    }

    public byte SubtractBytes(byte a, byte b, out bool borrow, int carryValue = 0)
    {
        int result = a - b - carryValue;
        borrow = result < 0;

        // If borrow occurred, adjust result by adding 256 to get correct byte value
        if (borrow)
        {
            result += 256;
        }

        byte newValue = (byte)result;
        F.Zero = newValue == 0;
        F.Negative = true;
        F.Carry = borrow;
        
        return newValue;
    }
    
    public ushort AddWord(ushort a, ushort b, out bool carry)
    {
        int sum = a + b;
        carry = sum > ushort.MaxValue;
        ushort result = (byte)sum;

        F.Zero = result == 0;
        F.Negative = false;
        F.Carry = carry;
        
        return result;
    }
}