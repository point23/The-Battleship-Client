using System.Collections;
using System.Collections.Generic;
using Runtime.Common.Interface;
using Runtime.Infrastructures.Helper;
using ThirdParty.SimpleJSON;
using UnityEditor.Experimental.GraphView;

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
        
        public bool ContainsResponder(string name)
        {
            return _responders.ContainsKey(name);
        }
        
        public async void RunCommands(CommandList commandList)
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
                {"data", command.data}
            });
            
            await _responders[command.responder].Run(command.action, command.data);
        }
    }

    public readonly struct Command
    {
        public readonly string responder;
        public readonly string action;
        public readonly JSONNode data;
        
        public Command(JSONNode json)
        {
            responder = json["responder"].Value;
            action = json["action"].Value;
            data = json["data"];
        }

        public Command(string responder, string action, JSONNode data)
        {
            this.responder = responder;
            this.action = action;
            this.data = data;
        }
    }

    public struct CommandList : IEnumerable<Command>
    {
        private List<Command> _list;

        public CommandList(JSONArray array)
        {
            _list = new List<Command>();
            foreach (var json in array.Children)
            {
                _list.Add(new Command(json));
            }
        }

        public CommandList(Command command)
        {
            _list = new List<Command> { command };
        }

        public void Add(Command command)
        {
            _list.Add(command);
        }

        public IEnumerator<Command> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}