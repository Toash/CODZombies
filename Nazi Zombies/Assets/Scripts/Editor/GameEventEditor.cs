using UnityEngine;
using UnityEditor;

// Creates a custom Label on the inspector for all the scripts named GameEvent
[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameEvent gameEvent = (GameEvent)target;
        if (GUILayout.Button("Raise"))
        {
            gameEvent.Raise();
        }
    }
}
