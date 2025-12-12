using UnityEditor;
using UnityEngine;

namespace LibYiroth.Variant.Editor
{
    [CustomPropertyDrawer(typeof(Container))]
    public class ContainerDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Calculate Rects with proper spacing
            float spacing = 5f;
            float fullWidth = position.width;
            
            // Layout: [Name (30%)]  [Type (80px)]  [Value (Rest)]
            float nameWidth = fullWidth * 0.3f;
            float typeWidth = 80f;
            float valueWidth = fullWidth - nameWidth - typeWidth - (spacing * 2);

            Rect nameRect = new Rect(position.x, position.y, nameWidth, position.height);
            Rect typeRect = new Rect(position.x + nameWidth + spacing, position.y, typeWidth, position.height);
            Rect valueRect = new Rect(position.x + nameWidth + typeWidth + (spacing * 2), position.y, valueWidth, position.height);

            // 1. Draw Name Field
            SerializedProperty nameProp = property.FindPropertyRelative("name");
            if (nameProp != null)
            {
                // GUIContent.none ensures we just get the text box, which prevents layout locking
                EditorGUI.PropertyField(nameRect, nameProp, GUIContent.none);
            }
            else
            {
                EditorGUI.LabelField(nameRect, "Name Missing");
            }

            // 2. Draw Type Dropdown & Value
            SerializedProperty variantProp = property.FindPropertyRelative("variable");
            if (variantProp != null)
            {
                // Look for _type. If not found, Unity serialization hasn't refreshed or [SerializeField] is missing.
                SerializedProperty typeProp = variantProp.FindPropertyRelative("type");

                if (typeProp != null)
                {
                    // Draw Type Dropdown
                    EditorGUI.PropertyField(typeRect, typeProp, GUIContent.none);

                    // Draw the correct Value field
                    VariantTypes currentType = (VariantTypes)typeProp.enumValueIndex;
                    SerializedProperty valueProp = null;

                    switch (currentType)
                    {
                        case VariantTypes.Int:
                            valueProp = variantProp.FindPropertyRelative("intValue");
                            break;
                        case VariantTypes.Float:
                            valueProp = variantProp.FindPropertyRelative("floatValue");
                            break;
                        case VariantTypes.Bool:
                            valueProp = variantProp.FindPropertyRelative("boolValue");
                            break;
                        case VariantTypes.String:
                            valueProp = variantProp.FindPropertyRelative("stringValue");
                            break;
                    }

                    if (valueProp != null)
                    {
                        EditorGUI.PropertyField(valueRect, valueProp, GUIContent.none);
                    }
                }
                else
                {
                    EditorGUI.LabelField(typeRect, "Serialize Err", EditorStyles.miniLabel);
                    EditorGUI.LabelField(valueRect, "Missing [SerializeField] in Variant.cs?", EditorStyles.miniLabel);
                }
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}