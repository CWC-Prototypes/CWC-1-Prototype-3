using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score;

    private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        // Since we are not necessarily in the same scene as our parent (which may
        // be its own problem) we can't count on setting the reference by dragging
        // it in the Inspector. So, we find it... GameObject.Find works across scenes.
        scoreText = GameObject.Find("Score").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
