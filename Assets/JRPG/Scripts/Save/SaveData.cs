using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SaveData : ScriptableObject
{
    public string saveName;
    public Vector2 playerInWorldPosition;
    public string mapName;
    public List<CharacterInstance> party;
    
}
