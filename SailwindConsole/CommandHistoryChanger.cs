using SailwindConsole.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace SailwindConsole
{
    internal class CommandHistoryChanger : MonoBehaviour
    {
        InputField inputField;
        string shortCommand;
        int commandIndex = -1;

        void Start()
        {
            inputField = GetComponent<InputField>();
        }

        void Update()
        {
            if (!inputField.isFocused) return;
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ModConsole.previousCommandIndex++;
                if (ModConsole.previousCommandIndex >= ModConsole.previousCommands.Count)
                {
                    inputField.text = "";
                    ModConsole.previousCommandIndex = ModConsole.previousCommands.Count;
                }
                else
                {
                    inputField.text = ModConsole.previousCommands[ModConsole.previousCommandIndex];
                    inputField.caretPosition = inputField.text.Length;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ModConsole.previousCommandIndex--;
                if (ModConsole.previousCommandIndex < 0)
                {
                    ModConsole.previousCommandIndex = 0;
                }
                inputField.text = ModConsole.previousCommands[ModConsole.previousCommandIndex];
                inputField.caretPosition = inputField.text.Length;
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (string.IsNullOrEmpty(shortCommand) || inputField.text.ToLower().Substring(0, Mathf.Min(shortCommand.Length, inputField.text.Length)) != shortCommand.ToLower())
                {
                    shortCommand = inputField.text.ToLower();
                    commandIndex = -1;
                }

                if (!string.IsNullOrEmpty(shortCommand))
                {
                    List<Command> commands = ModConsole.commands.FindAll(c => c.Name.ToLower().StartsWith(shortCommand.ToLower()));
                    if (commands.Count > 0)
                    {
                        commandIndex++;
                        if (commandIndex >= commands.Count)
                            commandIndex = 0;

                        inputField.text = commands[commandIndex].Name;
                    }
                }
            }
        }
    }
}