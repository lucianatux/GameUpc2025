using UnityEditor;
using UnityEngine;
using StatePattern;

[CustomEditor(typeof(EnemyAI), true)]
public class EnemyAIEditor : Editor
{
    private SerializedProperty enemyMoveSpeed;
    private SerializedProperty attackRange;
    private SerializedProperty retreatDistance;
    private SerializedProperty walkableLayer;

    private SerializedProperty attackCooldown;
    private SerializedProperty bulletPrefab;
    private SerializedProperty weaponTransform;
    private SerializedProperty attackTimer;

    private SerializedProperty warningPrefab;
    private SerializedProperty attentionPrefab;

    private SerializedProperty stunDuration;
    private SerializedProperty flashDuration;
    private SerializedProperty _waveID;
    private SerializedProperty roomID;

    private SerializedProperty rb;
    private SerializedProperty spriteRenderer;

    private bool showMovement = true;
    private bool showAttack = true;
    private bool showPrefabs = false;
    private bool showDamage = false;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EnemyAI enemy = (EnemyAI)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("ENEMY AI - Custom Inspector", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        showMovement = EditorGUILayout.Foldout(showMovement, "Movimiento y Detección", true);
        if (showMovement)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("enemyMoveSpeed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("attackRange"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("retreatDistance"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("walkableLayer"));
        }

        showAttack = EditorGUILayout.Foldout(showAttack, "Ataque", true);
        if (showAttack)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("attackCooldown"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletPrefab"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("weaponTransform"));
        }

        showPrefabs = EditorGUILayout.Foldout(showPrefabs, "Prefabs de Aviso", true);
        if (showPrefabs)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("warningPrefab"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("attentionPrefab"));
        }

        showDamage = EditorGUILayout.Foldout(showDamage, "Daño y Knockback", true);
        if (showDamage)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("flashDuration"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("stunDuration"));
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("_waveID"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("roomID"));

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Componentes Referenciados", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rb"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("spriteRenderer"));

        serializedObject.ApplyModifiedProperties();
    }
}