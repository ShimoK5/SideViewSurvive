using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Transform))]
[CanEditMultipleObjects]
public class WorldPosCS : Editor
{
    private Type _transformRotationGUIType;
    private System.Object _transformRotationGUIObject;
    private MethodInfo _rotationFieldMethod;
    private SerializedProperty _positionProperty;
    private SerializedProperty _scaleProperty;
    private Transform _transform;

    private void OnEnable()
    {
        _transform = target as Transform;
        _positionProperty = serializedObject.FindProperty("m_LocalPosition");
        _scaleProperty = serializedObject.FindProperty("m_LocalScale");

        _transformRotationGUIType = typeof(EditorApplication).Assembly.GetType("UnityEditor.TransformRotationGUI");
        if (_transformRotationGUIObject == null)
            _transformRotationGUIObject = Activator.CreateInstance(_transformRotationGUIType);

        var enableMethod = _transformRotationGUIType
            .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
            .First(m => m.Name == "OnEnable");

        // Initialize TransformRotationGUI
        enableMethod.Invoke(_transformRotationGUIObject, new object[]
        {
            serializedObject.FindProperty("m_LocalRotation"),
            EditorGUIUtility.TrTextContent("Rotation")
        });

        _rotationFieldMethod = _transformRotationGUIType
            .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
            .Where(m => m.Name == "RotationField")
            .FirstOrDefault(m => m.GetParameters().Length == 0);
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(_positionProperty);

        _rotationFieldMethod.Invoke(_transformRotationGUIObject, null);

        EditorGUILayout.PropertyField(_scaleProperty);

        using (new EditorGUI.DisabledScope(true))
        {
            EditorGUILayout.LabelField("World (Read Only)");
            EditorGUILayout.Vector3Field("Position", _transform.position);
            EditorGUILayout.Vector3Field("Rotation", _transform.rotation.eulerAngles);
            EditorGUILayout.Vector3Field("Scale", _transform.lossyScale);
        }
    }
}