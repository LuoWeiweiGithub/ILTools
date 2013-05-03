using System.Reflection;
using Animaonline.ILTools;

//// ReSharper disable CheckNamespace
namespace System
//// ReSharper restore CheckNamespace
{
    public static class MethodInfoExtensions
    {
        public static MethodILInfo GetInstructions(this MethodInfo methodInfo)
        {
            return ILTools.GetMethodIL(methodInfo);
        }
    }
}
