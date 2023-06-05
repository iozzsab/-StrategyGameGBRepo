using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows.WebCam;
using Utils;

namespace Core
{
    public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        public class StopAwaiter : AwaiterBase<AsyncExtensions.Void, UnitMovementStop>
        {
            public StopAwaiter(UnitMovementStop unitMovementStop)
            {
                ParentClass = unitMovementStop;
                ParentClass.OnStop += ONStop;
            }

            public override AsyncExtensions.Void GetResult() => new AsyncExtensions.Void();

            private void ONStop()
            {
                ParentClass.OnStop -= ONStop;
                IsCompleted = true;
                Continuation?.Invoke();
            }
        }

        public event Action OnStop;

        [SerializeField] private NavMeshAgent _agent;

        private void Update()
        {
            if (!_agent.pathPending)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                    {
                        OnStop?.Invoke();
                    }
                }
            }
        }

        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new StopAwaiter(this);
    }
}