using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    public FightInfo m_Fight;
}

public enum EnemyType
{
    Boss,
    enemy
}

[CreateAssetMenu]
public class FightInfo : ScriptableObject
{
    public EnemyType type;
    public List<CharacterInstance> enemies;
    public float level;
    public float xp;
    public bool givesFriend;
    public CharacterClass friend;
    public bool done = false;
}
