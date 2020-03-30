using System;
using System.Collections.Generic;
using System.IO;

namespace KizhiPart1
{
    public class Interpreter
    {
        private const string _VariableNotFoundError = "Переменная отсутствует в памяти";
        private Dictionary<string, int> _variables = new Dictionary<string, int>();
        private TextWriter _writer;

        public Interpreter(TextWriter writer)
        {
            _writer = writer;
        }
        
        private void CreateVariable(string name, int value)
        {
            _variables[name] = value;
        }

        private void Substraction(string name, int value)
        {
            if (_variables.ContainsKey(name))
            {
                _variables[name] -= value;
            }
            else
            {
                _writer.WriteLine(_VariableNotFoundError);
            }
        }

        private void Print(string name)
        {
            if (_variables.TryGetValue(name, out int value))
            {
                _writer.WriteLine(value);
            }
            else
            {
                _writer.WriteLine(_VariableNotFoundError);
            }
        }

        private void Remove(string name)
        {
            if (!_variables.Remove(name))
            {
                _writer.WriteLine(_VariableNotFoundError);
            }
        }

        public void ExecuteLine(string command)
        {
            var splittedCommand = command.Split();

            var commandName = splittedCommand[0];
            var variableName = splittedCommand[1];

            switch (commandName)
            {
                case "set":
                    CreateVariable(variableName, Convert.ToInt32(splittedCommand[2]));
                    break;

                case "sub":
                    Substraction(variableName, Convert.ToInt32(splittedCommand[2]));
                    break;

                case "print":
                    Print(variableName);
                    break;

                case "rem":
                    Remove(variableName);
                    break;
            }
        }
    }
}
