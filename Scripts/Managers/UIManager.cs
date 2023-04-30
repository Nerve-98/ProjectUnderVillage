using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager s_instance;
    public static UIManager Instance { get { return s_instance; } }
    public GameObject OreGetTextPrefab;
    public GameObject ScreenUI;


    void Awake()
    {
        s_instance = this;
        ScreenUI = GameObject.Find("ScreenUI");

    }
    public void OreGetText(int howmuch, Transform spawnposition)
    {
        TMPro.TextMeshProUGUI textMesh = Instantiate(OreGetTextPrefab, spawnposition.position, Quaternion.identity).GetComponentInChildren<TMPro.TextMeshProUGUI>();
        textMesh.text = "+" + howmuch.ToString() + "G";

    }

    public void DamageText(int damage, Transform spawnposition)
    {
        TMPro.TextMeshProUGUI textMesh = Instantiate(OreGetTextPrefab, spawnposition.position + Vector3.up, Quaternion.identity).GetComponentInChildren<TMPro.TextMeshProUGUI>();
        textMesh.color = Color.white;
        textMesh.fontSize = 1.5f;
        textMesh.outlineWidth = 0.1f;
        textMesh.text = damage.ToString();


    }
    void Start()
    {

    }

    void Update()
    {

    }
}
