using UnityEngine;

public class SceneActivate : ActiveObjectForItem
{

    // Use this for initialization
    void Start()
    {
        isActiveWithItem = true; //当前是否处于与item可交互状态
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ActivateWithItem(Item items)
    {
        if (isActiveWithItem)
            return;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/" + items.itemName);
    }
}
