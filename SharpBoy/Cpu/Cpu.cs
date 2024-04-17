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
        _register.F.HalfCarry = ((_register.A & 0xFFF) + valueToAdd & 0xFFF) > 0xFFF;

        _register.HL = newValue;

    }

    public void Adc(byte valueToAdd)
    {
        int carryValue = _register.F.Carry ? 0 : 1;
        byte newValue = _register.AddBytes(_register.A, (byte) (valueToAdd + carryValue), out var carry);
        _register.F.HalfCarry = (_register.A & 0xF) + (valueToAdd & 0xF) > 0xF;
        SetRegisterValue(ArithmeticTarget.A, newValue);

    }

    public byte Sub(byte valueToSubtract)
    {

        byte newValue = _register.SubtractBytes(_register.A, valueToSubtract, out var borrow);
        _register.F.Zero = newValue == 0;
        _register.F.Subtract = true;
        _register.F.Carry = borrow;

        // Check for half borrow
        _register.F.HalfCarry = (valueToSubtract & 0xF) > (_register.A & 0xF);

        SetRegisterValue(ArithmeticTarget.A, newValue);

        return newValue;
    }
    
    
    #endregion Arithmetic
}