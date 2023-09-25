using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterClass : ScriptableObject
{
    [Serializable]
    public struct Stats
    {
        public AnimationCurve maxHP;
        public AnimationCurve maxMana;
        public AnimationCurve maxEnergy;
        public AnimationCurve maxRage;
        public AnimationCurve speed;
        public AnimationCurve strength;
        public AnimationCurve agility;
        public AnimationCurve intellect;
        public AnimationCurve spirit;
    }

    [Serializable]
    public struct SkillUnlock
    {
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
    public int MaxHP => (int)characterClass.stats.maxHP.Evaluate(level);
    public int MaxMana => (int)characterClass.stats.maxMana.Evaluate(level);
    public int MaxEnergy => (int)characterClass.stats.maxEnergy.Evaluate(level);
    public int Speed => (int)characterClass.stats.speed.Evaluate(level);
    public int Strength => (int)characterClass.stats.strength.Evaluate(level);
    public int Agility => (int)characterClass.stats.agility.Evaluate(level);
    public int Intellect => (int)characterClass.stats.intellect.Evaluate(level);
    public int Spirit => (int)characterClass.stats.spirit.Evaluate(level);

    public int level;
    public int currentHP;
    public int currentMana;
    public int currentEnergy;
    public int currentSpeed;
    public int currentStrength;
    public int currentAgility;
    public int currentIntellect;
    public int currentSpirit;


    public CharacterInstance(CharacterClass _class)
    {
        characterClass = _class;

        level = 1;
        currentHP = MaxHP;
        currentMana = MaxMana;
        currentEnergy = MaxEnergy;
        currentSpeed = Speed;
        currentStrength = Strength;
        currentAgility = Agility;
        currentIntellect = Intellect;
        currentSpirit = Spirit;
    }
}
