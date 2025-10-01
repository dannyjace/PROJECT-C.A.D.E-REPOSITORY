
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spawner))]

public class SpawnerEditor : Editor
{
    private SerializedProperty autoSpawnProp;
    private SerializedProperty autoSpawnObjects;
    private SerializedProperty autoSpawnCount;
    private SerializedProperty autoSpawnObjAtATime;
    private SerializedProperty autoSpawnRate;
    private SerializedProperty autoSpawnRadius;

    private SerializedProperty mainGroup;
    private SerializedProperty secondaryGroup;
    private SerializedProperty tertiaryGroup;
    private SerializedProperty quaternaryGroup;

    private SerializedProperty isBossSpawnerProp;
    private SerializedProperty bossObjects;
    private SerializedProperty bossSpawnPositions;
    private SerializedProperty bossSpawnRate;
    private SerializedProperty bossesAtATime;
    private SerializedProperty spawnAtCompletionProgess;


    private void OnEnable()
    {
        autoSpawnProp = serializedObject.FindProperty("autoSpawn");
        autoSpawnObjects = serializedObject.FindProperty("autoSpawnObjects");
        autoSpawnCount = serializedObject.FindProperty("autoSpawnCount");
        autoSpawnObjAtATime = serializedObject.FindProperty("autoSpawnObjAtATime");
        autoSpawnRate = serializedObject.FindProperty("autoSpawnRate");
        autoSpawnRadius = serializedObject.FindProperty("autoSpawnRadius");

        mainGroup = serializedObject.FindProperty("mainGroup");
        secondaryGroup = serializedObject.FindProperty("secondaryGroup");
        tertiaryGroup = serializedObject.FindProperty("tertiaryGroup");
        quaternaryGroup = serializedObject.FindProperty("quaternaryGroup");

        isBossSpawnerProp = serializedObject.FindProperty("isBossSpawner");
        bossObjects = serializedObject.FindProperty("bossObjects");
        bossSpawnPositions = serializedObject.FindProperty("bossSpawnPositions");
        bossSpawnRate = serializedObject.FindProperty("bossSpawnRate");
        bossesAtATime = serializedObject.FindProperty("bossesAtATime");
        spawnAtCompletionProgess = serializedObject.FindProperty("spawnAtCompletionProgess");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(autoSpawnProp);



        if (autoSpawnProp.boolValue)
        {
            EditorGUILayout.LabelField("Auto Spawn Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(autoSpawnObjects);
            EditorGUILayout.PropertyField(autoSpawnCount);
            EditorGUILayout.PropertyField(autoSpawnObjAtATime);
            EditorGUILayout.PropertyField(autoSpawnRate);
            EditorGUILayout.PropertyField(autoSpawnRadius);
        }

        if (!autoSpawnProp.boolValue)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Manual Spawn Groups", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(mainGroup);
            EditorGUILayout.PropertyField(secondaryGroup);
            EditorGUILayout.PropertyField(tertiaryGroup);
            EditorGUILayout.PropertyField(quaternaryGroup);
        }

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(isBossSpawnerProp);
        if (isBossSpawnerProp.boolValue)
        {
            EditorGUILayout.HelpBox("This spawner is configured for boss spawning. Ensure boss settings are properly set.", MessageType.Info);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Boss Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(bossObjects);
            EditorGUILayout.PropertyField(bossSpawnPositions);
            EditorGUILayout.PropertyField(bossSpawnRate);
            EditorGUILayout.PropertyField(bossesAtATime);
            EditorGUILayout.PropertyField(spawnAtCompletionProgess);
        }


        serializedObject.ApplyModifiedProperties();

    }


}
