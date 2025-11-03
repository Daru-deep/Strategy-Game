using System.Collections;
using UnityEngine;

public class MotionDictionaryn : MonoBehaviour
{
   

    #region ==浮遊モーション==
    public void FloatMotion(Vector3 startPos, GameObject target, float amplitude, float speed)
    {

        if (target == null) return;
        if (startPos.y < amplitude)
        {
            float newY = startPos.y + Mathf.Sin(Time.time * speed) * amplitude;
            target.transform.position = new Vector3(target.transform.position.x, newY);
        }
        else
        {
            target.transform.position = new Vector3(target.transform.position.x, startPos.y);
        }
    }
    #endregion
    //=========================================

    //=========================================
    #region ==侵攻モーション==
    public void InvasionMotion(GameObject target, GameObject targetOb, float speed)
    {
        if (target == null) return;
        Vector3 targetPos = new Vector3(targetOb.transform.position.x + 0.5f, targetOb.transform.position.y, targetOb.transform.position.z);
        target.transform.position = Vector3.MoveTowards(
          target.transform.position,    // ���݈ʒu
          targetPos,    // �ڕW�ʒu
          speed * Time.deltaTime        // ���t���[���i�ދ���
      );
    }
    #endregion
    //==========================================

    //================================================================
    #region ==攻撃モーション==
    public void AttackMotion(GameObject target, float interval)
    {
        if (target == null) return;
        StartCoroutine(Attacking(target, interval));
    }

    
    IEnumerator Attacking(GameObject target, float interval)
    {
        if (target == null) yield break;
        CharactorManager manager = target.GetComponent<CharactorManager>();
        if (manager == null) yield break;
        if (manager == null || manager.gameObject == null) yield break; // 🔥 安全チェック

            manager.ImageChenge(1);
            yield return new WaitForSeconds(interval * 0.5f);

            // 攻撃途中でDestroyされた場合の安全確認
            if (manager == null || manager.gameObject == null) yield break;
            manager.ImageChenge(0);
    }
    #endregion
    //================================================================
}
