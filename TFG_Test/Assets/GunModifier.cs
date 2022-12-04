using UnityEngine;
using System.Collections.Generic;

public class GunModifier : MonoBehaviour
{
    [Range(0,100)]
    public int Long;

    [Range(0, 100)]
    public int Big;

    public Color Body, Base;

    public bool RandomGunForm =false;
    public bool RandomGunColor =false;
    public bool RandomGun =false;

    private int OLong, OBig;

    private SkinnedMeshRenderer meshRenderer;

    //public List<Material> 
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();

        int index = GetComponent<Shader>().FindPropertyIndex("Body");

        GetComponent<Renderer>().material.color = Color.white;

    }

    // Update is called once per frame
    void Update()
    {
        if (OLong != Long || OBig != Big)
        {
            UpdateGun();
        }

        if (RandomGunForm)
        {
            RandomGunForm = false;
            CreateNewGun();
        }

        if (RandomGunColor)
        {
            RandomGunColor = false;
        }

        if (RandomGun)
        {
            RandomGun = false;
            CreateNewGun();

        }
    }

    private void CreateNewGun()
    {
        Long = Random.Range(0,100);
        Big = Random.Range(0,100);
        UpdateGun();
    }

    private void UpdateGun()
    {
        meshRenderer.SetBlendShapeWeight(0, Long);
        meshRenderer.SetBlendShapeWeight(1, Big);
        OLong = Long;
        OBig = Big;

    }
}
