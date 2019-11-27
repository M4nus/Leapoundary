using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 position = Vector3.Lerp(transform.position, mouseScreenPosition, Time.unscaledDeltaTime * 1000f);
        transform.position = position;
    }
}
