using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator m_Animator;
    private Transform m_Transform;

    private bool m_IsRotated = false;
    private bool m_Moving = false;
    private static readonly int WalkingUp = Animator.StringToHash("WalkingUp");
    private static readonly int WalkingDown = Animator.StringToHash("WalkingDown");
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Interact = Animator.StringToHash("Interact");

    private void Save()
    {
        GameManager.Instance.m_SaveData.playerInWorldPosition = transform.position;
    }

    private void Load()
    {
        transform.position = GameManager.Instance.m_SaveData.playerInWorldPosition;
    }

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Transform = transform;
        GameManager.Instance.m_OnSave += Save;
        GameManager.Instance.m_OnLoad += Load;
    }

    void Update()
    {
        m_Moving = false;
        if (Time.timeScale > 0.0f)
        {
            ProcessInput();
        }

        if (!m_Moving)
        {
            m_Animator.SetBool(WalkingUp, false);
            m_Animator.SetBool(WalkingDown, false);
            m_Animator.SetBool(Walking, false);
        }
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Vector2 pos = m_Transform.position;

            pos.x += 1.0f * Time.deltaTime;

            m_Transform.position = pos;

            if (!m_IsRotated)
            {
                m_Transform.Rotate(0, 180, 0);
                m_IsRotated = true;
            }

            m_Animator.SetBool(Walking, true);
            m_Animator.SetBool(WalkingDown, false);
            m_Animator.SetBool(WalkingUp, false);
            m_Moving = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector2 pos = m_Transform.position;

            pos.x -= 1.0f * Time.deltaTime;

            m_Transform.position = pos;

            m_Animator.SetBool(Walking, true);
            m_Animator.SetBool(WalkingDown, false);
            m_Animator.SetBool(WalkingUp, false);
            if (m_IsRotated)
            {
                m_Transform.Rotate(0, 180, 0);
                m_IsRotated = false;
            }

            m_Moving = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector2 pos = m_Transform.position;

            pos.y -= 1.0f * Time.deltaTime;

            m_Transform.position = pos;

            m_Animator.SetBool(WalkingDown, true);
            m_Animator.SetBool(Walking, false);
            m_Moving = true;
        }

        if (Input.GetKey(KeyCode.W))
        {
            Vector2 pos = m_Transform.position;

            pos.y += 1.0f * Time.deltaTime;

            m_Transform.position = pos;

            m_Animator.SetBool(WalkingUp, true);
            m_Animator.SetBool(Walking, false);
            m_Moving = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            m_Animator.SetTrigger(Interact);
        }
    }
}