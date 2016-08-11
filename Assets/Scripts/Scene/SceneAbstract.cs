﻿using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Scene
{
    public abstract class SceneAbstract
    {
        public virtual void UpdatePre() { }
        public virtual void UpDate() { }
        public virtual void FixedUpdate() { }
        public virtual void UpdatePost() { }
        public void Enter()
        {
            _OnEnter();
        }
        public void Exit()
        {
            _OnExit();
        }
        protected virtual void _OnEnter() { }
        protected virtual void _OnExit() { }
        public abstract SceneType sceneType { get; }
    }
}
