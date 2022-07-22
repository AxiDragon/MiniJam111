using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float destroyTime = 10f;
    [SerializeField] GameObject hitEffect;
    public string faction;
    [HideInInspector] public float attackDamage = 2f;
    [HideInInspector] public float speed = 1f;
    
    Material bulletMaterial;

    void Awake()
    {
        GetMaterial();
        Destroy(gameObject, destroyTime);
    }

    private void GetMaterial()
    {
        bulletMaterial = GetComponent<MeshRenderer>().material;
    }

    public void SetColor(Color color)
    {
        if (bulletMaterial == null)
            GetMaterial();

        bulletMaterial.color = color;
    }

    void FixedUpdate()
    {
        transform.Translate(transform.forward * speed, Space.World);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (!coll.gameObject.CompareTag(faction))
        {
            if (hitEffect != null)
                Instantiate(hitEffect);

            if (coll.TryGetComponent<ColorChange>(out ColorChange colorChange))
            {
                colorChange.BlendColor(bulletMaterial.color, attackDamage);
            }

            Destroy(gameObject);
        }
    }
}
