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
        print(transform.rotation.y);
        //Red 30 : 330, Orange 330 : 270, Yellow 270 : 210, Green 210 : 150, Blue 
    }

    public CovorVeilSegment GetCovorVeilSegment()
    {
        return null;
    }
}
