using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterEffectFactory
{

    public CharacterEffectFactory()
    {
        
    }

    public CharacterEffect SungjunSkill0BuffMoveSpeed(TimeSpan duration, float factor)
    {
        return new CharacterEffect(1101, duration, CharacterEffectType.Buff, new CharacterEffectCategory.MoveSpeed(factor), "MoveSpeed", null);
    }

    public CharacterEffect SungjunSkill1DebuffMoveSpeed(TimeSpan duration, float factor)
    {
        return new CharacterEffect(1201, duration, CharacterEffectType.Debuff, new CharacterEffectCategory.MoveSpeed(factor), "MoveSpeed", null);
    }

    public CharacterEffect SungjunSkill2DebuffMoveSpeed(TimeSpan duration, float factor)
    {
        return new CharacterEffect(1301, duration, CharacterEffectType.Debuff, new CharacterEffectCategory.MoveSpeed(factor), "MoveSpeed", null);
    }

    public CharacterEffect SinniSkill0DebuffSilence(TimeSpan duration)
    {
        return new CharacterEffect(2101, duration, CharacterEffectType.Debuff, new CharacterEffectCategory.Silence(), "Silence", null);
    }

    public CharacterEffect SinniSkill0DebuffStack(TimeSpan duration)
    {
        return new CharacterEffect(2102, duration, CharacterEffectType.Debuff, new CharacterEffectCategory.Nothing(), "Nothing", null);
    }

    public CharacterEffect SinniSkill0DebuffBond(TimeSpan duration)
    {
        return new CharacterEffect(2103, duration, CharacterEffectType.Debuff, new CharacterEffectCategory.Bond(), "Bond", null);
    }

    public CharacterEffect SinniSkill1BuffMoveSpeed(TimeSpan duration, float factor)
    {
        return new CharacterEffect(2201, duration, CharacterEffectType.Buff, new CharacterEffectCategory.MoveSpeed(factor), "MoveSpeed", null);
    }

    public CharacterEffect GwangHoSkill1DebuffStun(TimeSpan duration)
    {
        return new CharacterEffect(3101, duration, CharacterEffectType.Debuff, new CharacterEffectCategory.Stun(), "Stun", null);
    }

    public CharacterEffect SeowooSkill0DebuffStun(TimeSpan duration)
    {
        return new CharacterEffect(4101, duration, CharacterEffectType.Debuff, new CharacterEffectCategory.Stun(), "Stun", null);
    }

    public CharacterEffect SeowooSkill1BuffMoveSpeed(TimeSpan duration, float factor)
    {
        return new CharacterEffect(4201, duration, CharacterEffectType.Buff, new CharacterEffectCategory.MoveSpeed(factor), "MoveSpeed", null);
    }

    public CharacterEffect SeowooSkill2DebuffStun(TimeSpan duration)
    {
        return new CharacterEffect(4301, duration, CharacterEffectType.Debuff, new CharacterEffectCategory.Stun(), "Stun", null);
    }

    public CharacterEffect JaehyeonSkill0BuffMoveSpeed(TimeSpan duration, float factor)
    {
        return new CharacterEffect(5101, duration, CharacterEffectType.Buff, new CharacterEffectCategory.MoveSpeed(factor), "MoveSpeed", null);
    }

    public CharacterEffect JaehyeonSkill2DebuffBond(TimeSpan duration)
    {
        return new CharacterEffect(5301, duration, CharacterEffectType.Debuff, new CharacterEffectCategory.Bond(), "Bond", null);
    }


}
