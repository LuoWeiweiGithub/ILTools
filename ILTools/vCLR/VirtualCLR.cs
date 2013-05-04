using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Animaonline.ILTools.vCLR
{
    public class VirtualCLR
    {
        /// <summary>
        /// Executes a list of IL instructions.
        /// </summary>
        /// <param name="methodILContext">Instructions to execute</param>
        /// <param name="callerEvaluationStack">Caller's evaluation stack (if any)</param>
        public void ExecuteILMethod(MethodILInfo methodILContext, Stack<object> callerEvaluationStack = null)
        {
            var vCLRExecContext = new VCLRExecContext(methodILContext);
            var position = new int();

            var offsetMappings = methodILContext.Instructions.ToDictionary(ilInstruction => ilInstruction.Offset, ilInstruction => methodILContext.Instructions.IndexOf(ilInstruction));

            //process the instructions
            while (position < methodILContext.Instructions.Count)
            {
                var instruction = methodILContext.Instructions[position++];

                object targetOffset = ExecuteInstruction(instruction, vCLRExecContext, callerEvaluationStack);

                //branch if requested
                if (targetOffset != null)
                {
                    //get the position by the given offset
                    position = offsetMappings[(int)targetOffset];
                }
            }
        }

        /// <summary>
        /// Executes the IL instruction, returns an offset if branching was requested.
        /// </summary>
        /// <param name="instruction">The instruction to execute</param>
        /// <param name="vCLRExecContext">The context of the executed method</param>
        /// <param name="callerEvaluationStack">Caller's evaluation stack (if any)</param>
        /// <returns>Returns an offset (if branching was requested)</returns>
        private object ExecuteInstruction(ILInstruction instruction, VCLRExecContext vCLRExecContext, Stack<object> callerEvaluationStack = null)
        {
            switch (instruction.OpCode)
            {
                case EnumOpCode.Nop:
                    break; //do nothing
                case EnumOpCode.Ret: //Returns from the current method, pushing a return value (if present) from the callee's evaluation stack onto the caller's evaluation stack.
                    Ret(instruction, vCLRExecContext, callerEvaluationStack);
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
                case EnumOpCode.Ldc_I4_M1:
                    Ldc_I4_M1(instruction, vCLRExecContext);
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
                    Ldarg_0(instruction, vCLRExecContext);
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
                default:
                    throw new NotImplementedException(string.Format("OpCode {0} - Not Implemented\r\nDescription: {1}", instruction.OpCodeInfo.Name, OpCodeDescriber.Describe(instruction.OpCode)));
            }

            return null;
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

        private void Ldarg_0(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            if (vCLRExecContext.MethodIL != null)
            {
                //get 'this' instance context if not already done
                if (!vCLRExecContext.HasObjectInstance)
                    vCLRExecContext.ObjectInstance = Activator.CreateInstance(vCLRExecContext.MethodIL.MethodInfo.DeclaringType);

                vCLRExecContext.StackPush(vCLRExecContext.ObjectInstance);
            }
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

        private void Ret(ILInstruction instruction, VCLRExecContext vCLRExecContext, Stack<object> callerEvaluationStack)
        {
            if (vCLRExecContext.EvaluationStack.Count > 0)
            {
                if (callerEvaluationStack != null)
                {
                    var retVal = vCLRExecContext.StackPop();

                    callerEvaluationStack.Push(retVal);
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

                for (int i = 0; i < ctorParameters.Length; i++)
                    invocationParameters[i] = vCLRExecContext.StackPop();
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
            var methodInfo = instruction.Operand as MethodInfo;

            if (methodInfo != null)
            {
                object methodReturnValue;

                var methodParameters = methodInfo.GetParameters();

                object[] invocationParameters = null;

                //The object on which to invoke the method or constructor. If a method is static, this argument is ignored.
                object invocationTargetInstance = null;

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
        }

        //Calls a late-bound method on an object, pushing the return value onto the evaluation stack.
        private void Callvirt(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var methodInfo = instruction.Operand as MethodInfo;

            if (methodInfo != null)
            {
                object methodReturnValue;

                var methodParameters = methodInfo.GetParameters();

                object[] invocationParameters = null;

                //The object on which to invoke the method or constructor. If a method is static, this argument is ignored.
                object invocationTargetInstance = null;

                if (methodParameters.Length > 0)
                {
                    invocationParameters = new object[methodParameters.Length];

                    for (int i = 0; i < methodParameters.Length; i++)
                        invocationParameters[i] = vCLRExecContext.StackPop();
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
        }

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
    }
}