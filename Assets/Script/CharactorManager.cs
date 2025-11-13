using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CharactorManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected MotionDictionaryn motionDictionaryn;
    protected GameObject targetOb;
    protected Vector3 startPos;
    protected string MotionInstructions;
    protected float attackPoint;
    protected float MoveSpeed;
    protected float attackInterval = 2f;
     protected Sprite attackImage;
     protected Sprite normalImage;
    protected float lastAttackTime = 0f;
    [SerializeField] protected GameObject HPPrefab;
    protected GameObject HPInstance;
    protected Canvas canvas;
    protected Vector3 hpOffset;
    protected float myHP = 0;
    [SerializeField]protected EnemySCO sco;
    protected string targetTag;
    protected bool attacking = false;


    [Header("Attack Common")]
    [SerializeField] protected string damageTargetTag = "PlayerBase"; // 敵は拠点を殴るのがデフォ
    [SerializeField] protected float attackRange = 0f; // 0なら距離チェックをしない


    public float GetConsumeMP()
    {
        return sco.MPConsumption;
    }
    private void PlayHPPrefab()
    {
        if (HPPrefab == null)
        {
            Debug.LogError($"{name}: HPPrefab が設定されていません。Inspectorで割り当ててください。");
            return;
        }

        HPInstance = Instantiate(HPPrefab, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
        if (canvas == null) Debug.Log("canvas_is_null");
        HPInstance.transform.SetParent(canvas.transform, false);
        HPInstance.GetComponent<FollowTransForm>().SetTarget(this.transform, hpOffset);
    }

    protected virtual void Start()
    {
        canvas = GameObject.FindWithTag("HPBarCanvas").GetComponent<Canvas>();
        MotionInstructions = "Invasion";
        startPos = transform.position;
        motionDictionaryn = GameObject.Find("MotionDirectory").GetComponent<MotionDictionaryn>();

        
       //HPの初期値をセット
        if (sco != null)
        {
                myHP = sco.fullHP;
                attackImage = sco.attackImage;
                normalImage = sco.normalImage;
                attackPoint = sco.attackPoint;
                attackInterval = sco.attackSpeed;
                MoveSpeed = sco.walkSpeed;
            if (!string.IsNullOrEmpty(sco.targetTagName))
            {
                targetTag = sco.targetTagName;
                targetOb = GameObject.FindWithTag(targetTag);
                Debug.Log($"<color=yellow>{gameObject.name}taegetTag is {targetTag}initialized</color>");
            }

            Debug.Log($"<color=yellow> '{sco.charaname}' initialized</color>");
        }
            else
        {
                Debug.LogWarning($"{gameObject.name}: SCOが設定されていません。");
        }
        
        PlayHPPrefab();
        

    }

    // Update is called once per frame
   public virtual void Update()
    {
        FindNearestTarget();
        
        if (MotionInstructions == null)
        {
            return;
        }

        if (sco.doFloat)
        {
            motionDictionaryn?.FloatMotion(startPos, gameObject, 0.5f, 1);
        }
        if (MotionInstructions == "Invasion")
        {
            if(targetOb == null)
            {
                return;
            }
            motionDictionaryn?.InvasionMotion(gameObject, targetOb, MoveSpeed);
            
        }
        else
        {
            return ;
        }



    }

    //==========================================
    #region === ターゲット探索 ===
    protected void FindNearestTarget()
    { 
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        if (targets.Length == 0)
        {
            targetOb = null;
            return;
        }

        float minDistance = Mathf.Infinity;
        GameObject nearest = null;

        foreach (GameObject t in targets)
        {
            float dist = Vector2.Distance(transform.position, t.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                nearest = t;
            }
        }

        targetOb = nearest;
    
    }


    #endregion
    //==========================================

    public void ImageChenge(int imagenum)
{
    // --- 安全チェック ---
    
    if (gameObject == null) return;

    SpriteRenderer imageRenderer = GetComponent<SpriteRenderer>();
    if (imageRenderer == null) return; 
    
    // --- スプライト切替 ---
    if (imagenum == 0 && normalImage != null)
        imageRenderer.sprite = normalImage;
    else if (imagenum == 1 && attackImage != null)
        imageRenderer.sprite = attackImage;
}

    #region 攻撃処理


    protected virtual bool IsValidTarget(GameObject obj)
    {
        if (obj == null) return false;
        string tagName = !string.IsNullOrEmpty(sco?.targetTagName)
            ? sco.targetTagName
            : targetTag; // こっちが正解
        return obj.CompareTag(tagName);
    }

    protected virtual void DoDamage(GameObject target)
    {
        CharactorManager targetManager = target.GetComponent<CharactorManager>();
        if (targetManager != null)
        {
            targetManager.HitDamage(attackPoint);
        }
        else
        {
            BaseManager bm = target.GetComponent<BaseManager>();
            bm?.ChengeHP(-attackPoint);
        }
    }

    GameObject toAttack;
    Coroutine attackCoroutine;  // 現在動いている攻撃コルーチンを保持

    protected IEnumerator AttackLoop()
{
    attacking = true;

    while (attacking && toAttack != null)
    {
        // 攻撃クールタイムチェック
        if (Time.time - lastAttackTime >= attackInterval)
        {
            lastAttackTime = Time.time;

            // 攻撃モーション再生（毎回呼ぶ）
            motionDictionaryn.AttackMotion(gameObject, attackInterval * 0.5f);

            // ダメージ適用
            DoDamage(toAttack);
        }

        yield return null;
    }

    attackCoroutine = null;
    attacking = false;
}

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        var tgt = collision.gameObject;
        if (!IsValidTarget(tgt)) return;

        float d = Vector2.Distance(transform.position, tgt.transform.position);
        if (attackRange > 0 && d > attackRange) return;

        MotionInstructions = "stop";
        toAttack = tgt;

        // 攻撃ループが動いていないときだけ開始
        if (attackCoroutine == null)
            attackCoroutine = StartCoroutine(AttackLoop());
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == toAttack)
        {
            attacking = false;
            MotionInstructions = "Invasion";
            toAttack = null;

            // 攻撃コルーチンを停止
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }



    #endregion
    #region 被ダメージ処理

    public void HitDamage(float getDamege)
    {
        if (HPInstance == null) { Debug.Log("HPInstanceNull"); return; }
        myHP -= getDamege;
        myHP = Mathf.Max(myHP, 0);
        HPBarChenger hPBar = HPInstance.GetComponent<HPBarChenger>();
        float ratio = Mathf.Clamp01(myHP / sco.fullHP);
        hPBar.GreenChenge(ratio);
         if(myHP <= 0)
        {
            OnDestroy();
        }
    }
    
    protected virtual void OnDestroy()
    {

        StopAllCoroutines(); 
        Destroy(HPInstance);
        Destroy(gameObject);
    }
    #endregion
}



