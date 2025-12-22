/*
 * Copyright 2025 yiroth
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * Purpose: For to keep editor inspector UI clean
 */

using UnityEditor;
using UnityEngine;

namespace LibYiroth.Variant.Editor
{
    public static class VariantDrawer
    {
        public static void Draw(Rect position, SerializedProperty variantProp)
        {
            const float spacing = 5f;
            const float typeWidth = 80f;
            float valueWidth = position.width - typeWidth - spacing;

            Rect typeRect = new Rect(position.x, position.y, typeWidth, position.height);
            Rect valueRect = new Rect(position.x + typeWidth + spacing, position.y, valueWidth, position.height);

            // Look for type. If not found, Unity serialization hasn't refreshed or [SerializeField] is missing.
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
                    case VariantTypes.Vector2:
                        valueProp = variantProp.FindPropertyRelative("vector2Value");
                        break;
                    case VariantTypes.Vector3:
                        valueProp = variantProp.FindPropertyRelative("vector3Value");
                        break;
                }

                if (valueProp != null)
                {
                    EditorGUI.PropertyField(valueRect, valueProp, GUIContent.none);
                }
            }
            else
            {
                EditorGUI.LabelField(typeRect, "Serialization error", EditorStyles.miniLabel);
                EditorGUI.LabelField(valueRect, "Missing [SerializeField] in Variant", EditorStyles.miniLabel);
            }
        }
    }
}