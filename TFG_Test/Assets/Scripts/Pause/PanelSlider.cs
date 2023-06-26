using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class PanelSlider : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TMPro.TextMeshProUGUI Text;

    private PauseManager pauseManager;

    private int actualvalue , ID;

    public void SetValues(int id, int value, string name, PauseManager pm)
    {
        pauseManager = pm;
        ID = id;
        actualvalue = value;
        slider.value = actualvalue;

        Text.text = name;
    }
    public void Postition(float newPos)
    {
        this.transform.position = new Vector3(0, newPos, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value != actualvalue)
        {
            actualvalue = (int)slider.value;
            SendNewValue();
        }
    }
    public void SendNewValue()
    {
        pauseManager.UpdateBS(ID, actualvalue);
    }
    

}
