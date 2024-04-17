namespace SharpBoy.Models;

public struct RegisterStruct
{
    //We represent each register as a 8bit register instead of having 16bits as they actually are.
    public byte A { get; set; } //AF
    public byte B { get; set; } //BC
    public byte C { get; set; } //BC
    public byte D { get; set; } //DE
    public byte E { get; set; } //DE
    public byte F { get; set; } //AF
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
            B = ((value & 0xFF00) >> 8) as byte;
        }
        
    }
}