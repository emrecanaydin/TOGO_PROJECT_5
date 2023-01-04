using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePanel : MonoBehaviour, IDragHandler
{

    public Transform player;
    public Transform child;

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 position = player.position;
        position.x = Mathf.Clamp(position.x + (eventData.delta.x / 100), -3.49f, 3.49f);
        player.position = position;
    }

}
