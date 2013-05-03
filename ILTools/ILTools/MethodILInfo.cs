using System.Collections.Generic; 
using System.Reflection;

namespace Animaonline.ILTools
{
    public class MethodILInfo
    {
        #region Public Constructor

        public MethodILInfo(MethodInfo methodInfo)
        {
            this.MethodInfo = methodInfo;
            this.Instructions = new List<ILInstruction>();
        }

        #endregion

        #region Public Properties

        public MethodInfo MethodInfo { get; set; }

        public List<ILInstruction> Instructions { get; set; }

        #endregion 
    }
}
