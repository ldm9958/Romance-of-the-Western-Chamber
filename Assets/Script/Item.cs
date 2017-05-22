using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform myParent; // 记录当前父亲节点
    Vector2 myPosition; // 记录原来的位置
    Transform tempParent; // 记录临时的父亲节点
    public string itemName; // 道具名
    LayerMask mask; // 交互的层

    public void OnBeginDrag(PointerEventData eventData)
    {
        myParent = transform.parent; // 保留当前父节点
        transform.SetParent(tempParent); // 将当前父节点设置为canvas
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPosition; // 新的相对坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(tempParent.GetComponent<RectTransform>(), 
            Input.mousePosition, eventData.enterEventCamera, out newPosition); // 获得以canvas作为参照系的鼠标指针的相对位置
        transform.localPosition = newPosition; // 移动当前图标到节点上
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask.value)) {
            ActiveObjectForItem sa = hit.transform.GetComponent<SceneActivate>();
            sa.ActivateWithItem(this);
        }
        transform.SetParent(myParent); // 还原父节点
        transform.position = myPosition; // 还原位置
    }

    // Use this for initialization
    void Start () {
        tempParent = GameObject.Find("Canvas").transform; // 获得临时父节点canvas
        myPosition = transform.position; // 记录当前位置
        mask = 1 << LayerMask.NameToLayer("Activate");
    }
	
	// Update is called once per frame
	void Update () {
    }
}
