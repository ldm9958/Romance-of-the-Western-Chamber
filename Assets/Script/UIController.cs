using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    private RectTransform  itemScoll;
    [SerializeField]
    private Button itemScollOpen;
    [SerializeField]
    private Button itemScollClose;
    [SerializeField]
    private RectTransform itemsMask;
    [SerializeField]
    private GameObject itemPrefab;

    // Use this for initialization
    void Start () {
        
        itemScollOpen.gameObject.SetActive(true);
        itemScollClose.gameObject.SetActive(false);
        Vector2 initScollSize = new Vector2(40f, 100f);
        itemScoll.sizeDelta = initScollSize;
        Vector3 initScollPosition = new Vector3(20f, 50f, 0);
        itemScoll.transform.position = initScollPosition;

        float posX = 0;
        //给itemsMask加一些子物体
        foreach (string item in GameData.items)
        {
            Image itemImage = (Instantiate(itemPrefab) as GameObject).GetComponent<Image>();
            itemImage.GetComponent<Item>().itemName = item;
            itemImage.sprite = Resources.Load<Sprite>("Icons/" + item);
            itemImage.SetNativeSize();
            itemImage.transform.SetParent(itemsMask.transform);
            Vector3 itemPositon = new Vector3(itemImage.rectTransform.sizeDelta.x / 2 + posX, 0);
            itemImage.transform.localPosition = itemPositon;
            posX += 60f;
        }
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void ItemScollOpen() // 卷轴打开
    {
        itemScollClose.gameObject.SetActive(true);
        itemScollClose.transform.position = new Vector3(72f, 40f);
        itemScollOpen.gameObject.SetActive(false);
        // 开始滚动
        StartCoroutine(OnScollOpen());
    }

    public float scollSpeed = 100f; //每秒变长这么多

    IEnumerator OnScollOpen() { // 缓慢打开item栏
        itemScollClose.enabled = false;
        float scollWidth = 40f + 60f * GameData.items.Count;
        while (itemScoll.sizeDelta.x < scollWidth)
        {
            float scollDistance = scollSpeed * Time.deltaTime; //一帧变长这么多
            Vector2 size = itemScoll.sizeDelta;
            size.x += scollDistance;
            if (size.x > scollWidth) size.x = scollWidth;
            itemScoll.sizeDelta = size;
            Vector3 scollPosition = itemScoll.transform.position;
            scollPosition.x = size.x / 2f; // 卷轴位置
            itemScoll.transform.position = scollPosition;
            // 移动红丝带
            Vector3 closePosition = itemScollClose.transform.position;
            closePosition.x += scollDistance;
            itemScollClose.transform.position = closePosition;
            // 移动mask + mask 拉长
            size = itemsMask.sizeDelta;
            size.x += scollDistance;
            itemsMask.sizeDelta = size;
            Vector3 maskPositon = itemsMask.position;
            maskPositon.x = 20f + size.x / 2f;
            itemsMask.transform.position = maskPositon;
            yield return null;
        }
        itemScollClose.enabled = true;
    }


    public void ItemScollClose() { // 关闭item卷轴
        StartCoroutine(OnScollClose());
    }

    IEnumerator OnScollClose() {    // 缓慢关闭item卷轴
        itemScollClose.enabled = false;
        while (itemScoll.sizeDelta.x > 40f) {
            float scollDistance = scollSpeed * Time.deltaTime; //一帧变长这么多
            Vector2 size = itemScoll.sizeDelta;
            size.x -= scollDistance;
            if (size.x < 40f) size.x = 40f;
            itemScoll.sizeDelta = size;
            Vector3 scollPosition = itemScoll.transform.position;
            scollPosition.x = size.x / 2f; // 卷轴位置
            itemScoll.transform.position = scollPosition;
            // 移动红丝带
            Vector3 closePosition = itemScollClose.transform.position;
            closePosition.x -= scollDistance;
            itemScollClose.transform.position = closePosition;
            // mask缩回去
            size = itemsMask.sizeDelta;
            size.x -= scollDistance;
            itemsMask.sizeDelta = size;
            Vector3 maskPositon = itemsMask.position;
            maskPositon.x = 20f + size.x / 2f;
            itemsMask.transform.position = maskPositon;
            yield return null;
        }
        itemScollOpen.gameObject.SetActive(true);
        itemScollClose.gameObject.SetActive(false);
        itemScollClose.enabled = true;
    }
}
