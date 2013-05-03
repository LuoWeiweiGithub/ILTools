using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Animaonline.ILTools
{
    public class MethodILInfo
    {
        public MethodILInfo(MethodInfo methodInfo)
        {
            this.MethodInfo = methodInfo;
            this.Instructions = new List<ILInstruction>();
        }

        public MethodInfo MethodInfo { get; set; }
        public List<ILInstruction> Instructions { get; set; }
    }
}
