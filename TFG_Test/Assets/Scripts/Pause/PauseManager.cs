using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public List<GameObject> sliders;
    private GameManager gameManager;

    [SerializeField] private GunModifier02 gunModifier;
    [SerializeField] private Gun TheGun;

    [Range(0, 100)]
    public int[] values;
    public string[] names;

    [SerializeField] private Transform Panel;
    [SerializeField] private GameObject PanelSliderPrebab;

    public void SetGameManager(GameManager _GM) { gameManager = _GM; }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void GetBSValues()
    {
        int size = gunModifier.blendShape.Length;
        values = new int[size];
        names = new string[size];

        for (int i = 0; i < size; i++)
        {
            values[i] = gunModifier.blendShape[i].weightBS;
            names[i] = gunModifier.blendShape[i].name;
        }

        if (sliders.Count == 0)
        {
            GenerdNewBP();
        }
    }


    public void GenerdNewBP()
    {
        for (int i = values.Length - 1; i >= 0 ; i--)
        {
            GameObject go = Instantiate(PanelSliderPrebab, Panel);

            sliders.Add(go);
            
            PanelSlider a = go.GetComponent<PanelSlider>();
            a.SetValues(i,values[i],names[i], this);
            a.Postition(a.gameObject.GetComponent<RectTransform>().rect.height * (values.Length-(i+1)));
        }
    }

    public void UpdateBS(int id,int value)
    {
        values[id] = value;

        gunModifier.SetNewForm(values);
    }
}
