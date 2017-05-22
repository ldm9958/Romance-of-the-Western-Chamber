using UnityEngine;

/// <summary>
/// 场景中的可以与Item交互的活跃物
/// </summary>
public abstract class ActiveObjectForItem : MonoBehaviour {
    protected bool isActiveWithItem = true; //当前是否处于与item可交互状态
    public abstract void ActivateWithItem(Item items);
}
