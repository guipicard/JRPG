using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    public FightInfo m_Fight;
}

[CreateAssetMenu]
public class FightInfo : ScriptableObject
{
    public CharacterInstance enemy;
    public float level;
}
