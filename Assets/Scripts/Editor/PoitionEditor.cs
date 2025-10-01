using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Poition))]
public class PoitionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Poition poition = (Poition)target;
        poition.EffectPoition = (Poition.Effect)EditorGUILayout.EnumPopup("Effect", poition.EffectPoition);

        if (poition.EffectPoition == Poition.Effect.health)
            poition.Health = EditorGUILayout.FloatField("Value", poition.Health);

        if (poition.EffectPoition == Poition.Effect.exp)
            poition.Exp = EditorGUILayout.FloatField("Value", poition.Exp);
        EditorUtility.SetDirty(target);
    }
}
