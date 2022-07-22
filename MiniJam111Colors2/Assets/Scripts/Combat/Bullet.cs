using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float destroyTime = 10f;
    [SerializeField] float speed = 1f;
    [SerializeField] GameObject hitEffect;
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
        transform.Translate(transform.forward * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(hitEffect);
            //change hit enemy color
            Destroy(gameObject);
        }
    }
}
