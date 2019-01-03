using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unit.MonoBehaviours;
using Unit;

[CustomEditor(typeof(CombatManagerMono))]
public class CombatManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        CombatManagerMono target = this.target as CombatManagerMono;
        base.OnInspectorGUI();
        if (target.Creature1 == null)
            return;
        DrawCreature(target.Creature1);
        DrawCreature(target.Creature2);
    }
    private static void DrawCreature(Creature c)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Creature", c.Name);   
        EditorGUILayout.LabelField("Hitpoints", $"{c.CurrentHitPoints} / {c.MaxHitPoints}");
        EditorGUILayout.EndHorizontal();

    }
}
