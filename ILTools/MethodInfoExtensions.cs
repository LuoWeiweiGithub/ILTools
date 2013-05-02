using System.Collections.Generic;
using System.Reflection;
using Animaonline.ILTools;

//// ReSharper disable CheckNamespace
namespace System
//// ReSharper restore CheckNamespace
{
    public static class MethodInfoExtensions
    {
        public static IEnumerable<ILInstruction> GetInstructions(this MethodInfo methodInfo)
        {
            return ILTools.GetMethodIL(methodInfo);
        }
    }
}
