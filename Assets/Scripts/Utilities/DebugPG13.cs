using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Debug = UnityEngine.Debug;

namespace Utilities
{
    public static class DebugPG13
    {

        public static void Log(Dictionary<object, object> dict, string parentCaller=null)
        {
            var stackTrace = new StackTrace();
            if (parentCaller != null)
            {
                var parentCallerMethod = stackTrace.GetFrame(2).GetMethod();
                if (parentCallerMethod.Name != parentCaller)
                    return;
            }
            
            var callerMethod = stackTrace.GetFrame(1).GetMethod();
            var callerClass = callerMethod.ReflectedType;
            var info = $"[{callerClass.Name}] {callerMethod.Name} -> ";
            foreach (var kvPair in dict)
            {
                info += $" {kvPair.Key} : {kvPair.Value}; ";
            }
            Debug.Log(info);
        }
        
        public static void Log(object key, object value, string parentCaller=null)
        {
            var stackTrace = new StackTrace();
            if (parentCaller != null)
            {
                var parentCallerMethod = stackTrace.GetFrame(2).GetMethod();
                if (parentCallerMethod.Name != parentCaller)
                    return;
            }
            
            var callerMethod = stackTrace.GetFrame(1).GetMethod();
            var callerClass = callerMethod.ReflectedType;
            var info = $"[{callerClass.Name}] {callerMethod.Name} -> ";
            info += $" {key} : {value}; ";
            Debug.Log(info);
        }
    }
}