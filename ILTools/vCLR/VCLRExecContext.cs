using System.Collections.Generic;

namespace Animaonline.ILTools.vCLR
{
    public class VCLRExecContext
    {
        #region Public Constructor

        public VCLRExecContext(MethodILInfo methodILInfo, int localsSize = 1024)
        {
            this.MethodIL = methodILInfo;
            this.EvaluationStack = new Stack<object>();
            this.MethodLocals = new object[localsSize];
        }

        #endregion

        #region Public Properties

        public MethodILInfo MethodIL { get; set; }
        public object[] Arguments { get; set; }
        public object ObjectInstance;
        public Stack<object> EvaluationStack { get; set; }
        public object[] MethodLocals { get; set; }

        public bool HasArguments
        {
            get
            {
                return Arguments != null;
            }
        }

        public bool HasObjectInstance
        {
            get
            {
                return ObjectInstance != null;
            }
        }

        #endregion

        #region Public Methods

        public void StackPush(object obj)
        {
            EvaluationStack.Push(obj);
        }

        public void StackPushRef(ref object obj)
        {
            EvaluationStack.Push(obj);
        }

        public object StackPop()
        {
            return EvaluationStack.Pop();
        }

        /// <summary>
        /// Pushes the local at the given index onto the evaluation stack
        /// </summary>
        /// <param name="index"></param>
        public void Ldloc(int index)
        {
            StackPush(MethodLocals[index]);
        }

        public void Stloc(int index)
        {
            //pop the stack and set the local at the given index
            MethodLocals[index] = StackPop();
        }

        #endregion
    }
}