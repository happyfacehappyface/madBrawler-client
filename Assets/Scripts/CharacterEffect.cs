using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterEffect
{
    private int _id;
    private Team _team;
    private TimeSpan _startTime;
    private TimeSpan _duration;
    private int _stack;

    private CharacterEffectType _effectType;
    public CharacterEffectType EffectType => _effectType;

    private CharacterEffectCategory _effectCategory;
    public CharacterEffectCategory EffectCategory => _effectCategory;

    private string _effectName;
    public string EffectName => _effectName;
    private Sprite _effectIcon;
    public Sprite EffectIcon => _effectIcon;

    public CharacterEffect(int id, TimeSpan duration, CharacterEffectType effectType, CharacterEffectCategory effectCategory, string effectName, Sprite effectIcon)
    {
        _id = id;
        _duration = duration;
        _effectType = effectType;
        _effectCategory = effectCategory;
        _effectName = effectName;
        _effectIcon = effectIcon;
        _stack = 1;
    }

    public void ManualStart(Team team)
    {
        _team = team;
        _startTime = GameController.Instance.GetPlayerTime(_team);
    }

    public TimeSpan GetLeftTime()
    {
        return _duration - (GameController.Instance.GetPlayerTime(_team) - _startTime);
    }

    public bool IsEnded()
    {
        return GetLeftTime() <= TimeSpan.Zero;
    }

    public bool TryAddStack(CharacterEffect other)
    {
        if (_id == other._id)
        {
            _stack += other._stack;
            if (GetLeftTime() < other.GetLeftTime())
            {
                _duration = GameController.Instance.GetPlayerTime(_team) + other.GetLeftTime();
            }
            return true;
        }
        return false;
    }



}



public abstract record CharacterEffectCategory
{
    public sealed record Nothing() : CharacterEffectCategory;
    public sealed record MoveSpeed(float Rate) : CharacterEffectCategory;
    public sealed record AttackPower(float Rate) : CharacterEffectCategory;
    public sealed record Stun() : CharacterEffectCategory;
    public sealed record Bond() : CharacterEffectCategory;
    public sealed record Silence() : CharacterEffectCategory;
}

public enum CharacterEffectType
{
    Buff,
    Debuff,
}
