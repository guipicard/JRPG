using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu]
public class CharacterClass : ScriptableObject
{
    [Serializable]
    public struct Stats
    {
        public AnimatorController controller;
        public Sprite Sprite;
        
        public AnimationCurve maxHP;
        public AnimationCurve maxMana;
        public AnimationCurve maxEnergy;
        public AnimationCurve maxRage;
        public AnimationCurve speed;
        public AnimationCurve strength;
        public AnimationCurve agility;
        public AnimationCurve intellect;
        public AnimationCurve spirit;
        
        public AnimatorController m_Animator;
        public bool m_FlipX;
        
        public float MaxLevel;
        
        public float MaxHP;
        public float MaxMana;
        public float MaxEnergy;
        public float MaxRage;
        public float MaxSpeed;
        public float MaxStrength;
        public float MaxAgility;
        public float MaxIntellect;
        public float MaxSpirit;
    }

    [Serializable]
    public struct SkillUnlock
    {
        public string name;
        public int level;
        public Skill skill;
    }



    public List<SkillUnlock> skillUnlock;
    public Stats stats;
}


[Serializable]
public class CharacterInstance
{
    public CharacterClass characterClass;


    public float HP;
    public float Mana;
    public float Energy;
    public float Rage;
    public float Speed;
    public float Strength;
    public float Agility;
    public float Intellect;
    public float Spirit;

    public float level;
    public float curveLevel;

    private float currentHP => characterClass.stats.maxHP.Evaluate(curveLevel) * characterClass.stats.MaxHP;
    private float currentMana => characterClass.stats.maxMana.Evaluate(curveLevel) * characterClass.stats.MaxMana;
    private float currentEnergy => characterClass.stats.maxEnergy.Evaluate(curveLevel) * characterClass.stats.MaxEnergy;
    private float currentRage => characterClass.stats.maxRage.Evaluate(curveLevel) * characterClass.stats.MaxRage;
    private float currentSpeed => characterClass.stats.speed.Evaluate(curveLevel) * characterClass.stats.MaxSpeed;
    private float currentStrength => characterClass.stats.strength.Evaluate(curveLevel) * characterClass.stats.MaxStrength;
    private float currentAgility => characterClass.stats.agility.Evaluate(curveLevel) * characterClass.stats.MaxAgility;
    private float currentIntellect => characterClass.stats.intellect.Evaluate(curveLevel) * characterClass.stats.MaxIntellect;
    private float currentSpirit => characterClass.stats.spirit.Evaluate(curveLevel) * characterClass.stats.MaxSpirit;

    public float percentHP => HP / (characterClass.stats.spirit.Evaluate(curveLevel) * characterClass.stats.MaxSpirit);
    public float percentMana => Mana / (characterClass.stats.maxMana.Evaluate(curveLevel) * characterClass.stats.MaxMana);
    public float percentEnergy => (Energy / characterClass.stats.MaxEnergy) * 100f;
    public float percentRage => (Rage / characterClass.stats.MaxRage) * 100f;
    public float percentSpeed => (Speed / characterClass.stats.MaxSpeed) * 100f;
    public float percentStrength => (Strength / characterClass.stats.MaxStrength) * 100f;
    public float percentAgility => (Agility / characterClass.stats.MaxAgility) * 100f;
    public float percentIntellect => (Intellect / characterClass.stats.MaxIntellect) * 100f;
    public float percentSpirit => (Spirit / characterClass.stats.MaxSpirit) * 100f;


    public CharacterInstance(CharacterClass _class)
    {
        characterClass = _class;
        level = 0f;
        LevelUp();
    }

    public void LevelUp()
    {
        level++;
        UpdateStats();
    }

    public void LevelUp(int _level)
    {
        level = _level;
        UpdateStats();
    }

    private void UpdateStats()
    {
        curveLevel = level / characterClass.stats.MaxLevel;

        HP = currentHP;
        Mana = currentMana;
        Energy = currentEnergy;
        Rage = currentRage;
        Speed = currentSpeed;
        Strength = currentStrength;
        Agility = currentAgility;
        Intellect = currentIntellect;
        Spirit = currentSpirit;
    }
}