using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Goal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreCounter;
    [SerializeField] private Transform ballTransform;
    [SerializeField] private BallMovement ballMovement;

    private int score = 0;

    public int Score
    {
        set {
            score = value;
            scoreCounter.text = score.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        score++;
        scoreCounter.text = score.ToString();

        ballTransform.position = Vector2.zero;
        ballMovement.Trajectory = Vector2.zero;
        ballMovement.GameHasStarted = false;
    }
}
