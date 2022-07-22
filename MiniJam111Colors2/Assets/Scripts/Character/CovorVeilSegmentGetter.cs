using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovorVeilSegmentGetter : MonoBehaviour
{
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            GetCovorVeilSegment().Launch();
        }
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
}
