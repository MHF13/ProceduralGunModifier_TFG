using UnityEngine;
using System.Collections.Generic;

public enum GunType
{
    Pistol,
    ShootGun
}


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

    public void setW0(int set) { weight0 = set; }
    public int getW0() { return weight0; }

}

public class GunModifier02 : MonoBehaviour
{
    private int numBS;

    // Esto sera para seleccionar otro modelo
    // Aun no haremos nada con eso
    public GunType type;

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
    public bool RandomGunColor = false;
    public bool RandomGun = false;

    //------------------------
    
    

    //-----------------------------------
    private void Start()
    {
        
        meshRenderer = GetComponent<SkinnedMeshRenderer>();

        Mesh GunMesh = meshRenderer.sharedMesh;
        numBS = GunMesh.blendShapeCount;

        blendShape = new BlendShape[numBS];

        //
        int[] ret = new int[numBS];
        for (int i = 0; i < numBS; i++)
        {
            string newName = GunMesh.GetBlendShapeName(i);
            blendShape[i] = new BlendShape(newName);
            ret[i] = blendShape[i].weightBS;
        }
        this.GetComponent<Gun>().SetNewBS(ret);


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
        // Form
        if (ChangeForm())
        {
            UpdateForm();
        }

        if (RandomGunForm)
        {
            RandomGunForm = false;
            CreateNewForm();
            UpdateForm();
        }

        // Colors
        if (ChangeColors())
        {
            UpdateColors();
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



    //-------------------------------------------------
    public void RandomForm()
    {
        CreateNewForm();
        UpdateForm();
    }
    public void RandomCollor()
    {
        CreateNewColors();
    }
    public void RandomAll()
    {
        CreateNewForm();
        CreateNewColors();
        UpdateGun();
    }

    // Form
    // Checks whether the values of sliders have been altered
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

    // Updates the value of the model's blendshapes with the ones we have saved
    private void UpdateForm()
    {
        // old

        /*
        for (int i = 0; i < numBS; i++)
        {
            meshRenderer.SetBlendShapeWeight(i, blendShape[i].weightBS);
            blendShape[i].setW0(blendShape[i].weightBS);
        }
        */
        // new
        int[] ret = new int[numBS];
        for (int i = 0; i < numBS; i++)
        {
            meshRenderer.SetBlendShapeWeight(i, blendShape[i].weightBS);
            blendShape[i].setW0(blendShape[i].weightBS);
            ret[i] = blendShape[i].weightBS;
        }
        this.GetComponent<Gun>().SetNewBS(ret);

    }
    
    // Assigns a random new valor for our blend shapes
    private void CreateNewForm()
    {
        foreach (var item in blendShape)
        {
           item.weightBS = Random.Range(0, 100);
        }
    }

    //------------
    // Colors
    // Checks whether the colour has been altered since the inspector
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
    // Update the colour of the model with the colours we have in our colour array
    private void UpdateColors()
    {
        for (int i = 0; i < numMat; i++)
        {
            materials[i].color = color[i];
            color0[i] = color[i];
        }
    }
    // Assign a new random colour to each material
    private void CreateNewColors()
    {
        for (int i = 0; i < numMat; i++)
        {
            color[i] = Random.ColorHSV();
        }
    }
    //-----------
    //All
    private void UpdateGun()
    {
        UpdateForm();

        UpdateColors();
    }

}