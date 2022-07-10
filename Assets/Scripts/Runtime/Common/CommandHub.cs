using System.Collections.Generic;
using Runtime.Common.Interface;
using Runtime.Infrastructures.Helper;
using Runtime.Infrastructures.JSON;
using UnityEngine;

namespace Runtime.Common
{
    public class CommandHub
    {
        private readonly Dictionary<string, ICommandResponder> _responders;

        public CommandHub()
        {
            _responders = new Dictionary<string, ICommandResponder>();
        }

        public void Register(string name, ICommandResponder responder)
        {
            _responders[name] = responder;
        }
        
        public async void RunCommands(List<Command> commandList)
        {
            foreach (var command in commandList)
            {
                RunCommand(command);
            }
        }

        private async void RunCommand(Command command)
        {
            DebugPG13.Log(new Dictionary<object, object>
            {
                {"responder", command.responder},
                {"action", command.action},
                {"data", command.Data}
            });
            
            await _responders[command.responder].Run(command.action, command.Data);
        }
    }

    public readonly struct Command
    {
        public readonly string responder;
        public readonly string action;
        private readonly string _data;
        public JsonData Data => new(_data);
        
        public Command(JsonData jsonData)
        {
            responder = jsonData["responder"].Value;
            action = jsonData["action"].Value;
            _data = jsonData["data"].Value;
        }
    }
}