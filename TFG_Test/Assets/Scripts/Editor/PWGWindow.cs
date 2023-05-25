using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PWGWindow : EditorWindow
{

    private GunModifier02 CurrentGun;
    private GunModifier02 currentlySelectedGun;
    private Transform newlySelectedGun;

    private void Awake()
    {
        
    }

    [MenuItem("Window/PGW")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(PWGWindow));
    }



    protected void OnFocus()
    {
        // Get asset the user is selecting
        newlySelectedGun = Selection.activeTransform;

        // If it's not null
        if (newlySelectedGun != null)
        {
            // If its a conversation scriptable, load new asset
            if (newlySelectedGun.GetComponent<GunModifier02>() != null)
            {
                currentlySelectedGun = newlySelectedGun.GetComponent<GunModifier02>();

                if (currentlySelectedGun != CurrentGun)
                {
                    LoadNewAsset(currentlySelectedGun);
                }
            }
        }
    }

    public void LoadNewAsset(GunModifier02 asset)
    {

        CurrentGun = asset;

        Repaint();

#if UNITY_EDITOR
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
#endif
    }


    private void Update()
    {
        if (true)
        {

        }
    }

    private void OnGUI()
    {
        //myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        //EditorGUILayout.LinkButton();

        if(GUILayout.Button("RandomGun")){

            //CurrentGun.RandomGun();
        }
        GUILayout.Button("RandomForm");
        GUILayout.Button("RandomColor");
    }
}
