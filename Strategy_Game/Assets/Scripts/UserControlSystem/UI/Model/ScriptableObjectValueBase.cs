using System;
using System.Collections;
using System.Collections.Generic;
using Abstractions;
using Core;
using UnityEngine;
using Utils;

public abstract class ScriptableObjectValueBase<T> : ScriptableObject, IAwaitable<T>
{
    public class NewValueNotifier<TAwaited> :  AwaiterBase<TAwaited,ScriptableObjectValueBase<TAwaited>>
    {
        private TAwaited _result;

        public NewValueNotifier(ScriptableObjectValueBase<TAwaited> scriptableObjectValueBase)
        {
            ParentClass = scriptableObjectValueBase;
            ParentClass.OnNewValue += ONNewValue;
        }
        private void ONNewValue(TAwaited obj)
        {
            ParentClass.OnNewValue -= ONNewValue;
            _result = obj;
            IsCompleted = true;
            Continuation?.Invoke();
        }
        public override TAwaited GetResult() => _result;
        
    }
    // public class NewValueNotifier<TAwaited> : IAwaiter<TAwaited>
    // {
    //     private readonly ScriptableObjectValueBase<TAwaited> _scriptableObjectValueBase;
    //     private TAwaited _result;
    //     private Action _continuation;
    //     private bool _isCompleted;
    //
    //     public NewValueNotifier(ScriptableObjectValueBase<TAwaited> scriptableObjectValueBase)
    //     {
    //         _scriptableObjectValueBase = scriptableObjectValueBase;
    //         _scriptableObjectValueBase.OnNewValue += ONNewValue;
    //     }
    //
    //     private void ONNewValue(TAwaited obj)
    //     {
    //         _scriptableObjectValueBase.OnNewValue -= ONNewValue;
    //         _result = obj;
    //         _isCompleted = true;
    //         _continuation?.Invoke();
    //     }
    //
    //     public void OnCompleted(Action continuation)
    //     {
    //         if (_isCompleted)
    //         {
    //             continuation?.Invoke();
    //         }
    //         else
    //         {
    //             _continuation = continuation;
    //         }
    //     }
    //     public bool IsCompleted => _isCompleted;
    //     public TAwaited GetResult() => _result;
    // }
    public T CurrentValue { get; private set; }
    public Action<T> OnNewValue;

    public void SetValue(T value)
    {
        CurrentValue = value;
        OnNewValue?.Invoke(value);
    }

    public IAwaiter<T> GetAwaiter()
    {
        return new NewValueNotifier<T>(this);
    }
}
