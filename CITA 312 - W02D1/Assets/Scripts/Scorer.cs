using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    private int intScore;

    private void Start()
    {
        scoreText.text = "Score: " + intScore;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectible")
        {
            intScore++;
            scoreText.text = "Score: " + intScore.ToString();
        }
    }
}
