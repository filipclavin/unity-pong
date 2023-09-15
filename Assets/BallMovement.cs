using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private InputAction startButton;
    [SerializeField] private InputAction resetButton;
    [SerializeField] private float movementSpeed;
    [SerializeField] private List<Goal> goals;
    [SerializeField] private float padelInfluence;

    private bool gameHasStarted = false;
    public bool GameHasStarted
    {
        set { gameHasStarted = value; }
    }

    private Vector2 trajectory = Vector2.zero;
    public Vector2 Trajectory
    {
        set { trajectory = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        startButton.Enable();
        resetButton.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameHasStarted && startButton.WasPressedThisFrame())
        {
            StartGame();
        }

        if (resetButton.WasPressedThisFrame())
        {
            ResetGame();
        }

        if (gameHasStarted)
        {
            Move();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 collNormal = contact.normal;
        collNormal = new Vector2(Mathf.Round(collNormal.x), Mathf.Round(collNormal.y));

        // Ball hit on one of its sides
        if (collNormal.x != 0 )
        {
            float yContactLocal = collision.transform.InverseTransformPoint(contact.point).y;
            trajectory = new Vector2(-trajectory.x, trajectory.y + yContactLocal * padelInfluence);
            trajectory = movementSpeed * trajectory.normalized;
        }

        // Ball hit on top or bottom
        if (collNormal.y != 0)
        {
            trajectory = new Vector2(trajectory.x, -trajectory.y);
        }
    }

    public void StartGame()
    {
        float newX = Random.Range(-1f, 1f);

        if (newX < 1f && newX > 0f)
        {
            newX = 1f;
        }
        if (newX > -1f && newX < 0f)
        {
            newX = -1f;
        }

        float newY = Random.Range(-1f, 1f);

        if (newY < 1f && newY > 0f)
        {
            newY = 1f;
        }
        if (newY > -1f && newY < 0f)
        {
            newY = -1f;
        }

        trajectory = new Vector2(newX, newY);
        trajectory = movementSpeed * trajectory.normalized;

        gameHasStarted = true;
    }

    public void ResetGame()
    {
        trajectory = Vector2.zero;
        transform.position = Vector2.zero;

        foreach (Goal goal in goals)
        {
            goal.Score = 0;
        }

        gameHasStarted = false;
    }

    private void Move()
    {
        Vector2 pos = transform.position;

        pos += Time.deltaTime * trajectory;

        transform.position = pos;
    }
}
