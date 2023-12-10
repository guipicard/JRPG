using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    private Animator m_Animator;
    private Transform m_Transform;

    private bool m_IsRotated = false;
    private static readonly int WalkingUp = Animator.StringToHash("WalkingUp");
    private static readonly int WalkingDown = Animator.StringToHash("WalkingDown");
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Interact = Animator.StringToHash("Interact");

    private float m_playerSpeed = 1.0f;
    private bool walkingCoroutineStarted = false;
    private Coroutine walkingCoroutine;

    private bool isInCombatTrigger = false;

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
        if (GameManager.Instance.m_Fight != null)
        {
            Vector3 newPos = m_Transform.position;
            newPos.x = GameManager.Instance.m_LastPosition.x;
            newPos.y = GameManager.Instance.m_LastPosition.y;
            m_Transform.position = newPos;
            GameManager.Instance.EndFight();
        }
        
    }

    private void OnDestroy()
    {
        GameManager.Instance.m_OnSave -= Save;
        GameManager.Instance.m_OnLoad -= Load;
    }

    void Update()
    {
        if (Time.timeScale > 0.0f)
        {
            ProcessInput();
        }
    }

    private void ProcessInput()
    {
        float xValue = 0;
        float yValue = 0;

        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        Vector3 velocity = rigidbody.velocity;
        velocity.x = xAxis * m_playerSpeed;
        velocity.y = yAxis * m_playerSpeed;
        rigidbody.velocity = velocity;

        if (Input.GetKey(KeyCode.D))
        {
            xValue = 1;

            if (!m_IsRotated)
            {
                m_Transform.Rotate(0, 180, 0);
                m_IsRotated = true;
            }

            m_Animator.SetFloat("XInput", xValue);
            if (!walkingCoroutineStarted)
            {
                walkingCoroutine = StartCoroutine(MovementSound());
                walkingCoroutineStarted = true;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            xValue = -1;

            m_Animator.SetFloat("XInput", xValue);
            if (m_IsRotated)
            {
                m_Transform.Rotate(0, 180, 0);
                m_IsRotated = false;
            }

            if (!walkingCoroutineStarted)
            {
                walkingCoroutine = StartCoroutine(MovementSound());
                walkingCoroutineStarted = true;
            }
        }
        else
        {
            xValue = 0;
            m_Animator.SetFloat("XInput", xValue);
        }

        if (Input.GetKey(KeyCode.S))
        {
            yValue = -1;

            m_Animator.SetFloat("YInput", yValue);
            if (!walkingCoroutineStarted)
            {
                walkingCoroutine = StartCoroutine(MovementSound());
                walkingCoroutineStarted = true;
            }
        }
        else if (Input.GetKey(KeyCode.W))
        {
            yValue = 1;

            m_Animator.SetFloat("YInput", yValue);
            if (!walkingCoroutineStarted)
            {
                walkingCoroutine = StartCoroutine(MovementSound());
                walkingCoroutineStarted = true;
            }
        }
        else
        {
            yValue = 0;
            m_Animator.SetFloat("YInput", yValue);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            m_Animator.SetTrigger(Interact);
            AudioMan._instance.Play("Interact");
            if (isInCombatTrigger)
            {
                StopCoroutine(walkingCoroutine);
                GameManager.Instance.m_LastPosition = new Vector2(m_Transform.position.x, m_Transform.position.y);
                SceneManager.LoadScene("CombatScene");
            }
        }

        if (xValue == 0 && yValue == 0 && walkingCoroutineStarted)
        {
            StopCoroutine(walkingCoroutine);
            walkingCoroutineStarted = false;
        }
    }

    public IEnumerator MovementSound()
    {
        while (true)
        {
            AudioMan._instance.Play("Steps");
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("fight"))
        {
            isInCombatTrigger = true;
            GameManager.Instance.SetFight(collision.gameObject.GetComponent<Fight>().m_Fight);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInCombatTrigger = false;
        // GameManager.Instance.EndFight();
    }
}