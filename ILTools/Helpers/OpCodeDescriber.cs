namespace Animaonline.ILTools
{
    public static class OpCodeDescriber
    {
        public static string Describe(EnumOpCode opCode)
        {
            switch (opCode)
            {
                case EnumOpCode.Add:
                    return "Adds two values and pushes the result onto the evaluation stack.";
                case EnumOpCode.Add_Ovf:
                    return "Adds two integers, performs an overflow check, and pushes the result onto the evaluation stack.";
                case EnumOpCode.Add_Ovf_Un:
                    return "Adds two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.";
                case EnumOpCode.And:
                    return "Computes the bitwise AND of two values and pushes the result onto the evaluation stack.";
                case EnumOpCode.Arglist:
                    return "Returns an unmanaged pointer to the argument list of the current method.";
                case EnumOpCode.Beq:
                    return "Transfers control to a target instruction if two values are equal.";
                case EnumOpCode.Beq_S:
                    return "Transfers control to a target instruction (short form) if two values are equal.";
                case EnumOpCode.Bge:
                    return "Transfers control to a target instruction if the first value is greater than or equal to the second value.";
                case EnumOpCode.Bge_S:
                    return "Transfers control to a target instruction (short form) if the first value is greater than or equal to the second value.";
                case EnumOpCode.Bge_Un:
                    return "Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.";
                case EnumOpCode.Bge_Un_S:
                    return "Transfers control to a target instruction (short form) if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.";
                case EnumOpCode.Bgt:
                    return "Transfers control to a target instruction if the first value is greater than the second value.";
                case EnumOpCode.Bgt_S:
                    return "Transfers control to a target instruction (short form) if the first value is greater than the second value.";
                case EnumOpCode.Bgt_Un:
                    return "Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.";
                case EnumOpCode.Bgt_Un_S:
                    return "Transfers control to a target instruction (short form) if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.";
                case EnumOpCode.Ble:
                    return "Transfers control to a target instruction if the first value is less than or equal to the second value.";
                case EnumOpCode.Ble_S:
                    return "Transfers control to a target instruction (short form) if the first value is less than or equal to the second value.";
                case EnumOpCode.Ble_Un:
                    return
                        "Transfers control to a target instruction if the first value is less than or equal to the second value, when comparing unsigned integer values or unordered float values.";
                case EnumOpCode.Ble_Un_S:
                    return
                        "Transfers control to a target instruction (short form) if the first value is less than or equal to the second value, when comparing unsigned integer values or unordered float values.";
                case EnumOpCode.Blt:
                    return "Transfers control to a target instruction if the first value is less than the second value.";
                case EnumOpCode.Blt_S:
                    return
                        "Transfers control to a target instruction (short form) if the first value is less than the second value.";
                case EnumOpCode.Blt_Un:
                    return
                        "Transfers control to a target instruction if the first value is less than the second value, when comparing unsigned integer values or unordered float values.";
                case EnumOpCode.Blt_Un_S:
                    return
                        "Transfers control to a target instruction (short form) if the first value is less than the second value, when comparing unsigned integer values or unordered float values.";
                case EnumOpCode.Bne_Un:
                    return
                        "Transfers control to a target instruction when two unsigned integer values or unordered float values are not equal.";
                case EnumOpCode.Bne_Un_S:
                    return
                        "Transfers control to a target instruction (short form) when two unsigned integer values or unordered float values are not equal.";
                case EnumOpCode.Box:
                    return "Converts a value type to an object reference (type O).";
                case EnumOpCode.Br:
                    return "Unconditionally transfers control to a target instruction.";
                case EnumOpCode.Br_S:
                    return "Unconditionally transfers control to a target instruction (short form).";
                case EnumOpCode.Break:
                    return
                        "Signals the Common Language Infrastructure (CLI) to inform the debugger that a break point has been tripped.";
                case EnumOpCode.Brfalse:
                    return
                        "Transfers control to a target instruction if value is false, a null reference (Nothing in Visual Basic), or zero.";
                case EnumOpCode.Brfalse_S:
                    return "Transfers control to a target instruction if value is false, a null reference, or zero.";
                case EnumOpCode.Brtrue:
                    return "Transfers control to a target instruction if value is true, not null, or non-zero.";
                case EnumOpCode.Brtrue_S:
                    return
                        "Transfers control to a target instruction (short form) if value is true, not null, or non-zero.";
                case EnumOpCode.Call:
                    return "Calls the method indicated by the passed method descriptor.";
                case EnumOpCode.Calli:
                    return
                        "Calls the method indicated on the evaluation stack (as a pointer to an entry point) with arguments described by a calling convention.";
                case EnumOpCode.Callvirt:
                    return "Calls a late-bound method on an object, pushing the return value onto the evaluation stack.";
                case EnumOpCode.Castclass:
                    return "Attempts to cast an object passed by reference to the specified class.";
                case EnumOpCode.Ceq:
                    return
                        "Compares two values. If they are equal, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.";
                case EnumOpCode.Cgt:
                    return
                        "Compares two values. If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.";
                case EnumOpCode.Cgt_Un:
                    return
                        "Compares two unsigned or unordered values. If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.";
                case EnumOpCode.Ckfinite:
                    return "Throws ArithmeticException if value is not a finite number.";
                case EnumOpCode.Clt:
                    return
                        "Compares two values. If the first value is less than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.";
                case EnumOpCode.Clt_Un:
                    return
                        "Compares the unsigned or unordered values value1 and value2. If value1 is less than value2, then the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.";
                case EnumOpCode.Constrained:
                    return "Constrains the type on which a virtual method call is made.";
                case EnumOpCode.Conv_I:
                    return "Converts the value on top of the evaluation stack to native int.";
                case EnumOpCode.Conv_I1:
                    return "Converts the value on top of the evaluation stack to int8, then extends (pads) it to int32.";
                case EnumOpCode.Conv_I2:
                    return
                        "Converts the value on top of the evaluation stack to int16, then extends (pads) it to int32.";
                case EnumOpCode.Conv_I4:
                    return "Converts the value on top of the evaluation stack to int32.";
                case EnumOpCode.Conv_I8:
                    return "Converts the value on top of the evaluation stack to int64.";
                case EnumOpCode.Conv_Ovf_I:
                    return
                        "Converts the signed value on top of the evaluation stack to signed native int, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_I_Un:
                    return
                        "Converts the unsigned value on top of the evaluation stack to signed native int, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_I1:
                    return
                        "Converts the signed value on top of the evaluation stack to signed int8 and extends it to int32, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_I1_Un:
                    return
                        "Converts the unsigned value on top of the evaluation stack to signed int8 and extends it to int32, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_I2:
                    return
                        "Converts the signed value on top of the evaluation stack to signed int16 and extending it to int32, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_I2_Un:
                    return
                        "Converts the unsigned value on top of the evaluation stack to signed int16 and extends it to int32, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_I4:
                    return
                        "Converts the signed value on top of the evaluation stack to signed int32, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_I4_Un:
                    return
                        "Converts the unsigned value on top of the evaluation stack to signed int32, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_I8:
                    return
                        "Converts the signed value on top of the evaluation stack to signed int64, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_I8_Un:
                    return
                        "Converts the unsigned value on top of the evaluation stack to signed int64, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_U:
                    return
                        "Converts the signed value on top of the evaluation stack to unsigned native int, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_U_Un:
                    return
                        "Converts the unsigned value on top of the evaluation stack to unsigned native int, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_U1:
                    return
                        "Converts the signed value on top of the evaluation stack to unsigned int8 and extends it to int32, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_U1_Un:
                    return
                        "Converts the unsigned value on top of the evaluation stack to unsigned int8 and extends it to int32, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_U2:
                    return
                        "Converts the signed value on top of the evaluation stack to unsigned int16 and extends it to int32, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_U2_Un:
                    return
                        "Converts the unsigned value on top of the evaluation stack to unsigned int16 and extends it to int32, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_U4:
                    return
                        "Converts the signed value on top of the evaluation stack to unsigned int32, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_U4_Un:
                    return
                        "Converts the unsigned value on top of the evaluation stack to unsigned int32, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_U8:
                    return
                        "Converts the signed value on top of the evaluation stack to unsigned int64, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_Ovf_U8_Un:
                    return
                        "Converts the unsigned value on top of the evaluation stack to unsigned int64, throwing OverflowException on overflow.";
                case EnumOpCode.Conv_R_Un:
                    return "Converts the unsigned integer value on top of the evaluation stack to float32.";
                case EnumOpCode.Conv_R4:
                    return "Converts the value on top of the evaluation stack to float32.";
                case EnumOpCode.Conv_R8:
                    return "Converts the value on top of the evaluation stack to float64.";
                case EnumOpCode.Conv_U:
                    return
                        "Converts the value on top of the evaluation stack to unsigned native int, and extends it to native int.";
                case EnumOpCode.Conv_U1:
                    return
                        "Converts the value on top of the evaluation stack to unsigned int8, and extends it to int32.";
                case EnumOpCode.Conv_U2:
                    return
                        "Converts the value on top of the evaluation stack to unsigned int16, and extends it to int32.";
                case EnumOpCode.Conv_U4:
                    return
                        "Converts the value on top of the evaluation stack to unsigned int32, and extends it to int32.";
                case EnumOpCode.Conv_U8:
                    return
                        "Converts the value on top of the evaluation stack to unsigned int64, and extends it to int64.";
                case EnumOpCode.Cpblk:
                    return "Copies a specified number bytes from a source address to a destination address.";
                case EnumOpCode.Cpobj:
                    return
                        "Copies the value type located at the address of an object (type &, * or native int) to the address of the destination object (type &, * or native int).";
                case EnumOpCode.Div:
                    return
                        "Divides two values and pushes the result as a floating-point (type F) or quotient (type int32) onto the evaluation stack.";
                case EnumOpCode.Div_Un:
                    return
                        "Divides two unsigned integer values and pushes the result (int32) onto the evaluation stack.";
                case EnumOpCode.Dup:
                    return
                        "Copies the current topmost value on the evaluation stack, and then pushes the copy onto the evaluation stack.";
                case EnumOpCode.Endfilter:
                    return
                        "Transfers control from the filter clause of an exception back to the Common Language Infrastructure (CLI) exception handler.";
                case EnumOpCode.Endfinally:
                    return
                        "Transfers control from the fault or finally clause of an exception block back to the Common Language Infrastructure (CLI) exception handler.";
                case EnumOpCode.Initblk:
                    return
                        "Initializes a specified block of memory at a specific address to a given size and initial value.";
                case EnumOpCode.Initobj:
                    return
                        "Initializes each field of the value type at a specified address to a null reference or a 0 of the appropriate primitive type.";
                case EnumOpCode.Isinst:
                    return "Tests whether an object reference (type O) is an instance of a particular class.";
                case EnumOpCode.Jmp:
                    return "Exits current method and jumps to specified method.";
                case EnumOpCode.Ldarg:
                    return "Loads an argument (referenced by a specified index value) onto the stack.";
                case EnumOpCode.Ldarg_0:
                    return "Loads the argument at index 0 onto the evaluation stack.";
                case EnumOpCode.Ldarg_1:
                    return "Loads the argument at index 1 onto the evaluation stack.";
                case EnumOpCode.Ldarg_2:
                    return "Loads the argument at index 2 onto the evaluation stack.";
                case EnumOpCode.Ldarg_3:
                    return "Loads the argument at index 3 onto the evaluation stack.";
                case EnumOpCode.Ldarg_S:
                    return "Loads the argument (referenced by a specified short form index) onto the evaluation stack.";
                case EnumOpCode.Ldarga:
                    return "Load an argument address onto the evaluation stack.";
                case EnumOpCode.Ldarga_S:
                    return "Load an argument address, in short form, onto the evaluation stack.";
                case EnumOpCode.Ldc_I4:
                    return "Pushes a supplied value of type int32 onto the evaluation stack as an int32.";
                case EnumOpCode.Ldc_I4_0:
                    return "Pushes the integer value of 0 onto the evaluation stack as an int32.";
                case EnumOpCode.Ldc_I4_1:
                    return "Pushes the integer value of 1 onto the evaluation stack as an int32.";
                case EnumOpCode.Ldc_I4_2:
                    return "Pushes the integer value of 2 onto the evaluation stack as an int32.";
                case EnumOpCode.Ldc_I4_3:
                    return "Pushes the integer value of 3 onto the evaluation stack as an int32.";
                case EnumOpCode.Ldc_I4_4:
                    return "Pushes the integer value of 4 onto the evaluation stack as an int32.";
                case EnumOpCode.Ldc_I4_5:
                    return "Pushes the integer value of 5 onto the evaluation stack as an int32.";
                case EnumOpCode.Ldc_I4_6:
                    return "Pushes the integer value of 6 onto the evaluation stack as an int32.";
                case EnumOpCode.Ldc_I4_7:
                    return "Pushes the integer value of 7 onto the evaluation stack as an int32.";
                case EnumOpCode.Ldc_I4_8:
                    return "Pushes the integer value of 8 onto the evaluation stack as an int32.";
                case EnumOpCode.Ldc_I4_M1:
                    return "Pushes the integer value of -1 onto the evaluation stack as an int32.";
                case EnumOpCode.Ldc_I4_S:
                    return "Pushes the supplied int8 value onto the evaluation stack as an int32, short form.";
                case EnumOpCode.Ldc_I8:
                    return "Pushes a supplied value of type int64 onto the evaluation stack as an int64.";
                case EnumOpCode.Ldc_R4:
                    return "Pushes a supplied value of type float32 onto the evaluation stack as type F (float).";
                case EnumOpCode.Ldc_R8:
                    return "Pushes a supplied value of type float64 onto the evaluation stack as type F (float).";
                case EnumOpCode.Ldelem:
                    return
                        "Loads the element at a specified array index onto the top of the evaluation stack as the type specified in the instruction. ";
                case EnumOpCode.Ldelem_I:
                    return
                        "Loads the element with type native int at a specified array index onto the top of the evaluation stack as a native int.";
                case EnumOpCode.Ldelem_I1:
                    return
                        "Loads the element with type int8 at a specified array index onto the top of the evaluation stack as an int32.";
                case EnumOpCode.Ldelem_I2:
                    return
                        "Loads the element with type int16 at a specified array index onto the top of the evaluation stack as an int32.";
                case EnumOpCode.Ldelem_I4:
                    return
                        "Loads the element with type int32 at a specified array index onto the top of the evaluation stack as an int32.";
                case EnumOpCode.Ldelem_I8:
                    return
                        "Loads the element with type int64 at a specified array index onto the top of the evaluation stack as an int64.";
                case EnumOpCode.Ldelem_R4:
                    return
                        "Loads the element with type float32 at a specified array index onto the top of the evaluation stack as type F (float).";
                case EnumOpCode.Ldelem_R8:
                    return
                        "Loads the element with type float64 at a specified array index onto the top of the evaluation stack as type F (float).";
                case EnumOpCode.Ldelem_Ref:
                    return
                        "Loads the element containing an object reference at a specified array index onto the top of the evaluation stack as type O (object reference).";
                case EnumOpCode.Ldelem_U1:
                    return
                        "Loads the element with type unsigned int8 at a specified array index onto the top of the evaluation stack as an int32.";
                case EnumOpCode.Ldelem_U2:
                    return
                        "Loads the element with type unsigned int16 at a specified array index onto the top of the evaluation stack as an int32.";
                case EnumOpCode.Ldelem_U4:
                    return
                        "Loads the element with type unsigned int32 at a specified array index onto the top of the evaluation stack as an int32.";
                case EnumOpCode.Ldelema:
                    return
                        "Loads the address of the array element at a specified array index onto the top of the evaluation stack as type & (managed pointer).";
                case EnumOpCode.Ldfld:
                    return
                        "Finds the value of a field in the object whose reference is currently on the evaluation stack.";
                case EnumOpCode.Ldflda:
                    return
                        "Finds the address of a field in the object whose reference is currently on the evaluation stack.";
                case EnumOpCode.Ldftn:
                    return
                        "Pushes an unmanaged pointer (type native int) to the native code implementing a specific method onto the evaluation stack.";
                case EnumOpCode.Ldind_I:
                    return "Loads a value of type native int as a native int onto the evaluation stack indirectly.";
                case EnumOpCode.Ldind_I1:
                    return "Loads a value of type int8 as an int32 onto the evaluation stack indirectly.";
                case EnumOpCode.Ldind_I2:
                    return "Loads a value of type int16 as an int32 onto the evaluation stack indirectly.";
                case EnumOpCode.Ldind_I4:
                    return "Loads a value of type int32 as an int32 onto the evaluation stack indirectly.";
                case EnumOpCode.Ldind_I8:
                    return "Loads a value of type int64 as an int64 onto the evaluation stack indirectly.";
                case EnumOpCode.Ldind_R4:
                    return "Loads a value of type float32 as a type F (float) onto the evaluation stack indirectly.";
                case EnumOpCode.Ldind_R8:
                    return "Loads a value of type float64 as a type F (float) onto the evaluation stack indirectly.";
                case EnumOpCode.Ldind_Ref:
                    return
                        "Loads an object reference as a type O (object reference) onto the evaluation stack indirectly.";
                case EnumOpCode.Ldind_U1:
                    return "Loads a value of type unsigned int8 as an int32 onto the evaluation stack indirectly.";
                case EnumOpCode.Ldind_U2:
                    return "Loads a value of type unsigned int16 as an int32 onto the evaluation stack indirectly.";
                case EnumOpCode.Ldind_U4:
                    return "Loads a value of type unsigned int32 as an int32 onto the evaluation stack indirectly.";
                case EnumOpCode.Ldlen:
                    return
                        "Pushes the number of elements of a zero-based, one-dimensional array onto the evaluation stack.";
                case EnumOpCode.Ldloc:
                    return "Loads the local variable at a specific index onto the evaluation stack.";
                case EnumOpCode.Ldloc_0:
                    return "Loads the local variable at index 0 onto the evaluation stack.";
                case EnumOpCode.Ldloc_1:
                    return "Loads the local variable at index 1 onto the evaluation stack.";
                case EnumOpCode.Ldloc_2:
                    return "Loads the local variable at index 2 onto the evaluation stack.";
                case EnumOpCode.Ldloc_3:
                    return "Loads the local variable at index 3 onto the evaluation stack.";
                case EnumOpCode.Ldloc_S:
                    return "Loads the local variable at a specific index onto the evaluation stack, short form.";
                case EnumOpCode.Ldloca:
                    return "Loads the address of the local variable at a specific index onto the evaluation stack.";
                case EnumOpCode.Ldloca_S:
                    return
                        "Loads the address of the local variable at a specific index onto the evaluation stack, short form.";
                case EnumOpCode.Ldnull:
                    return "Pushes a null reference (type O) onto the evaluation stack.";
                case EnumOpCode.Ldobj:
                    return "Copies the value type object pointed to by an address to the top of the evaluation stack.";
                case EnumOpCode.Ldsfld:
                    return "Pushes the value of a static field onto the evaluation stack.";
                case EnumOpCode.Ldsflda:
                    return "Pushes the address of a static field onto the evaluation stack.";
                case EnumOpCode.Ldstr:
                    return "Pushes a new object reference to a string literal stored in the metadata.";
                case EnumOpCode.Ldtoken:
                    return
                        "Converts a metadata token to its runtime representation, pushing it onto the evaluation stack.";
                case EnumOpCode.Ldvirtftn:
                    return
                        "Pushes an unmanaged pointer (type native int) to the native code implementing a particular virtual method associated with a specified object onto the evaluation stack.";
                case EnumOpCode.Leave:
                    return
                        "Exits a protected region of code, unconditionally transferring control to a specific target instruction.";
                case EnumOpCode.Leave_S:
                    return
                        "Exits a protected region of code, unconditionally transferring control to a target instruction (short form).";
                case EnumOpCode.Localloc:
                    return
                        "Allocates a certain number of bytes from the local dynamic memory pool and pushes the address (a transient pointer, type *) of the first allocated byte onto the evaluation stack.";
                case EnumOpCode.Mkrefany:
                    return "Pushes a typed reference to an instance of a specific type onto the evaluation stack.";
                case EnumOpCode.Mul:
                    return "Multiplies two values and pushes the result on the evaluation stack.";
                case EnumOpCode.Mul_Ovf:
                    return
                        "Multiplies two integer values, performs an overflow check, and pushes the result onto the evaluation stack.";
                case EnumOpCode.Mul_Ovf_Un:
                    return
                        "Multiplies two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.";
                case EnumOpCode.Neg:
                    return "Negates a value and pushes the result onto the evaluation stack.";
                case EnumOpCode.Newarr:
                    return
                        "Pushes an object reference to a new zero-based, one-dimensional array whose elements are of a specific type onto the evaluation stack.";
                case EnumOpCode.Newobj:
                    return
                        "Creates a new object or a new instance of a value type, pushing an object reference (type O) onto the evaluation stack.";
                case EnumOpCode.Nop:
                    return
                        "Fills space if opcodes are patched. No meaningful operation is performed although a processing cycle can be consumed.";
                case EnumOpCode.Not:
                    return
                        "Computes the bitwise complement of the integer value on top of the stack and pushes the result onto the evaluation stack as the same type.";
                case EnumOpCode.Or:
                    return
                        "Compute the bitwise complement of the two integer values on top of the stack and pushes the result onto the evaluation stack.";
                case EnumOpCode.Pop:
                    return "Removes the value currently on top of the evaluation stack.";
                case EnumOpCode.Prefix1:
                    return "Infrastructure. ";
                case EnumOpCode.Prefix2:
                    return "Infrastructure. ";
                case EnumOpCode.Prefix3:
                    return "Infrastructure. ";
                case EnumOpCode.Prefix4:
                    return "Infrastructure. ";
                case EnumOpCode.Prefix5:
                    return "Infrastructure. ";
                case EnumOpCode.Prefix6:
                    return "Infrastructure. ";
                case EnumOpCode.Prefix7:
                    return "Infrastructure. ";
                case EnumOpCode.Prefixref:
                    return "Infrastructure. ";
                case EnumOpCode.Readonly:
                    return
                        "Specifies that the subsequent array address operation performs no type check at run time, and that it returns a managed pointer whose mutability is restricted.";
                case EnumOpCode.Refanytype:
                    return "Retrieves the type token embedded in a typed reference.";
                case EnumOpCode.Refanyval:
                    return "Retrieves the address (type &) embedded in a typed reference.";
                case EnumOpCode.Rem:
                    return "Divides two values and pushes the remainder onto the evaluation stack.";
                case EnumOpCode.Rem_Un:
                    return "Divides two unsigned values and pushes the remainder onto the evaluation stack.";
                case EnumOpCode.Ret:
                    return
                        "Returns from the current method, pushing a return value (if present) from the callee's evaluation stack onto the caller's evaluation stack.";
                case EnumOpCode.Rethrow:
                    return "Rethrows the current exception.";
                case EnumOpCode.Shl:
                    return
                        "Shifts an integer value to the left (in zeroes) by a specified number of bits, pushing the result onto the evaluation stack.";
                case EnumOpCode.Shr:
                    return
                        "Shifts an integer value (in sign) to the right by a specified number of bits, pushing the result onto the evaluation stack.";
                case EnumOpCode.Shr_Un:
                    return
                        "Shifts an unsigned integer value (in zeroes) to the right by a specified number of bits, pushing the result onto the evaluation stack.";
                case EnumOpCode.Sizeof:
                    return "Pushes the size, in bytes, of a supplied value type onto the evaluation stack.";
                case EnumOpCode.Starg:
                    return "Stores the value on top of the evaluation stack in the argument slot at a specified index.";
                case EnumOpCode.Starg_S:
                    return
                        "Stores the value on top of the evaluation stack in the argument slot at a specified index, short form.";
                case EnumOpCode.Stelem:
                    return
                        "Replaces the array element at a given index with the value on the evaluation stack, whose type is specified in the instruction.";
                case EnumOpCode.Stelem_I:
                    return
                        "Replaces the array element at a given index with the native int value on the evaluation stack.";
                case EnumOpCode.Stelem_I1:
                    return "Replaces the array element at a given index with the int8 value on the evaluation stack.";
                case EnumOpCode.Stelem_I2:
                    return "Replaces the array element at a given index with the int16 value on the evaluation stack.";
                case EnumOpCode.Stelem_I4:
                    return "Replaces the array element at a given index with the int32 value on the evaluation stack.";
                case EnumOpCode.Stelem_I8:
                    return "Replaces the array element at a given index with the int64 value on the evaluation stack.";
                case EnumOpCode.Stelem_R4:
                    return "Replaces the array element at a given index with the float32 value on the evaluation stack.";
                case EnumOpCode.Stelem_R8:
                    return "Replaces the array element at a given index with the float64 value on the evaluation stack.";
                case EnumOpCode.Stelem_Ref:
                    return
                        "Replaces the array element at a given index with the object ref value (type O) on the evaluation stack.";
                case EnumOpCode.Stfld:
                    return "Replaces the value stored in the field of an object reference or pointer with a new value.";
                case EnumOpCode.Stind_I:
                    return "Stores a value of type native int at a supplied address.";
                case EnumOpCode.Stind_I1:
                    return "Stores a value of type int8 at a supplied address.";
                case EnumOpCode.Stind_I2:
                    return "Stores a value of type int16 at a supplied address.";
                case EnumOpCode.Stind_I4:
                    return "Stores a value of type int32 at a supplied address.";
                case EnumOpCode.Stind_I8:
                    return "Stores a value of type int64 at a supplied address.";
                case EnumOpCode.Stind_R4:
                    return "Stores a value of type float32 at a supplied address.";
                case EnumOpCode.Stind_R8:
                    return "Stores a value of type float64 at a supplied address.";
                case EnumOpCode.Stind_Ref:
                    return "Stores a object reference value at a supplied address.";
                case EnumOpCode.Stloc:
                    return
                        "Pops the current value from the top of the evaluation stack and stores it in a the local variable list at a specified index.";
                case EnumOpCode.Stloc_0:
                    return
                        "Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 0.";
                case EnumOpCode.Stloc_1:
                    return
                        "Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 1.";
                case EnumOpCode.Stloc_2:
                    return
                        "Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 2.";
                case EnumOpCode.Stloc_3:
                    return
                        "Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 3.";
                case EnumOpCode.Stloc_S:
                    return
                        "Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index (short form).";
                case EnumOpCode.Stobj:
                    return
                        "Copies a value of a specified type from the evaluation stack into a supplied memory address.";
                case EnumOpCode.Stsfld:
                    return "Replaces the value of a static field with a value from the evaluation stack.";
                case EnumOpCode.Sub:
                    return "Subtracts one value from another and pushes the result onto the evaluation stack.";
                case EnumOpCode.Sub_Ovf:
                    return
                        "Subtracts one integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.";
                case EnumOpCode.Sub_Ovf_Un:
                    return
                        "Subtracts one unsigned integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.";
                case EnumOpCode.Switch:
                    return "Implements a jump table.";
                case EnumOpCode.Tailcall:
                    return
                        "Performs a postfixed method call instruction such that the current method's stack frame is removed before the actual call instruction is executed.";
                case EnumOpCode.Throw:
                    return "Throws the exception object currently on the evaluation stack.";
                case EnumOpCode.Unaligned:
                    return
                        "Indicates that an address currently atop the evaluation stack might not be aligned to the natural size of the immediately following ldind, stind, ldfld, stfld, ldobj, stobj, initblk, or cpblk instruction.";
                case EnumOpCode.Unbox:
                    return "Converts the boxed representation of a value type to its unboxed form.";
                case EnumOpCode.Unbox_Any:
                    return
                        "Converts the boxed representation of a type specified in the instruction to its unboxed form. ";
                case EnumOpCode.Volatile:
                    return
                        "Specifies that an address currently atop the evaluation stack might be volatile, and the results of reading that location cannot be cached or that multiple stores to that location cannot be suppressed.";
                case EnumOpCode.Xor:
                    return
                        "Computes the bitwise XOR of the top two values on the evaluation stack, pushing the result onto the evaluation stack.";
            }

            return "Unknown OpCode";
        }
    }
}