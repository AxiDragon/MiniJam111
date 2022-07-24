using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckDifferenceUI : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public ColorCheck checker;

    private void Update()
    {
        textMesh.text = checker.difference.ToString();
    }
}


