using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAOEEffect : MonoBehaviour
{
    [SerializeField] private float effectLastingTime = 0f;
    private CircleCollider2D AOEEffectCollider;

    public float AOE;
    public float AOEdamage;

    // Example animationTime
    [SerializeField] private float animationTime = 3f;

    private void Awake() {
        
    }

    void Start()
    {
        AOEEffectCollider = GetComponent<CircleCollider2D>();
        AOEEffectCollider.radius = AOE;
        // Effect last for x amount of time then not take effect anymore
        StartCoroutine(PersistExistance(effectLastingTime));
        StartCoroutine(PersistAnimationTime(animationTime));
    }

    IEnumerator PersistExistance(float EffectlastingTime)
    {       
        if(EffectlastingTime >= 0.1f){
            yield return new WaitForSeconds(EffectlastingTime);
        }
        else{
            yield return new WaitForFixedUpdate();
        }
        
        AOEEffectCollider.enabled = false;
    }

    IEnumerator PersistAnimationTime(float AnimationTime)
    {
        yield return new WaitForSeconds(AnimationTime);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AOE);
    }
}
