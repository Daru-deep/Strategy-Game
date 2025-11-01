using UnityEngine;

public class FollowTransForm : MonoBehaviour
{

    private Transform target; // 追従する対象
    private Vector3 offset; // オフセット（World Spaceのオフセット）
    private RectTransform rectTransform;


    public void SetTarget(Transform target, Vector3 offset)
    {
        this.target = target;
        this.offset = offset;
        rectTransform = GetComponent<RectTransform>();
        RefreshPosition();
    }
    public void SetTarget(Transform target)
    {
        SetTarget(target, Vector3.zero);
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        RefreshPosition();
    }

    private void RefreshPosition()
    {
        if (target)
        {

            Vector3 worldPosWithOffset = target.position + new Vector3(offset.x, offset.y, offset.z);
            Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPosWithOffset);
            screenPos += new Vector2(offset.x, offset.y);
            rectTransform.position = screenPos;


        }
    }

}