using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;

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

    private IEnumerator HandleTriggerFinishLine()
    {
        yield return new WaitForSeconds(0.2f);

        Transform collectPoint = transform.GetChild(0);
        int cubeCount = 0, sphereCount = 0, totalScore = 0;
        for (int i = 1; i < StackManager.instance.collectedList.Count; i++)
        {
            GameObject currentObject = StackManager.instance.collectedList[i];
            currentObject.transform.parent = collectPoint;
            if (currentObject.tag == "CollectedCube") { cubeCount++; }
            if (currentObject.tag == "CollectedSphere") { sphereCount++; }
            currentObject.transform.position = new Vector3(collectPoint.position.x, 0.75f * i, transform.position.z);
            currentObject.transform.localScale = new Vector3(.7f, .7f, .7f);
        }

        totalScore = (cubeCount * 5) + (sphereCount * 10);

        gameOverText.text = $"You've collected {cubeCount} cube(s), {sphereCount} sphere(s) \n\n Total Score: {totalScore}";

        Time.timeScale = 0;
        gameOverPanel.SetActive(true);

    }

}
