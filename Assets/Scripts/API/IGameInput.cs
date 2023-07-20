using System;
using UnityEngine;

namespace API
{
    public interface IGameInput : PlayerInputAction.IInteractActions
    {
        public Vector2 MousePosition { get; }
        event Action OnClick;
    }
}