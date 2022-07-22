using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovorVeilSegment : MonoBehaviour
{
    [HideInInspector] public float attackDamage = 2f;
    [HideInInspector] public Color attackColor;
    [SerializeField] HitParticle hitEffect;
    MeshRenderer rend;
    Transform owner;
    Transform parent;
    bool attacking = false;
    public float attackTime = .5f;
    public float attackSpeed = 120f;
    public float attackCooldown = 2f;
    float startYRotation;

    IEnumerator attackRoutine;

    void Awake()
    {
        owner = GameObject.FindWithTag("Player").transform;
        rend = GetComponent<MeshRenderer>();
        attackColor = rend.material.color;
    }

    private void Start()
    {
        parent = transform.parent;
        startYRotation = transform.localEulerAngles.y;
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
        ResetSegment();
        yield return new WaitForSeconds(attackCooldown);
        rend.enabled = true;
        yield return LeanTween.scale(gameObject, Vector3.one, 1f).setEase(LeanTweenType.easeOutSine);
    }

    private void ResetSegment()
    {
        transform.parent = parent;
        transform.localEulerAngles = Vector3.up * startYRotation;
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one * 0.01f;
        rend.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (!other.CompareTag("Player") && attacking)
        {
            if (other.TryGetComponent<ColorChange>(out ColorChange colorChange))
            {
                colorChange.BlendColor(attackColor, attackDamage);
            }

            if (attacking)
            {
                StopCoroutine(attackRoutine);
            }

            Vector3 hitPosition = other.ClosestPointOnBounds(transform.position);
            SpawnParticle(hitPosition);
            StartCoroutine(SegmentCooldown());
        }
    }
 
    void SpawnParticle(Vector3 spawnPos)
    {
        HitParticle particle = Instantiate(hitEffect, spawnPos, Quaternion.identity);
        particle.Activate(attackColor);
    }
}
