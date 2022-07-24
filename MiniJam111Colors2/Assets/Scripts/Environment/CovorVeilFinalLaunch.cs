using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CovorVeilFinalLaunch : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] UnityEvent hitEvent;
    Action hitAction;

    private void Start()
    {
        hitAction = () => { hitEvent.Invoke(); };
    }

    public void Trigger()
    {
        //Vector3 position = transform.position;
        transform.parent = null;
        //transform.position = position;
        //StartCoroutine(Launch());
    }

    IEnumerator Launch()
    {
        yield return new WaitForSeconds(2f);
        LeanTween.move(gameObject, target.position, .5f).setEaseInCubic().setOnComplete(hitAction);
    }
}
