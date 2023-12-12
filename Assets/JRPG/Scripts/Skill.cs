using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        Magical = 1 << 0,
        Physical = 1 << 1,
        Divine = 1 << 2,
        Natural = 1 << 3,
    }

    [Serializable, Flags]
    public enum SkillElement
    {
        Normal = 1 << 0,
        Fire = 1 << 1,
        Ice = 1 << 2,
        Nature = 1 << 3,
        Lightning = 1 << 4,
        Light = 1 << 5,
        Shadow  = 1 << 6,
    }

    [Serializable]
    public struct OverTimeSetting
    {
        [Range(0,100)]public int probability;
        public int nbTurn;
        public float damage;
    }


    [Serializable]
    public struct Effect
    {
        [Serializable, Flags]
        public enum EffectType
        {
            DamageDirect =  1 << 0,
            HealDirect = 1 << 1,
            DamageOvertime = 1 << 2,
            HealOvertime = 1 << 3,
        }

        [Serializable, Flags]
        public enum DamageType
        {
            Regen = 1 << 0,
            Bleeding = 1 << 1,
            Poisined = 1 << 2,
            Burning = 1 << 3,
            Cursed = 1 << 4,
            Freezing = 1 << 5,
        }

        public DamageType damageType;
        public EffectType effectType;
        public OverTimeSetting overTimeSetting;
    }

    public int damage;
    public int manaCost;
    public int manaGeneration;
    public int energyGenerated;
    public int energyCost;
    public int rageCost;
    public int rageGeneration;
    public int cooldown;

    public List<Effect> effects;
    public Target target;
    public PowerType powerType;
    public SkillElement element;

    public GameObject vfx;
    public GameObject overTimeVfx;
    public AudioClip sfx;
}
