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

    private int ID;

    public void SetNewValues(int id, int value, string name, PauseManager pm)
    {
        pauseManager = pm;
        ID = id;
        slider.value = value;

        Text.text = name;
    }
    public void Postition(float newPos)
    {
        this.transform.position = new Vector3(0, newPos, 0);
    }
    public void SendNewValue()
    {
        pauseManager.UpdateBS(ID, (int)slider.value);
    }

    public void SetValue(int value)
    {
        slider.value = value;
    }

    
}
