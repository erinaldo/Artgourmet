using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Artebit.Restaurante.Caixa.Classes
{
    public class CustomKeyboardCommandProvider : DefaultKeyboardCommandProvider
    {
        public CustomKeyboardCommandProvider(GridViewDataControl grid)
            : base(grid)
        {
        }

        public override IEnumerable<ICommand> ProvideCommandsForKey(Key key)
        {
            List<ICommand> commandsToExecute = base.ProvideCommandsForKey(key).ToList();
            if (key == Key.Enter)
            {
                commandsToExecute.Clear();
                commandsToExecute.Add(RadGridViewCommands.CommitEdit);
                //commandsToExecute.Add(RadGridViewCommands.SelectCurrentItem);
                commandsToExecute.Add(RadGridViewCommands.BeginEdit);
            }
            else
            {
                if (key == Key.Tab)
                {
                    //commandsToExecute.Clear();
                    //commandsToExecute.Add(RadGridViewCommands.CommitEdit);
                    //commandsToExecute.Add(RadGridViewCommands.mo
                    commandsToExecute.Add(RadGridViewCommands.BeginEdit);
                }

                if (key == Key.Down)
                {
                    commandsToExecute.Clear();
                    commandsToExecute.Add(RadGridViewCommands.CommitEdit);
                    commandsToExecute.Add(RadGridViewCommands.MoveNext);
                    commandsToExecute.Add(RadGridViewCommands.BeginEdit);
                }

                if (key == Key.Up)
                {
                    commandsToExecute.Clear();
                    commandsToExecute.Add(RadGridViewCommands.CommitEdit);
                    commandsToExecute.Add(RadGridViewCommands.MovePrevious);
                    commandsToExecute.Add(RadGridViewCommands.BeginEdit);
                }
            }

            return commandsToExecute;
        }
    }
}