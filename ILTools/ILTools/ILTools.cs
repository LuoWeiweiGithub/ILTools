using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Animaonline.ILTools  
{
    public static class ILTools
    {
        public static MethodILInfo GetMethodIL(MethodInfo methodInfo)
        {
            var methodBody = methodInfo.GetMethodBody();

            if (methodBody == null)
                throw new ArgumentException("Cannot process a method without a body");

            var ilInfo = new MethodILInfo(methodInfo);

            //get IL bytes
            var ilBytes = methodBody.GetILAsByteArray();

            var position = new int();

            //process IL bytes
            while (position < ilBytes.Length)
            {
                ILInstruction instruction;
                int metadataToken;

                //fetch opcode

                byte opCodeValue = ilBytes[position];

                //increase position
                position++;

                //single byte opcode
                if (opCodeValue != 0xFE)
                    instruction = new ILInstruction(OpCodesTable.SingleByteOpCodes[opCodeValue]);

                /* 
                 * ---multi byte opcode---
                 * 0xFE indicates that this is a multi byte opcode
                 */
                else
                {
                    //get the next opcode;
                    opCodeValue = /* 0xFE */ ilBytes[position];
                    position++;
                    instruction = new ILInstruction(OpCodesTable.MultiByteOpCodes[opCodeValue]);
                }

                //set the offset
                instruction.Offset = position - 1;

                //get the operand (if any)
                switch (instruction.OpCodeInfo.OperandType)
                {
                    case OperandType.InlineBrTarget:
                        metadataToken = OperandReader.ReadInt32(ilBytes, ref position);
                        metadataToken += position;
                        instruction.Operand = metadataToken;
                        break;
                    case OperandType.InlineField:
                        metadataToken = OperandReader.ReadInt32(ilBytes, ref position);
                        instruction.Operand = methodInfo.Module.ResolveField(metadataToken);
                        break;
                    case OperandType.InlineMethod:
                        metadataToken = OperandReader.ReadInt32(ilBytes, ref position);
                        try
                        {
                            instruction.Operand = methodInfo.Module.ResolveMethod(metadataToken);
                        }
                        catch //TODO
                        {
                            instruction.Operand = methodInfo.Module.ResolveMember(metadataToken);
                        }
                        break;
                    case OperandType.InlineSig:
                        metadataToken = OperandReader.ReadInt32(ilBytes, ref position);
                        instruction.Operand = methodInfo.Module.ResolveSignature(metadataToken);
                        break;
                    case OperandType.InlineTok:
                        metadataToken = OperandReader.ReadInt32(ilBytes, ref position);
                        try
                        {
                            instruction.Operand = methodInfo.Module.ResolveType(metadataToken);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("TODO", ex);
                        }
                        break;
                    case OperandType.InlineType:
                        metadataToken = OperandReader.ReadInt32(ilBytes, ref position);
                        if (methodInfo.DeclaringType != null)
                            instruction.Operand = methodInfo.Module.ResolveType(metadataToken, methodInfo.DeclaringType.GetGenericArguments(), methodInfo.GetGenericArguments());
                        break;
                    case OperandType.InlineI:
                        {
                            instruction.Operand = OperandReader.ReadInt32(ilBytes, ref position);
                            break;
                        }
                    case OperandType.InlineI8:
                        {
                            instruction.Operand = OperandReader.ReadInt64(ilBytes, ref position);
                            break;
                        }
                    case OperandType.InlineNone:
                        {
                            instruction.Operand = null;
                            break;
                        }
                    case OperandType.InlineR:
                        {
                            instruction.Operand = OperandReader.ReadDouble(ilBytes, ref position);
                            break;
                        }
                    case OperandType.InlineString:
                        {
                            metadataToken = OperandReader.ReadInt32(ilBytes, ref position);
                            instruction.Operand = methodInfo.Module.ResolveString(metadataToken);
                            break;
                        }
                    case OperandType.InlineSwitch:
                        {
                            int count = OperandReader.ReadInt32(ilBytes, ref position);

                            var casesAddresses = new int[count];

                            for (int i = 0; i < count; i++)
                                casesAddresses[i] = OperandReader.ReadInt32(ilBytes, ref position);

                            var cases = new int[count];

                            for (int i = 0; i < count; i++)
                                cases[i] = position + casesAddresses[i];
                            break;
                        }
                    case OperandType.InlineVar:
                        {
                            instruction.Operand = OperandReader.ReadUInt16(ilBytes, ref position);
                            break;
                        }
                    case OperandType.ShortInlineBrTarget:
                        {
                            instruction.Operand = OperandReader.ReadSByte(ilBytes, ref position) + position;
                            break;
                        }
                    case OperandType.ShortInlineI:
                        {
                            instruction.Operand = OperandReader.ReadSByte(ilBytes, ref position);
                            break;
                        }
                    case OperandType.ShortInlineR:
                        {
                            instruction.Operand = OperandReader.ReadSingle(ilBytes, ref position);
                            break;
                        }
                    case OperandType.ShortInlineVar:
                        {
                            instruction.Operand = OperandReader.ReadByte(ilBytes, ref position);
                            break;
                        }
                    default:
                        throw new Exception("Unknown operand type.");
                }

                ilInfo.Instructions.Add(instruction);
            }

            return ilInfo;
        }
    }
}
