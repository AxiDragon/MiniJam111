using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovorVeilSegment : MonoBehaviour
{
    [HideInInspector] public float attackDamage = 2f;
    [HideInInspector] public Color attackColor;
    [SerializeField] GameObject hitEffect;
    Transform parent;
    bool attacking = false;
    float attackTime = 1f;
    float attackSpeed = 15f;
    float attackCooldown = 2f;

    IEnumerator attackRoutine;

    void Start()
    {
        parent = transform.parent;
        attackColor = GetComponent<MeshRenderer>().material.color;
    }

    void Update()
    {
        
    }

    [ContextMenu("Launch")]
    public void Launch()
    {
        attackRoutine = LaunchSegment();
        StartCoroutine(attackRoutine);
    }

    IEnumerator LaunchSegment()
    {
        transform.parent = null;
        attacking = true;
        float timer = 0f;
     
        while (timer < attackTime)
        {
            transform.localScale += Vector3.right * Time.deltaTime * attackSpeed;
            timer += Time.deltaTime;
            yield return null;
        }

        yield return SegmentCooldown();
        attacking = false;
    }

    IEnumerator SegmentCooldown()
    {
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(attackCooldown);
        yield return LeanTween.scale(gameObject, Vector3.one, 12f).setEase(LeanTweenType.easeInElastic);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && attacking)
        {
            if (hitEffect != null)
                Instantiate(hitEffect);

            if (other.TryGetComponent<ColorChange>(out ColorChange colorChange))
            {
                colorChange.BlendColor(attackColor, attackDamage);
            }

            if (attacking)
            {
                StopCoroutine(attackRoutine);
            }

            StartCoroutine(SegmentCooldown());
        }
    }
}
