using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Runtime.Infrastructures.Helper
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
        
        public static void LogError(Dictionary<object, object> dict, string parentCaller=null)
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
            Debug.LogError(info);
        }
        
        public static void LogError(object key, object value, string parentCaller=null)
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
            Debug.LogError(info);
        }

        public static void LogDictionary<T, K>(Dictionary<T, K> dict)
            where K : IEnumerable
        {
            var stackTrace = new StackTrace();
            var callerMethod = stackTrace.GetFrame(1).GetMethod();
            var callerClass = callerMethod.ReflectedType;
            var info = $"[{callerClass.Name}] {callerMethod.Name} -> ";

            foreach (var key in dict.Keys)
            {
                var values = "";
                foreach (var value in dict[key])
                {
                    values += $"{value};";
                }
                info += $"[{JsonUtility.ToJson(key)} : {values}]\n";
            }
            Debug.Log(info);
        }
    }
}