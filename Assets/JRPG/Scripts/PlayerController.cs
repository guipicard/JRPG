using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SaveData saveData;

    private void Awake()
    {
        saveData = GameManager.Instance.SaveData;
        GameManager.Instance.onSave += Save;
        GameManager.Instance.onLoad += Load;
        GameManager.Instance.Load(saveData, GameManager.Instance.GetIndex());
    }

    public void Save()
    {
        saveData.playerInWorldPosition = transform.position;
    }
    public void Load()
    {
        transform.position = saveData.playerInWorldPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)) 
        {
            Vector2 pos = transform.position;

            pos.x += 10.0f * Time.deltaTime;

            transform.position = pos;
        }
    }
}
