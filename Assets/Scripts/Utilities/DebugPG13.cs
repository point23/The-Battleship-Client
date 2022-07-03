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
            var callerMethod = stackTrace.GetFrame(1).GetMethod();
            var callerClass = callerMethod.ReflectedType;
            var info = $"[{callerClass.Name}] {callerMethod.Name} -> ";
            foreach (var kvPair in dict)
            {
                info += $" {kvPair.Key} : {kvPair.Value}; ";
            }
            Debug.Log(info);
        }
        
        public static void Log(object key, object value)
        {
            var stackTrace = new StackTrace();
            var callerMethod = stackTrace.GetFrame(1).GetMethod();
            var callerClass = callerMethod.ReflectedType;
            var info = $"[{callerClass.Name}] {callerMethod.Name} -> ";
            info += $" {key} : {value}; ";
            Debug.Log(info);
        }
    }
}