using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class BlendShape
{
    [HideInInspector]
    public string name;
    [Range(0, 100)]
    public int weightBS;
    private int weight0;

    public BlendShape(string _name)
    {
        name = _name;
        weightBS = 0;
        weight0 = 0;
    }

    public void setW0(int set)
    {
        weight0 = set;
    }
    public int getW0() { return weight0; }


}

[ExecuteInEditMode]
public class GunModifier02 : MonoBehaviour
{
    //FORM
    //------------------------
    private int numBS;

    /*private string[] nameBS;
    [Header("Blend Shape weights")]
    [Range(0, 100)]
    public int[] weightBS;
    private int[] weight0;*/
    [Header("blendShapes")]
    [SerializeField]
    public BlendShape[] blendShape;

    private SkinnedMeshRenderer meshRenderer;

    //Materials
    //------------------------
    private int numMat;

    [Header("Materials colors")]
    public Color[] color;
    private Color[] color0;

    private Material[] materials;

    //------------------------
    [Header("Randomizators")]
    public bool RandomGunForm =false;
    public bool RandomGunColor =false;
    public bool RandomGun =false;

    private void Awake()
    {
        // Form
        meshRenderer = GetComponent<SkinnedMeshRenderer>();

        Mesh GunMesh = meshRenderer.sharedMesh;
        numBS = GunMesh.blendShapeCount;

        blendShape = new BlendShape[numBS];

        for (int i = 0; i < numBS; i++)
        {
            string newName = GunMesh.GetBlendShapeName(i);
            blendShape[i] = new BlendShape(newName);
        }

        // Colors
        materials = GetComponent<Renderer>().materials;
        numMat = materials.Length;
        color = new Color[numMat];
        color0 = new Color[numMat];

        for (int i = 0; i < numMat; i++)
        {
            color[i] = materials[i].color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (ChangeForm())
        {
            UpdateForm();
        }

        if (ChangeColors())
        {
            UpdateColors();
        }

        if (RandomGunForm)
        {
            RandomGunForm = false;
            CreateNewForm();
            UpdateForm();
        }

        if (RandomGunColor)
        {
            RandomGunColor = false;
            CreateNewColors();
        }

        if (RandomGun)
        {
            RandomGun = false;
            CreateNewForm();
            CreateNewColors();
            UpdateGun();
        }

    }

    // Form
    private bool ChangeForm()
    {

        foreach (var item in blendShape)
        {
            if (item.weightBS != item.getW0())
            {
                return true;
            }
        }
        return false;

    }
    private void UpdateForm()
    {

        for (int i = 0; i < numBS; i++)
        {
            meshRenderer.SetBlendShapeWeight(i, blendShape[i].weightBS);
            blendShape[i].setW0(blendShape[i].weightBS);
        }


    }
    private void CreateNewForm()
    {
        foreach (var item in blendShape)
        {
           item.weightBS = Random.Range(0, 100);
        }

    }

    // Colors
    private bool ChangeColors()
    {
        for (int i = 0; i < numMat; i++)
        {
            if (color[i] != color0[i])
            {
                return true;
            }
        }
        return false;
    }
    private void UpdateColors()
    {
        for (int i = 0; i < numMat; i++)
        {
            materials[i].color = color[i];
            color0[i] = color[i];
        }
    }
    private void CreateNewColors()
    {
        for (int i = 0; i < numMat; i++)
        {
            color[i] = Random.ColorHSV();
        }
    }

    //All
    private void UpdateGun()
    {
        UpdateForm();

        UpdateColors();
    }

}