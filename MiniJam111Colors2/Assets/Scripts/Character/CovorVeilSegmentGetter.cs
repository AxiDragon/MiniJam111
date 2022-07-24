using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovorVeilSegmentGetter : MonoBehaviour
{
    public bool hideOnStart = false;
    [HideInInspector] public bool yellowAtStart = false;
    [SerializeField] Material red;
    [SerializeField] Material orange;
    [SerializeField] Material yellow;
    [SerializeField] Material green;
    [SerializeField] Material blue;
    [SerializeField] Material purple;

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

        if (hideOnStart)
            HideSegments();

        if (yellowAtStart)
            UpdateSegmentColor("Yellow");
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

    public void UpdateSegmentColor(string colorName)
    {
        List<string> colors = new List<string>();
        Material newMaterial = red;

        colors.Add(colorName);

        switch (colorName)
        {
            case "Green":
                newMaterial = green;
                break;
            case "Purple":
                newMaterial = purple;
                break;
            case "Orange":
                newMaterial = orange;
                break;
            case "Yellow":
                newMaterial = yellow;
                colors.Add("Orange");
                break;
        }

        foreach(string color in colors)
        {
            foreach(CovorVeilSegment segment in veilSegments)
            {
                if (segment.name == color)
                {
                    segment.rend.material = newMaterial;
                    segment.SetAttackColor();
                    segment.GetComponentInChildren<SetTrailColor>().SetColor();
                }
            }
        }

        FindObjectOfType<PlayerFighter>().attackDamage += 1f;
        FindObjectOfType<PlayerFighter>().attackCooldown -= .4f;
        GetComponent<CovorVeilMovement>().rotationSpeed += .1f;
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
