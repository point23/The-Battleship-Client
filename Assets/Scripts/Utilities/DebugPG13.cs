using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Debug = UnityEngine.Debug;

namespace Utilities
{
    public static class DebugPG13
    {
        public static void Log(Dictionary<object, object> dict)
        {
            var stackTrace = new StackTrace();
            var callerMethod = stackTrace.GetFrame(1).GetMethod().Name;
            var callerClass = stackTrace.GetFrame(1).GetType().Name;
            var info = $"[{callerClass}] callerMethod -> ";
            foreach (var kvPair in dict)
            {
                info += $" {kvPair.Key} : {kvPair.Value}; ";
            }
            Debug.Log(info);
        }
    }
}