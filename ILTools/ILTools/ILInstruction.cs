using System;
using System.Globalization;
using System.Reflection.Emit;

namespace Animaonline.ILTools
{
    public class ILInstruction
    {
        #region Public Constructor

        public ILInstruction(OpCode opCode)
        {
            this.OpCodeInfo = opCode;
            this.OpCode = (EnumOpCode)opCode.Value;
        }

        #endregion

        #region Public Properties

        public EnumOpCode OpCode { get; private set; }

        public OpCode OpCodeInfo { get; private set; }

        public object Operand { get; set; }

        public byte[] OperandData { get; set; }

        public int Offset { get; set; }

        #endregion

        #region Public Methods

        public string GetCode()
        {
            string result = "";
            result += GetExpandedOffset(Offset) + " : " + OpCodeInfo;
            if (Operand != null)
            {
                switch (OpCodeInfo.OperandType)
                {
                    case OperandType.InlineField:
                        var fOperand = ((System.Reflection.FieldInfo)Operand);
                        result += " " + ProcessSpecialTypes(fOperand.FieldType.ToString()) + " " +
                            ProcessSpecialTypes(fOperand.ReflectedType.ToString()) +
                            "::" + fOperand.Name + "";
                        break;
                    case OperandType.InlineMethod:
                        try
                        {
                            var mOperand = (System.Reflection.MethodInfo)Operand;
                            result += " ";
                            if (!mOperand.IsStatic) result += "instance ";
                            result += ProcessSpecialTypes(mOperand.ReturnType.ToString()) +
                                " " + ProcessSpecialTypes(mOperand.ReflectedType.ToString()) +
                                "::" + mOperand.Name + "()";
                        }
                        catch
                        {
                            try
                            {
                                var mOperand = (System.Reflection.ConstructorInfo)Operand;
                                result += " ";
                                if (!mOperand.IsStatic) result += "instance ";
                                result += "void " +
                                    ProcessSpecialTypes(mOperand.ReflectedType.ToString()) +
                                    "::" + mOperand.Name + "()";
                            }
                            catch
                            {
                            }
                        }
                        break;
                    case OperandType.ShortInlineBrTarget:
                    case OperandType.InlineBrTarget:
                        result += " " + GetExpandedOffset((int)Operand);
                        break;
                    case OperandType.InlineType:
                        result += " " + ProcessSpecialTypes(Operand.ToString());
                        break;
                    case OperandType.InlineString:
                        if (Operand.ToString() == "\r\n") result += " \"\\r\\n\"";
                        else result += " \"" + Operand + "\"";
                        break;
                    case OperandType.ShortInlineVar:
                        result += Operand.ToString();
                        break;
                    case OperandType.InlineI:
                    case OperandType.InlineI8:
                    case OperandType.InlineR:
                    case OperandType.ShortInlineI:
                    case OperandType.ShortInlineR:
                        result += Operand.ToString();
                        break;
                    case OperandType.InlineTok:
                        if (Operand is Type)
                            result += ((Type)Operand).FullName;
                        else
                            result += "not supported";
                        break;

                    default: result += "not supported"; break;
                }
            }
            return result;

        }

        #endregion

        #region Private Static Methods

        private static string ProcessSpecialTypes(string typeName)
        {
            string result = typeName;
            switch (typeName)
            {
                case "System.string":
                case "System.String":
                case "String":
                    result = "string"; break;
                case "System.Int32":
                case "Int":
                case "Int32":
                    result = "int"; break;
            }
            return result;
        }

        private string GetExpandedOffset(long offset)
        {
            string result = offset.ToString(CultureInfo.InvariantCulture);
            for (int i = 0; result.Length < 4; i++)
            {
                result = "0" + result;
            }
            return result;
        }

        #endregion

        #region Overridden Methods

        public override string ToString()
        {
            return GetCode();
        }

        #endregion
    }
}