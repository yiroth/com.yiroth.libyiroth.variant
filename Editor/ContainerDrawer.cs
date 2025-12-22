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

using System.Linq;
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

            // Check if [HideAttribute] attribute is present on the field using this Container
            bool hideAttribute = fieldInfo.GetCustomAttributes(typeof(LibYiroth.Helper.Editor.HideAttribute), true).Any();

            // Calculate Rects with proper spacing
            float spacing = 5f;
            float fullWidth = position.width;
            
            if (hideAttribute)
            {
                // Draw only Variant (Type + Value)
                SerializedProperty variantProp = property.FindPropertyRelative("variable");
                if (variantProp != null)
                {
                    VariantDrawer.Draw(position, variantProp);
                }
            }
            else
            {
                // Layout: [Name (30%)]  [Variant (Rest)]
                float nameWidth = fullWidth * 0.3f;
                float variantWidth = fullWidth - nameWidth - spacing;

                Rect nameRect = new Rect(position.x, position.y, nameWidth, position.height);
                Rect variantRect = new Rect(position.x + nameWidth + spacing, position.y, variantWidth, position.height);

                // Draw Name Field
                SerializedProperty nameProp = property.FindPropertyRelative("name");
                if (nameProp != null)
                {
                    // GUIContent.none ensures we just get the text box, which prevents layout locking
                    EditorGUI.PropertyField(nameRect, nameProp, GUIContent.none);
                }
                else
                {
                    EditorGUI.LabelField(nameRect, "'Name' doesn't exist");
                }

                // Draw Type Dropdown & Value
                SerializedProperty variantProp = property.FindPropertyRelative("variable");
                if (variantProp != null)
                {
                    VariantDrawer.Draw(variantRect, variantProp);
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
