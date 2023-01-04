using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackManager : MonoBehaviour
{

    public bool isGameStarted;
    public bool isGameOver;
    public static StackManager instance;
    public List<GameObject> collectedList = new List<GameObject>();

    private void Awake()
    {
        isGameStarted = false;
        isGameOver = false;
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (collectedList.Count > 0 && !isGameOver)
        {
            UpdateCollectedPosition();
        }
    }

    public void StackCollectable(GameObject other, int index)
    {
        other.transform.parent = transform;
        collectedList.Add(other.gameObject);
        StartCoroutine((UpdateCollectedScale()));
    }

    public void RemoveCollectable()
    {
        Destroy(collectedList[collectedList.Count - 1].gameObject, 0f);
        collectedList.RemoveAt(collectedList.Count - 1);
        StartCoroutine((UpdateCollectedScale()));
    }

    private IEnumerator UpdateCollectedScale()
    {

        for (int i = collectedList.Count - 1; i >= 0; i--)
        {
            int index = i;
            Vector3 defaultScale = new Vector3(1f, 1f, 1f);
            collectedList[index].transform.DOScale(defaultScale * 1.5f, 0.1f).OnComplete(() =>
                collectedList[index].transform.DOScale(defaultScale, 0.1f)
            );
            yield return new WaitForSeconds(0.05f);
        }

    }

    private void UpdateCollectedPosition()
    {
        for (int i = 1; i < collectedList.Count; i++)
        {
            int index = i;
            GameObject currentGameObject = collectedList[index];
            Vector3 position = new Vector3(collectedList[index - 1].transform.localPosition.x, 0.37f, collectedList[0].transform.localPosition.z + (index + 1 * 1.5f));
            currentGameObject.transform.DOLocalMove(position, 0.35f);
        }
    }

}
