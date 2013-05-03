using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Animaonline.ILTools.vCLR
{
    public class VirtualCLR
    {
        #region Public Constructor

        public VirtualCLR()
        {
            OpCodeActions = new Func<ILInstruction, VCLRExecContext, object>[256];
            OpCodeActions[OpCodes.Newobj.Value] = Newobj;
            OpCodeActions[OpCodes.Nop.Value] = Nop;
            OpCodeActions[OpCodes.Ret.Value] = Ret;
            OpCodeActions[OpCodes.Stloc_0.Value] = Stloc_0;
            OpCodeActions[OpCodes.Stloc_1.Value] = Stloc_1;
            OpCodeActions[OpCodes.Stloc_2.Value] = Stloc_2;
            OpCodeActions[OpCodes.Ldloc_0.Value] = Ldloc_0;
            OpCodeActions[OpCodes.Ldloc_1.Value] = Ldloc_1;
            OpCodeActions[OpCodes.Ldloc_2.Value] = Ldloc_2;
            OpCodeActions[OpCodes.Callvirt.Value] = Callvirt;
            OpCodeActions[OpCodes.Ldc_I4_0.Value] = Ldc_I4_0;
            OpCodeActions[OpCodes.Ldc_I4_1.Value] = Ldc_I4_1;
            OpCodeActions[OpCodes.Ldc_I4.Value] = Ldc_I4;
            OpCodeActions[OpCodes.Br_S.Value] = Br_S;
            OpCodeActions[OpCodes.Blt_S.Value] = Blt_S;
            OpCodeActions[OpCodes.Add.Value] = Add;
            OpCodeActions[OpCodes.Ldstr.Value] = Ldstr;
            OpCodeActions[OpCodes.Box.Value] = Box;
            OpCodeActions[OpCodes.Call.Value] = Call;
            OpCodeActions[OpCodes.Ldarg_0.Value] = Ldarg_0;
        }

        #endregion

        #region Public Methods

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

                var targetOpCodeAction = OpCodeActions[instruction.OpCodeInfo.Value];

                if (targetOpCodeAction == null)
                    throw new NotImplementedException(string.Format("OpCode {0} - Not Implemented\r\nDescription: {1}", instruction.OpCodeInfo.Name, OpCodeDescriber.Describe(instruction.OpCode)));

                var targetOffset = targetOpCodeAction(instruction, vCLRExecContext);

                //branch if requested
                if (targetOffset != null)
                {
                    //get the position by the given offset
                    position = offsetMappings[(int)targetOffset];
                }
            }
        }

        #endregion

        #region Private Properties

        private Func<ILInstruction, VCLRExecContext, object>[] OpCodeActions { get; set; }

        #endregion

        #region OpCode Implementations

        private object Ldarg_0(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            if (vCLRExecContext.MethodIL != null)
            {
                //get 'this' instance context if not already done
                if (!vCLRExecContext.HasObjectInstance)
                    vCLRExecContext.ObjectInstance = Activator.CreateInstance(vCLRExecContext.MethodIL.MethodInfo.DeclaringType);

                vCLRExecContext.StackPush(vCLRExecContext.ObjectInstance);
            }

            return null;
        }

        private object Box(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            object o1 = vCLRExecContext.StackPop();
            o1 = Convert.ChangeType(o1, instruction.Operand as Type);
            vCLRExecContext.StackPush((object)o1);

            return null;
        }

        private object Ldstr(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPush(instruction.Operand);
            return null;
        }

        private object Add(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            int i2 = (int)vCLRExecContext.StackPop();
            int i1 = (int)vCLRExecContext.StackPop();

            vCLRExecContext.StackPush(i1 + i2);

            return null;
        }

        private object Blt_S(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            //Transfers control to a target instruction (short form) if the first value is less than the second value.
            int i2 = (int)vCLRExecContext.StackPop();
            int i1 = (int)vCLRExecContext.StackPop();
            if (i1 < i2)
                return (int)instruction.Operand;

            return null;
        }

        private object Br_S(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            return (int)instruction.Operand;
        }

        private object Ldc_I4(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPush(instruction.Operand);
            return null;
        }

        private object Ldc_I4_0(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPush(0);
            return null;
        }

        private object Ldc_I4_1(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.StackPush(1);
            return null;
        }

        private object Ldloc_0(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.Ldloc(0);
            return null;
        }

        private object Ldloc_1(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.Ldloc(1);
            return null;
        }

        private object Ldloc_2(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.Ldloc(2);
            return null;
        }

        private object Stloc_0(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.Stloc(0);
            return null;
        }

        private object Stloc_1(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.Stloc(1);
            return null;
        }

        private object Stloc_2(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            vCLRExecContext.Stloc(2);
            return null;
        }

        private object Ret(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            if (vCLRExecContext.EvaluationStack.Count > 0)
            {
                return null; //TODO
                //if (callerEvaluationStack != null)
                //{
                //    var retVal = vCLRExecContext.StackPop();

                //    callerEvaluationStack.Push(retVal);
                //}
            }

            return null;
        }

        private object Nop(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            return null;
        }

        private object Newobj(ILInstruction instruction, VCLRExecContext vCLRExecContext)
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

            return null;
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
        private object Call(ILInstruction instruction, VCLRExecContext vCLRExecContext)
        {
            var methodInfo = instruction.Operand as MethodInfo;

            if (methodInfo != null)
            {
                object methodReturnValue;

                var methodParameters = methodInfo.GetParameters();

                object[] invocationParameters = null;

                //The object on which to invoke the method or constructor. If a method is static, this argument is ignored.
                object invocationTargetInstance = null;

                if (!methodInfo.IsStatic)
                {
                    //get invocation instance target
                    invocationTargetInstance = vCLRExecContext.StackPop();
                }

                if (methodParameters.Length > 0)
                {
                    invocationParameters = new object[methodParameters.Length];

                    for (int i = methodParameters.Length - 1; i >= 0; i--)
                        invocationParameters[i] = vCLRExecContext.StackPop(); //Convert.ChangeType(vCLRExecContext.StackPop(), methodParameters[i].ParameterType);
                }

                if (invocationParameters != null)
                    methodReturnValue = methodInfo.Invoke(invocationTargetInstance, invocationParameters);
                else
                    methodReturnValue = methodInfo.Invoke(invocationTargetInstance, null);

                if (methodReturnValue != null)
                    vCLRExecContext.StackPush(methodReturnValue);
            }

            return null;
        }

        //Calls a late-bound method on an object, pushing the return value onto the evaluation stack.
        private object Callvirt(ILInstruction instruction, VCLRExecContext vCLRExecContext)
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

            return null;
        }

        #endregion

        #region Private Methods

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
