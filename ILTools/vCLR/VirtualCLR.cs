using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Animaonline.ILTools.vCLR
{
    public class VirtualCLR
    {
        #region Public Constructor

        /// <summary>
        /// Creates a new instance of VirtualCLR
        /// </summary>
        /// <param name="scope">The scope in which the methods will be executed</param>
        public VirtualCLR(vCLRScope scope = vCLRScope.Method)
        {
            if (scope == vCLRScope.Global)
                throw new NotImplementedException();

            this.Scope = scope;
        }

        #endregion

        #region Public Properties

        public vCLRScope Scope { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes a list of IL instructions.
        /// </summary>
        /// <param name="methodILContext">Instructions to execute</param>
        /// <param name="callerContext">A reference to caller's context</param>
        public void ExecuteILMethod(MethodILInfo methodILContext, VCLRExecContext callerContext = null)
        {
            Console.WriteLine("\r\n--Executing Instructions--\r\n");
            foreach (var instruction in methodILContext.Instructions)
                Console.WriteLine(instruction);

            //TODO: Set locals boundaries
            var vCLRExecContext = new VCLRExecContext(methodILContext);

            if (callerContext != null && callerContext.Arguments != null)
                vCLRExecContext.Arguments = callerContext.Arguments;

            var position = new int();

            var offsetMappings = methodILContext.Instructions.ToDictionary(ilInstruction => ilInstruction.Offset, ilInstruction => methodILContext.Instructions.IndexOf(ilInstruction));

            //process the instructions
            while (position < methodILContext.Instructions.Count)
            {
                var instruction = methodILContext.Instructions[position++];

                object targetOffset = ExecuteInstruction(instruction, vCLRExecContext, callerContext);

                //branch if requested
                if (targetOffset != null)
                {
                    //get the position by the given offset
                    position = offsetMappings[(int)targetOffset];
                }
            }
        }

        #endregion

        #region OpCode Implementations

        /// <summary>
        /// Executes the IL instruction, returns an offset if branching was requested.
        /// </summary>
        /// <param name="instruction">The instruction to execute</param>
        /// <param name="vCLRExecContext">The context of the executed method</param>
        /// <param name="callerContext">A reference to caller's context</param>
        /// <returns>Returns an offset (if branching was requested)</returns>
        private object ExecuteInstruction(ILInstruction instruction, VCLRExecContext vCLRExecContext, VCLRExecContext callerContext = null)
        {
            switch (instruction.OpCode)
            {
                case EnumOpCode.Nop:
                    break; //do nothing
                case EnumOpCode.Ret: //Returns from the current method, pushing a return value (if present) from the callee's evaluation stack onto the caller's evaluation stack.
                    Ret(instruction, vCLRExecContext, callerContext);
                    break;
                case EnumOpCode.Blt:
                    Blt(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Pop:
                    Pop(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldc_I4:
                    Ldc_I4(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldc_I4_0:
                    Ldc_I4_0(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldc_I4_1:
                    Ldc_I4_1(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldc_I4_2:
                    Ldc_I4_2(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldc_I4_3:
                    Ldc_I4_3(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldc_I4_4:
                    Ldc_I4_4(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldc_I4_5:
                    Ldc_I4_5(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldc_I4_7:
                    Ldc_I4_7(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldc_I4_8:
                    Ldc_I4_8(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldc_I4_M1:
                    Ldc_I4_M1(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldc_I4_S:
                    Ldc_I4_S(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldloc_S:
                    Ldloc_S(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldloc_0:
                    Ldloc_0(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldloc_1:
                    Ldloc_1(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldloc_2:
                    Ldloc_2(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldloc_3:
                    Ldloc_3(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Stloc_S:
                    Stloc(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Stloc_0:
                    Stloc_0(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Stloc_1:
                    Stloc_1(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Stloc_2:
                    Stloc_2(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Stloc_3:
                    Stloc_3(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Br_S:
                    return Br_S(instruction, vCLRExecContext);
                case EnumOpCode.Blt_S:
                    return Blt_S(instruction, vCLRExecContext);
                case EnumOpCode.Clt:
                    Clt(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Cgt:
                    Cgt(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ceq:
                    Ceq(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Br:
                    return Br(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Brtrue_S:
                    return Brtrue_S(instruction, vCLRExecContext);
                case EnumOpCode.Ldstr:
                    Ldstr(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Add:
                    Add(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Call:
                    Call(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Callvirt:
                    Callvirt(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldarg_0:
                    Ldarg_0(instruction, vCLRExecContext, callerContext);
                    break;
                case EnumOpCode.Ldarg_1:
                    Ldarg_1(instruction, vCLRExecContext, callerContext);
                    break;
                case EnumOpCode.Ldarg_2:
                    Ldarg_2(instruction, vCLRExecContext, callerContext);
                    break;
                case EnumOpCode.Ldarg_3:
                    Ldarg_3(instruction, vCLRExecContext, callerContext);
                    break;
                case EnumOpCode.Box:
                    Box(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Stfld:
                    Stfld(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Newobj:
                    Newobj(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Dup:
                    Dup(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Newarr:
                    Newarr(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Stelem_Ref:
                    Stelem_Ref(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Brtrue:
                    return Brtrue(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Leave_S:
                    return Leave_S(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldlen:
                    Ldlen(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Conv_I4:
                    Conv_I4(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldelem_Ref:
                    Ldelem_Ref(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Brfalse_S:
                    return Brfalse_S(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldsfld:
                    Ldsfld(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldsflda:
                    Ldsflda(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldnull:
                    Ldnull(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldftn:
                    Ldftn(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Stsfld:
                    Stsfld(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ldfld:
                    Ldfld(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Volatile:
                    Volatile(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Mkrefany:
                    Mkrefany(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Throw:
                    Throw(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Bne_Un_S:
                    return Bne_Un_S(instruction, vCLRExecContext);
                case EnumOpCode.Ldloca_S:
                    Ldloca_S(instruction, vCLRExecContext);
                    break;
                case EnumOpCode.Ble_S:
                    return Ble_S(instruction, vCLRExecContext);
                case EnumOpCode.Conv_I8:
                    Conv_I8(instruction, vCLRExecContext);
                    break;
                default:
                    throw new NotImplementedException(string.Format("OpCode {0} - Not Implemented\r\nDescription: {1}", instruction.OpCodeInfo.Name, OpCodeDescriber.Describe(instruction.OpCode)));
            }

            return null;
        }

        /// <summary>
        /// Converts the value on top of the evaluation stack to int64.
        /// </summary>
        private void Conv_I8(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            //Description: Converts the value on top of the evaluation stack to int32.
            var value = vCLRExecContext.StackPop();
            var i1 = Convert.ToInt64(value);
            vCLRExecContext.StackPush(i1);
        }

        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is less than or equal to the second value.
        /// </summary>
        private object Ble_S(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var i2 = (int)vCLRExecContext.StackPop();
            var i1 = (int)vCLRExecContext.StackPop();
            if (i1 < i2 || i1 == i2)
                return (int)instruction.Operand;

            return null;
        }

        /// <summary>
        /// Loads the address of the local variable at a specific index onto the evaluation stack, short form.
        /// </summary>
        private void Ldloca_S(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var gc = GCHandle.Alloc(vCLRExecContext.MethodLocals[(byte)instruction.Operand], GCHandleType.Pinned);
            var ptr = gc.AddrOfPinnedObject();

            vCLRExecContext.StackPush(ptr);
        }

        /// <summary>
        /// Transfers control to a target instruction (short form) when two unsigned integer values or unordered float values are not equal.
        /// </summary>
        private object Bne_Un_S(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var o2 = vCLRExecContext.StackPop();
            var o1 = vCLRExecContext.StackPop();
            if (o1 != o2)
                return (int)instruction.Operand;

            return null;
        }

        private void Throw(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var ex = (Exception)vCLRExecContext.StackPop();

            throw (ex);
        }

        //This bypasses the restriction that you can't have a pointer to T,
        //letting you write very high-performance generic code.
        //It's dangerous if you don't know what you're doing, but very worth if you do.
        static T Read<T>(IntPtr address)
        {
            var obj = default(T);
            var tr = __makeref(obj);

            //This is equivalent to shooting yourself in the foot
            //but it's the only high-perf solution in some cases
            //it sets the first field of the TypedReference (which is a pointer)
            //to the address you give it, then it dereferences the value.
            //Better be 10000% sure that your type T is unmanaged/blittable...
            unsafe { *(IntPtr*)(&tr) = address; }

            return __refvalue( tr,T);
        }

        /// <summary>
        /// Pushes a typed reference to an instance of a specific type onto the evaluation stack.
        /// </summary>
        private void Mkrefany(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var targetType = (Type)instruction.Operand;

            var ptr = (IntPtr)vCLRExecContext.StackPop();
        }

        /// <summary>
        /// Finds the value of a field in the object whose reference is currently on the evaluation stack.
        /// </summary>
        private void Ldfld(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var o1 = vCLRExecContext.StackPop();

            var fieldInfo = (FieldInfo)instruction.Operand;

            var value = fieldInfo.GetValue(o1);

            vCLRExecContext.StackPush(value);
        }

        /// <summary>
        /// Specifies that an address currently atop the evaluation stack might be volatile, and the results of reading that location cannot be cached or that multiple stores to that location cannot be suppressed.
        /// </summary>
        private void Volatile(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Replaces the value of a static field with a value from the evaluation stack.
        /// </summary>
        private void Stsfld(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var fieldInfo = (FieldInfo)instruction.Operand;

            var o1 = vCLRExecContext.StackPop();

            fieldInfo.SetValue(vCLRExecContext, o1);
        }

        /// <summary>
        /// Pushes an unmanaged pointer (type native int) to the native code implementing a specific method onto the evaluation stack.
        /// </summary>
        private void Ldftn(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var methodInfo = (MethodInfo)instruction.Operand;

            var ptr = methodInfo.MethodHandle.GetFunctionPointer();

            vCLRExecContext.StackPush(ptr);
        }

        /// <summary>
        /// Pushes a null reference (type O) onto the evaluation stack.
        /// </summary>
        private void Ldnull(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPush(null);
        }

        /// <summary>
        /// Pushes the address of a static field onto the evaluation stack.
        /// </summary> 
        private void Ldsflda(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var fieldInfo = (FieldInfo)instruction.Operand;

            var o1 = fieldInfo.FieldHandle.Value;

            vCLRExecContext.StackPush(o1);
        }

        /// <summary>
        /// Pushes the value of a static field onto the evaluation stack.
        /// </summary>
        private void Ldsfld(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var fieldInfo = (FieldInfo)instruction.Operand;

            var o1 = fieldInfo.GetValue(null);

            vCLRExecContext.StackPush(o1);
        }

        /// <summary>
        /// Transfers control to a target instruction if value is false, a null reference, or zero.
        /// </summary>
        private object Brfalse_S(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var o1 = vCLRExecContext.StackPop();

            if (o1 != null)
                if (isNumericType(o1))
                {
                    var i1 = Convert.ToInt32(o1);
                    if (i1 == 0)
                        return (int)instruction.Operand;
                }
                else
                    return (int)instruction.Operand;

            return null;
        }

        /// <summary>
        /// Loads the element containing an object reference at a specified array index onto the top of the evaluation stack as type O (object reference).
        /// </summary>
        private void Ldelem_Ref(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var index = (int)vCLRExecContext.StackPop();
            var array = (Array)vCLRExecContext.StackPop();
            var value = array.GetValue(index);

            vCLRExecContext.StackPush(value);
        }

        private void Conv_I4(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            //Description: Converts the value on top of the evaluation stack to int32.
            var value = vCLRExecContext.StackPop();
            var i1 = Convert.ToInt32(value);
            vCLRExecContext.StackPush(i1);
        }

        private void Ldlen(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var array = (Array)vCLRExecContext.StackPop();
            vCLRExecContext.StackPush(array.Length);
        }

        private object Leave_S(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            //Description: Exits a protected region of code, unconditionally transferring control to a target instruction (short form).
            return instruction.Operand;
        }

        private void Ldc_I4_3(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPush(3);
        }

        private void Ldc_I4_2(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPush(2);
        }

        private object Brtrue(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            //Transfers control to a target instruction (short form) if value is true, not null, or non-zero.
            var o1 = vCLRExecContext.StackPop();

            if (o1 != null)
            {
                if (o1 is bool && ((bool)o1)) //bool && true
                    return (int)instruction.Operand;
                if (isNumericType(o1))
                {
                    var i1 = Convert.ToInt32(o1);
                    if (i1 != 0)
                        return (int)instruction.Operand;
                }
            }

            return null;
        }

        private void Stelem_Ref(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var value = vCLRExecContext.StackPop();
            var index = (int)vCLRExecContext.StackPop();
            var array = (Array)vCLRExecContext.StackPop();

            array.SetValue(value, index);
        }

        private void Ldloc_3(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.Ldloc(3);
        }

        private void Stloc_3(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.Stloc(3);
        }

        private void Newarr(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            //Description: Pushes an object reference to a new zero-based, one-dimensional array whose elements are of a specific type onto the evaluation stack.
            var length = (int)vCLRExecContext.StackPop();
            var arrInst = Array.CreateInstance((Type)instruction.Operand, length);
            vCLRExecContext.StackPush(arrInst);
        }

        private object Br(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            return (int)instruction.Operand;
        }

        private void Ldc_I4_7(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPush(7);
        }

        private void Ldc_I4_5(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPush(5);
        }

        private void Ldc_I4_8(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPush(8);
        }

        private void Dup(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var cpy = vCLRExecContext.EvaluationStack.Peek();
            vCLRExecContext.StackPush(cpy);
        }

        private void Ldc_I4_4(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPush(4);
        }

        private void Pop(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPop();
        }

        private void Stloc_2(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.Stloc(2);
        }

        private object Br_S(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            return (int)instruction.Operand;
        }

        private void Box(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            //Converts a value type to an object reference (type O).
            var o1 = vCLRExecContext.StackPop();
            o1 = Convert.ChangeType(o1, instruction.Operand as Type);

            vCLRExecContext.StackPush((object)o1);
        }

        private void Ldarg_0(ILInstruction instruction, VCLRExecContext vCLRExecContext, VCLRExecContext callerContext)
        {
            if (callerContext != null && callerContext.HasObjectInstance)
                vCLRExecContext.ObjectInstance = callerContext.ObjectInstance;

            //get 'this' instance context if not already done
            if (!vCLRExecContext.HasObjectInstance)
                vCLRExecContext.ObjectInstance = Activator.CreateInstance(vCLRExecContext.MethodIL.MethodInfo.DeclaringType);

            vCLRExecContext.StackPush(vCLRExecContext.ObjectInstance);
        }

        private void Ldarg_1(ILInstruction instruction, VCLRExecContext vCLRExecContext, VCLRExecContext callerContext)
        {
            var o1 = vCLRExecContext.Arguments[0];

            vCLRExecContext.StackPush(o1);
        }

        private void Ldarg_2(ILInstruction instruction, VCLRExecContext vCLRExecContext, VCLRExecContext callerContext)
        {
            var o1 = vCLRExecContext.Arguments[1];

            vCLRExecContext.StackPush(o1);
        }

        private void Ldarg_3(ILInstruction instruction, VCLRExecContext vCLRExecContext, VCLRExecContext callerContext)
        {
            var o1 = vCLRExecContext.Arguments[2];

            vCLRExecContext.StackPush(o1);
        }

        private void Add(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var i2 = (int)vCLRExecContext.StackPop();
            var i1 = (int)vCLRExecContext.StackPop();

            vCLRExecContext.StackPush(i1 + i2);
        }

        private void Ldstr(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPush(instruction.Operand);
        }

        private object Brtrue_S(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            //Transfers control to a target instruction (short form) if value is true, not null, or non-zero.
            var o1 = vCLRExecContext.StackPop();

            if (o1 != null)
            {
                if (o1 is bool && ((bool)o1)) //bool && true
                    return (int)instruction.Operand;
                if (isNumericType(o1))
                {
                    var i1 = Convert.ToInt32(o1);
                    if (i1 != 0)
                        return (int)instruction.Operand;
                }
                return (int)instruction.Operand;
            }

            return null;
        }

        private void Ceq(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            //Compares two values. If they are equal, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
            var o2 = vCLRExecContext.StackPop();
            var o1 = vCLRExecContext.StackPop();
            if (o1.Equals(o2))
                vCLRExecContext.StackPush(1);
            else
                vCLRExecContext.StackPush(0);
        }

        private void Cgt(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            //Compares two values. If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
            var i2 = (int)vCLRExecContext.StackPop();
            var i1 = (int)vCLRExecContext.StackPop();
            if (i1 > i2)
                vCLRExecContext.StackPush(1);
            else
                vCLRExecContext.StackPush(0);
        }

        private void Clt(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            //Compares two values. If the first value is less than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
            var i2 = (int)vCLRExecContext.StackPop();
            var i1 = (int)vCLRExecContext.StackPop();
            if (i1 < i2)
                vCLRExecContext.StackPush(1);
            else
                vCLRExecContext.StackPush(0);
        }

        private object Blt(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var i2 = (int)vCLRExecContext.StackPop();
            var i1 = (int)vCLRExecContext.StackPop();
            if (i1 < i2)
                return (int)instruction.Operand;

            return null;
        }

        private object Blt_S(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            //Transfers control to a target instruction (short form) if the first value is less than the second value.
            var i2 = (int)vCLRExecContext.StackPop();
            var i1 = (int)vCLRExecContext.StackPop();
            if (i1 < i2)
                return (int)instruction.Operand;

            return null;
        }

        private void Stloc_1(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.Stloc(1);
        }

        private void Stloc_0(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.Stloc(0);
        }

        private void Stloc(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.Stloc(Convert.ToInt32(instruction.Operand));
        }

        private void Ldloc_2(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.Ldloc(2);
        }

        private void Ldloc_1(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.Ldloc(1);
        }

        private void Ldloc_0(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.Ldloc(0);
        }

        private void Ldloc_S(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.Ldloc(Convert.ToInt32(instruction.Operand));
        }

        private void Ldc_I4_M1(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPush(-1);
        }

        private void Ldc_I4_S(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            int i1 = Convert.ToInt32(instruction.Operand);
            vCLRExecContext.StackPush(i1);
        }

        private void Ldc_I4_1(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPush(1);
        }

        private void Ldc_I4_0(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPush(0);
        }

        private void Ldc_I4(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPush((int)instruction.Operand);
        }

        private void Ret(ILInstruction instruction, VCLRExecContext vCLRExecContext, VCLRExecContext callerContext)
        {
            if (vCLRExecContext.EvaluationStack.Count > 0)
            {
                if (callerContext != null)
                {
                    var retVal = vCLRExecContext.StackPop();

                    callerContext.StackPush(retVal);
                }
            }
        }

        private void Newobj(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var targetCtor = (ConstructorInfo)instruction.Operand;

            var ctorParameters = targetCtor.GetParameters();

            object[] invocationParameters = null;

            if (ctorParameters.Length > 0)
            {
                invocationParameters = new object[ctorParameters.Length];

                for (int i = ctorParameters.Length - 1; i >= 0; i--)
                {
                    var targetType = ctorParameters[i];

                    var o1 = vCLRExecContext.StackPop();

                    if (targetType.ParameterType == typeof(Boolean))
                        invocationParameters[i] = Convert.ToBoolean(o1);
                    else
                        invocationParameters[i] = o1;
                }
            }

            var ctorInstance = targetCtor.Invoke(invocationParameters);

            if (ctorInstance != null)
                vCLRExecContext.StackPush(ctorInstance);
        }

        private void Stfld(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var targetField = (FieldInfo)instruction.Operand;
            var targetValue = vCLRExecContext.StackPop();
            var targetInstance = vCLRExecContext.StackPop();
            targetField.SetValue(targetInstance, targetValue);
        }

        /// <summary>
        /// Invokes a method using reflection, passing all parameters from the stack (if any)
        /// </summary>
        /// <param name="instruction">The instruction being executed</param>
        /// <param name="vCLRExecContext">The context of the executed method</param>
        private void Call(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var ownerMethod = vCLRExecContext.MethodIL.MethodInfo;
            var methodInfo = instruction.Operand as MethodInfo;
            var methodParameters = methodInfo.GetParameters();
            object[] invocationParameters = null;
            //The object on which to invoke the method or constructor. If a method is static, this argument is ignored.
            object invocationTargetInstance = null;


            if (Scope == vCLRScope.Class)
            {
                //check if the target method resides in the same class as the entry method
                if (ownerMethod.DeclaringType == methodInfo.DeclaringType)
                {
                    //go deeper

                    if (methodParameters.Length > 0)
                    {
                        invocationParameters = new object[methodParameters.Length];

                        for (int i = methodParameters.Length - 1; i >= 0; i--)
                            invocationParameters[i] = vCLRExecContext.StackPop(); //Convert.ChangeType(vCLRExecContext.StackPop(), methodParameters[i].ParameterType);

                        vCLRExecContext.Arguments = invocationParameters;
                    }

                    if (!methodInfo.IsStatic)
                    {
                        //get invocation instance target
                        invocationTargetInstance = vCLRExecContext.StackPop();
                    }

                    ExecuteILMethod(methodInfo.GetInstructions(), vCLRExecContext);
                    return;
                }
            }

            object methodReturnValue;

            if (methodParameters.Length > 0)
            {
                invocationParameters = new object[methodParameters.Length];

                for (int i = methodParameters.Length - 1; i >= 0; i--)
                    invocationParameters[i] = vCLRExecContext.StackPop(); //Convert.ChangeType(vCLRExecContext.StackPop(), methodParameters[i].ParameterType);
            }

            if (!methodInfo.IsStatic)
            {
                //get invocation instance target
                invocationTargetInstance = vCLRExecContext.StackPop();
            }

            if (invocationParameters != null)
                methodReturnValue = methodInfo.Invoke(invocationTargetInstance, invocationParameters);
            else
                methodReturnValue = methodInfo.Invoke(invocationTargetInstance, null);

            if (methodReturnValue != null)
                vCLRExecContext.StackPush(methodReturnValue);
        }

        //Calls a late-bound method on an object, pushing the return value onto the evaluation stack.
        private void Callvirt(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            Call(instruction, vCLRExecContext);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Determines if the input object is a numeric type.
        /// </summary>
        /// <param name="obj">Input object</param>
        /// <returns></returns>
        private static bool isNumericType(object obj)
        {
            switch (Type.GetTypeCode(obj.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        #endregion
    }
}