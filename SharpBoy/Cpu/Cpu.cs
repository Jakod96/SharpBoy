using SharpBoy.Cpu.CpuInstructions.Enums;
using SharpBoy.Cpu.Models;

namespace SharpBoy.Cpu;

public class Cpu
{
    public Register _register;

    public Cpu(Register register)
    {
        _register = register;
    }

    public void Execute(Instructions instructions)
    {
        switch (instructions)
        {
            case Instructions.Add:
            default:
                return;
        }
    }

    private byte GetRegisterValue(ArithmeticTarget arithmeticTarget)
    {
        return arithmeticTarget switch
        {
            ArithmeticTarget.A => _register.A,
            ArithmeticTarget.B => _register.B,
            ArithmeticTarget.C => _register.C,
            ArithmeticTarget.D => _register.D,
            ArithmeticTarget.E => _register.E,
            ArithmeticTarget.H => _register.H,
            ArithmeticTarget.L => _register.L,
            _ => throw new ArgumentOutOfRangeException(nameof(arithmeticTarget), arithmeticTarget, null)
        };
    }

    private void SetRegisterValue(ArithmeticTarget arithmeticTarget, byte newValue)
    {
        switch (arithmeticTarget)
        {
            case ArithmeticTarget.A:
                _register.A = newValue;
                break;
            case ArithmeticTarget.B:
                _register.B = newValue;
                break;
            case ArithmeticTarget.C:
                _register.C = newValue;
                break;
            case ArithmeticTarget.D:
                _register.D = newValue;
                break;
            case ArithmeticTarget.E:
                _register.E = newValue;
                break;
            case ArithmeticTarget.H:
                _register.H = newValue;
                break;
            case ArithmeticTarget.L:
                _register.L = newValue;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(arithmeticTarget), arithmeticTarget, null);
        }
    }
    
    #region Arithmetic

    public void Add(byte valueToAdd)
    {
        byte newValue = _register.AddBytes(_register.A, valueToAdd, out var carry);
        // Half Carry is set if adding the lower nibbles of the value and register A
        // together (plus the optional carry bit) result in a value bigger the 0xF.
        // If the result is larger than 0xF than the addition caused a carry from
        // the lower nibble to the upper nibble.
        _register.F.HalfCarry = (_register.A & 0xF) + (valueToAdd & 0xF) > 0xF;
        SetRegisterValue(ArithmeticTarget.A, newValue);
    }

    public void AddHl(ushort valueToAdd)
    {
        ushort registerValue = _register.HL;

        ushort newValue = _register.AddWord(registerValue, valueToAdd, out var carry);
        SetHalfCarry(_register.A, valueToAdd); //TODO: Check if it really should be A

        _register.HL = newValue;

    }

    public void Adc(byte valueToAdd)
    {
        int carryValue = _register.F.Carry ? 1 : 0;
        byte newValue = _register.AddBytes(_register.A, valueToAdd, out var carry, carryValue);
        if (carry)
        {
            SetHalfCarryWithCarry(_register.A, valueToAdd);
            
        }
        else
        {
            SetHalfCarry(_register.A, valueToAdd);
        }
        SetRegisterValue(ArithmeticTarget.A, newValue);

    }

    public void Sub(byte valueToSubtract)
    {
        byte newValue = _register.SubtractBytes(_register.A, valueToSubtract, out var borrow);
        SetHalfCarrySub(_register.A, valueToSubtract);
        SetRegisterValue(ArithmeticTarget.A, newValue);
    }

    public void Sbc(byte valueToSubtract)
    {
        int carryValue = _register.F.Carry ? 1 : 0;
        byte newValue = _register.SubtractBytes(_register.A, valueToSubtract, out var borrow, carryValue);

        if (borrow)
        {
            SetHalfCarrySubWithCarry(_register.A, valueToSubtract);
        }
        else
        {
            SetHalfCarrySub(_register.A, valueToSubtract);
        }

        SetRegisterValue(ArithmeticTarget.A, newValue);
    }


    public void And(byte value)
    {
        byte result = (byte)(_register.A & value);
        _register.F.Zero = result == 0;
        _register.F.Negative = false;
        _register.F.HalfCarry = true;
        _register.F.Carry = false;
        _register.A = result;
    }

    public void Xor(byte value)
    {
        byte result = (byte)(_register.A & value);
        _register.F.Zero = result == 0;
        _register.F.Negative = false;
        _register.F.HalfCarry = false;
        _register.F.Carry = false;
        _register.A = result;
    }
    
    public void Or(byte value)
    {
        byte result = (byte)(_register.A | value);
        _register.F.Zero = result == 0;
        _register.F.Negative = false;
        _register.F.HalfCarry = false;
        _register.F.Carry = false;
        _register.A = result;
    }

    public void Cp(byte value)
    {
        int result = _register.A - value;
        _register.F.Negative = false;
        _register.F.Zero = result == 0;
        SetHalfCarrySub(_register.A, value);
        SetCarry(result);
    }

    public byte Inc(byte value)
    {
        int result = value + 1;
        _register.F.Zero = (result & 0xFF) == 0;
        _register.F.Negative = false;
        SetHalfCarry(value, 1);
        return (byte) result;
    }
    
    public byte Dec(byte value)
    {
        int result = value - 1;
        _register.F.Zero = (result & 0xFF) == 0;
        _register.F.Negative = true;
        SetHalfCarrySub(value, 1);
        return (byte) result;
    }


    #endregion Arithmetic


    #region FlagHelpers
    private void SetHalfCarry(ushort value1, ushort value2)
    {
        _register.F.HalfCarry = (value1 & 0xFFF) + (value2 & 0xFFF) > 0xFFF;

    }
    
    private void SetHalfCarry(byte value1, byte value2)
    {
        _register.F.HalfCarry = (value1 & 0xF) + (value2 & 0xF) > 0xF;
    }
    private void SetHalfCarryWithCarry(byte value1, byte value2)
    {
        _register.F.HalfCarry = (value1 & 0xF) + (value2 & 0xF) >= 0xF;
    }
    
    private void SetHalfCarrySub(byte value1, byte value2)
    {
        _register.F.HalfCarry = (value1 & 0xF) < (value2 & 0xF);
    }
    private void SetHalfCarrySubWithCarry(byte b1, byte b2) {
        int carry = _register.F.Carry ? 1 : 0;
        _register.F.HalfCarry = (b1 & 0xF) < ((b2 & 0xF) + carry);
    }

    private void SetCarry(int i)
    {
        _register.F.Carry = (i >> 8) != 0;
    }
    #endregion
}