﻿using System;
using UnityEngine;
using Utils;

namespace UserControlSystem.UI.Model
{
    public abstract class ScriptableObjectValueBase<T> : ScriptableObject, IAwaitable<T>
    {
        public class NewValueNotifier <TAwaited> : AwaiterBase<TAwaited>
        {
            private readonly ScriptableObjectValueBase<TAwaited> _scriptableObjectValueBase;


            public NewValueNotifier(ScriptableObjectValueBase<TAwaited> scriptableObjectValueBase)
            {
                _scriptableObjectValueBase = scriptableObjectValueBase;
                _scriptableObjectValueBase.OnNewValue += OnNewValue;
            }

            private void OnNewValue(TAwaited obj)
            {
                _scriptableObjectValueBase.OnNewValue -= OnNewValue;
                OnWaitFinish(obj);
            }
        }

        public T CurrentValue { get; private set; }
        public Action<T> OnNewValue;


        public virtual void SetValue(T value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }


        public IAwaiter<T> GetAwaiter()
        {
            return new NewValueNotifier<T>(this);
        }
    }
}