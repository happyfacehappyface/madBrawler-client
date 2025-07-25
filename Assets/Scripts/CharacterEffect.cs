using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterEffect
{
    private Team _team;
    private TimeSpan _startTime;
    private TimeSpan _duration;

    private CharacterEffectType _effectType;
    public CharacterEffectType EffectType => _effectType;

    private CharacterEffectCategory _effectCategory;
    public CharacterEffectCategory EffectCategory => _effectCategory;

    private string _effectName;
    public string EffectName => _effectName;
    private Sprite _effectIcon;
    public Sprite EffectIcon => _effectIcon;

    public CharacterEffect(Team team, TimeSpan duration, CharacterEffectType effectType, CharacterEffectCategory effectCategory, string effectName, Sprite effectIcon)
    {
        _team = team;
        _duration = duration;
        _effectType = effectType;
        _effectCategory = effectCategory;
        _effectName = effectName;
        _effectIcon = effectIcon;
    }

    public void ManualStart()
    {
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



}



public abstract record CharacterEffectCategory
{
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
