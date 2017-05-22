using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public bool isCameraMoveEnable; // 当前状态是否允许移动摄像头
    public float cameraMoveSpeed = 0.1f;
    private bool isOnDrag;

    // Use this for initialization
    void Start()
    {
        isCameraMoveEnable = true;
        isOnDrag = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnGUI()
    {
        if (isCameraMoveEnable)
        {
            if (Event.current != null && Event.current.type == EventType.mouseDown)
            {
                isOnDrag = true;
                StartCoroutine(move());
            }

            if (Event.current != null && Event.current.type == EventType.mouseUp) {
                isOnDrag = false;
            }
        }
    }

    float mousePosX;

    IEnumerator move() {
        Vector3 position = transform.localPosition;
        mousePosX = Input.mousePosition.x;
        while (isOnDrag) {
            yield return null;
            position.z -= (Input.mousePosition.x - mousePosX) * cameraMoveSpeed;
            mousePosX = Input.mousePosition.x;
            transform.localPosition = position;
        }
    }
}
