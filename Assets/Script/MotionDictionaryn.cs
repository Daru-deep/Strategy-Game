using System.Collections;
using UnityEngine;

public class MotionDictionaryn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {

    }

    public void FloatMotion(Vector3 startPos, GameObject target, float amplitude, float speed)
    {


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

    public void InvasionMotion(GameObject target, GameObject targetOb, float speed)
    {
        Vector3 targetPos = new Vector3(targetOb.transform.position.x + 0.5f, targetOb.transform.position.y, targetOb.transform.position.z);
        target.transform.position = Vector3.MoveTowards(
          target.transform.position,    // 現在位置
          targetPos,    // 目標位置
          speed * Time.deltaTime        // 毎フレーム進む距離
      );
    }
    public void AttackMotion(GameObject target, float interval)
    {
        StartCoroutine(Attacking(target, interval));
    }

    IEnumerator Attacking(GameObject target, float interval)
    {
        CharactorManager manager = target.GetComponent<CharactorManager>();
        if (manager != null)
        {
            manager.ImageChenge(1); // 攻撃中のスプライトに変更
            yield return new WaitForSeconds(interval);
            manager.ImageChenge(0); // 通常スプライトに戻す
        }
    }
}
