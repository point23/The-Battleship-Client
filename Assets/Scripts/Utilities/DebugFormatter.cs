using System;

namespace Utilities
{
    public class DebugFormatter
    {
        private readonly string _className;
        
        public DebugFormatter(Type type)
        {
            _className = $"[{type}]";
        }
        
        public DebugFormatter(string className)
        {
            _className = $"[{className}]";
        }

        public string Format(string methodName, string info)
        {
            return _className + $" {methodName} -> {info}";
        }
        
    }
}