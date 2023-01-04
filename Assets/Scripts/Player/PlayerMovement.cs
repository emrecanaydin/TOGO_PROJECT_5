using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverDetailText;
    public TextMeshProUGUI gameOverScoreText;

    private float _currentScore;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StackManager.instance.isGameStarted = true;
        }
        if (StackManager.instance.isGameStarted && !StackManager.instance.isGameOver)
        {
            Vector3 movement = new Vector3(0, 0, 1) * moveSpeed * Time.deltaTime;
            transform.position += movement;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Barrier")
        {
            StackManager.instance.RemoveCollectable();
        }
        if(other.tag == "FinishLine")
        {
            StackManager.instance.isGameOver = true;
            StartCoroutine(HandleTriggerFinishLine());
        }
    }

    IEnumerator CountUpToTarget(float targetScore)
    {
        while (_currentScore < targetScore)
        {
            _currentScore += 1f;
            _currentScore = Mathf.Clamp(_currentScore, 0f, targetScore);
            gameOverScoreText.text = "Score: " + _currentScore;
            yield return new WaitForSeconds(0.005f);
        }

        Transform collectPoint = transform.GetChild(0);
        for (int i = 1; i < StackManager.instance.collectedList.Count; i++)
        {
            GameObject currentObject = StackManager.instance.collectedList[i];
            currentObject.transform.parent = collectPoint;
            currentObject.transform.position = new Vector3(collectPoint.position.x, 0.75f * i, transform.position.z);
            currentObject.transform.localScale = new Vector3(.7f, .7f, .7f);
        }
        Time.timeScale = 0;
    }

    private IEnumerator HandleTriggerFinishLine()
    {
        yield return new WaitForSeconds(0.1f);
        int cubeCount = 0, sphereCount = 0, totalScore = 0;
        for (int i = 1; i < StackManager.instance.collectedList.Count; i++)
        {
            GameObject currentObject = StackManager.instance.collectedList[i];
            if (currentObject.tag == "CollectedCube") { cubeCount++; }
            if (currentObject.tag == "CollectedSphere") { sphereCount++; }
        }
        totalScore = (cubeCount * 5) + (sphereCount * 10);
        gameOverDetailText.text = $"You've collected {cubeCount} cube(s), {sphereCount} sphere(s)";
        gameOverPanel.SetActive(true);
        StartCoroutine(CountUpToTarget(totalScore));
    }

}
