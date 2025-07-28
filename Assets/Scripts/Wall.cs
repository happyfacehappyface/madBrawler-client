using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wall : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private WallInside _wallInside;

    private WallState _state;

    private const float _activeTime = 5.0f;

    public void ManualStart()
    {
        Activate();
        _wallInside.ManualStart(this);
    }

    public void ManualUpdate()
    {
        if (_state is WallState.Deactive deactive)
        {
            if (deactive.EndTime < GameController.Instance.GetCurrentTime())
            {
                Activate();
            }
        }
    }


    public void Activate()
    {
        _state = new WallState.Active();
        _animator.SetBool("isActive", true);
    }

    public void Deactivate()
    {
        _state = new WallState.Deactive(GameController.Instance.GetCurrentTime() + TimeSpan.FromSeconds(_activeTime));
        _animator.SetBool("isActive", false);
    }

    private abstract record WallState
    {
        public sealed record Active() : WallState;
        public sealed record Deactive(TimeSpan EndTime) : WallState;
    }
}
