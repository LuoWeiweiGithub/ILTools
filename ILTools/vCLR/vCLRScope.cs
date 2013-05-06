using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Animaonline.ILTools.vCLR
{
    public enum vCLRScope
    {
        /// <summary>
        /// Execution is limited to the input method
        /// </summary>
        Method,
        /// <summary>
        /// Execution is limited to all methods in the input method's declaring type.
        /// </summary>
        Class,
        /// <summary>
        /// Executes all methods virtually
        /// </summary>
        Global
    }
}
