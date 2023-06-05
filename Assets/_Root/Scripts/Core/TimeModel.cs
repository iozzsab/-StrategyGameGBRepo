﻿using Abstractions;
using System;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core
{
    public class TimeModel : ITimeModel, ITickable
    {
        private ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();
        public IObservable<int> GameTime => _gameTime.Select(f => (int)f);


        public void Tick() =>
            _gameTime.Value += Time.deltaTime;
    }
}