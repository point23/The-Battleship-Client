using System.Collections.Generic;
using Runtime.Infrastructures.JSON;
using UnityEngine;

namespace Runtime.Common
{
    public class CommandHub : MonoBehaviour
    {
        public Dictionary<string, ICommandResponder> responders;

        public void Awake()
        {
            responders = new Dictionary<string, ICommandResponder>();
        }

        public void Register(string name, ICommandResponder responder)
        {
            responders[name] = responder;
        }
        
        public void RunCommands(List<Command> commandList)
        {
            foreach (var command in commandList)
            {
                RunCommand(command);
            }
        }
        
        public void RunCommand(Command command)
        {
            responders[command.responder].Run(command.data);
        }
    }

    public struct Command
    {
        public string responder;
        public JsonData data;
    }
}