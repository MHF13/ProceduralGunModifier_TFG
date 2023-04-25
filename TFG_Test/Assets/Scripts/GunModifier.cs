using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class GunModifier : MonoBehaviour
{
    //FORM
    //------------------------
    private int numBS;
    private string[] nameBS;

    [Header("Blend Shape weights")]
    [Range(0, 100)]
    public int[] weightBS;
    private int[] weight0;

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

    public int count = 0;
    private void Awake()
    {
        // Form
        meshRenderer = GetComponent<SkinnedMeshRenderer>();

        Mesh GunMesh = meshRenderer.sharedMesh;
        numBS = GunMesh.blendShapeCount;

        nameBS = new string[numBS];
        weightBS = new int[numBS];
        weight0 = new int[numBS];

        for (int i = 0; i < numBS; i++)
        {
            nameBS[i] = GunMesh.GetBlendShapeName(i);
            weightBS[i] = 0;
            weight0[i] = 0;
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
        count = numMat;
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
        for (int i = 0; i < numBS; i++)
        {
            if (weightBS[i] != weight0[i])
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
            meshRenderer.SetBlendShapeWeight(i, weightBS[i]);
            weight0[i] = weightBS[i];
        }
    }
    private void CreateNewForm()
    {
        for (int i = 0; i < numBS; i++)
        {
            weightBS[i] = Random.Range(0, 100);
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
