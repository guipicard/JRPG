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
        public AnimationCurve hp;
        public AnimationCurve strength;
        public AnimationCurve speed;
        public AnimationCurve intelligence;
        public AnimationCurve mana;
    }

    [Serializable]
    public struct SkillUnlock
    {
        public int level;
        public Skill skill;
    }

    public Stats stats;
    public List<SkillUnlock> skillUnlock;
}

public class Character
{
    public CharacterClass characterClass;

    public int level;
    public int currentHP;
    public int currentMana;
    public int currentSpeed;

    public Character(CharacterClass characterClass)
    {
        level = 1;
        currentMana = (int) characterClass.stats.mana.Evaluate(level);
    }
}


