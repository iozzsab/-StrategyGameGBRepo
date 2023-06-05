using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace Core
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        public override void ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log( $"Attacking {command.Target}");
            //Debug.Log(command.Attackable != null ? $"Attacking {command.Attackable}" : "No Target To Attack");
            //todo почему не видно  "Attackable" тут
        }
    }
}