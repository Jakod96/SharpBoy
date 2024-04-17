namespace SharpBoy.Structs;

public struct RegisterStruct
{
    //We represent each register as a 8bit register instead of having 16bits as they actually are.
    public byte A { get; set; }
    public byte B { get; set; } //BC
    public byte C { get; set; } //BC
    public byte D { get; set; } //DE
    public byte E { get; set; } //DE
    public FlagRegisterStruct F { get; set; }
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
}