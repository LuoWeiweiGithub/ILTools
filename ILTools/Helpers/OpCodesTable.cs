using System;
using System.Reflection.Emit;

namespace Animaonline.ILTools
{
    internal static class OpCodesTable
    {
        private static OpCode[] _singleByteOpCodes;
        private static OpCode[] _multiByteOpCodes;

        public static OpCode[] SingleByteOpCodes
        {
            get
            {
                if (_singleByteOpCodes == null || _multiByteOpCodes == null)
                    LoadOpCodes();

                return _singleByteOpCodes;
            }
        }

        public static OpCode[] MultiByteOpCodes
        {
            get
            {
                if (_singleByteOpCodes == null || _multiByteOpCodes == null)
                    LoadOpCodes();

                return _multiByteOpCodes;
            }
        }

        private static void LoadOpCodes()
        {
            _singleByteOpCodes = new OpCode[0x100];
            _multiByteOpCodes = new OpCode[0x100];

            var opCodesFields = typeof(OpCodes).GetFields();

            foreach (var fieldInfo in opCodesFields)
            {
                if (fieldInfo.FieldType != typeof(OpCode))
                    continue;

                var opCode = (OpCode)fieldInfo.GetValue(null);

                var opCodeValue = (ushort)opCode.Value;

                //1 byte
                if (opCodeValue < 0x100)
                    _singleByteOpCodes[opCodeValue] = opCode;
                //greater than 1 byte
                else
                {
                    if ((opCodeValue & 0xff00) != 0xfe00)
                        throw new Exception("Invalid OpCode.");

                    _multiByteOpCodes[opCodeValue & 0xff] = opCode;
                }
            }
        }
    }
}