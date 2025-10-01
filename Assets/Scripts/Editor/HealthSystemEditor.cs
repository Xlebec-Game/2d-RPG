using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;

[CustomEditor(typeof(HealthSystem))]
public class HealthSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        HealthSystem healthSystem = (HealthSystem)target;

        healthSystem.TypeCharacter = (Type)EditorGUILayout.EnumPopup("Type", healthSystem.TypeCharacter);
        healthSystem.Health = EditorGUILayout.FloatField("Health", healthSystem.Health);

        if (healthSystem.TypeCharacter == Type.player)
        {
            healthSystem.HPslider = (Slider)EditorGUILayout.ObjectField("HPSlider", healthSystem.HPslider, typeof(Slider), true);
            healthSystem.Upgrade = (PlayerUpgraide)EditorGUILayout.ObjectField("Upgrade", healthSystem.Upgrade, typeof(PlayerUpgraide), true);
        }           

        if (healthSystem.TypeCharacter == Type.enemy)        
            healthSystem.HardEnemy = EditorGUILayout.Toggle("Is Hard Enemy", healthSystem.HardEnemy);   
        EditorUtility.SetDirty(target);
    }
}
