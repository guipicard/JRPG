using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    [Serializable, Flags]
    public enum Target
    {
        Enemy = 1 << 0,
        Party = 1 << 1,
        Self = 1 << 2,
    }

    [Serializable, Flags]
    public enum PowerType
    {
        Physical = 1 << 0,
        Magical = 1 << 1,
    }

    [Serializable]
    public struct OvertimeSetting
    {
        [Serializable, Flags]
        public enum OvertimeType
        {
            Regen = 1 << 0,
            Bleeding = 1 << 1,
            Poisoned = 1 << 2,
            Burning = 1 << 3,
            Cursed = 1 << 4,
        }

        public OvertimeType type;
        [Range(0, 100)] public int probability;
        public int nbTurn;
        public float damage;
    }

    [Serializable]
    public struct Effect
    {
        [Serializable, Flags]
        public enum EffectType
        {
            Damage = 1 << 0,
            Healing  = 1 << 1,
            Dot = 1 << 2,
        }

        public EffectType effectType;
        public OvertimeSetting dotSetting;
    }

    [Serializable, Flags]
    public enum SkillType
    {
        Normal = 1 << 0,
        Fire = 1 << 1,
        Ice = 1 << 2,
    }

    public SkillType skillType;
    public int manaCost;
    public Target target;
    public PowerType powerType;
    public Effect effect;

    public GameObject vfx;
    public GameObject dotVFX;
    public AudioClip sfx;
}
