using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovorVeilSegmentGetter : MonoBehaviour
{
    [SerializeField] bool hideOnStart = false;
    List<CovorVeilSegment> veilSegments = new();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<CovorVeilSegment>(out CovorVeilSegment segment))
            {
                veilSegments.Add(segment);
            }
        }
    }

    private void Start()
    {
        if (hideOnStart)
            HideSegments();
    }

    public CovorVeilSegment GetCovorVeilSegment()
    {
        float rotation = transform.localEulerAngles.y;
        //Red 30 : 330, Orange 330 : 270, Yellow 270 : 210, Green 210 : 150, Blue 150 : 90, Purple 90 : 30

        for (int i = 0; i < veilSegments.Count; i++)
        {
            float checkRotation = 30f + i * 60f;
            
            if (checkRotation <= rotation)
                continue;
        
            return veilSegments[i];
        }

        return veilSegments[0];
    }

    public void HideSegments()
    {
        for (int i = 0; i < veilSegments.Count; i++)
        {
            veilSegments[i].gameObject.SetActive(false);
        }
    }

    public void ShowSegments()
    {
        for (int i = 0; i < veilSegments.Count; i++)
        {
            veilSegments[i].gameObject.SetActive(true);
        }
    }

    public void ResetSegments()
    {
        for (int i = 0; i < veilSegments.Count; i++)
        {
            StartCoroutine(veilSegments[i].SegmentCooldown());
        }
    }
}
