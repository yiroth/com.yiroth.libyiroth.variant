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
 * Purpose: Named container for the Variant type
 */

using UnityEngine;

namespace LibYiroth.Variant
{
    [System.Serializable]
    public class Container
    {
        [SerializeField] private string name;
        [SerializeField] private VariantTypes type;
        [SerializeField] private Variant variable;

        public Container(string name, VariantTypes type, Variant variable)
        {
            this.name = name;
            this.type = type;
            this.variable = variable;
        }
        
        public Container(string name, VariantTypes type, object value) : this(name, type, new Variant(value)) { }
        public Container(string name, VariantTypes type, float value) : this(name, type, new Variant(value)) { }
        public Container(string name, VariantTypes type, int value) : this(name, type, new Variant(value)) { }
        public Container(string name, VariantTypes type, bool value) : this(name, type, new Variant(value)) { }
        public Container(string name, VariantTypes type, string value) : this(name, type, new Variant(value)) { }
        public Container(string name, VariantTypes type, Vector2 value) : this(name, type, new Variant(value)) { }
        public Container(string name, VariantTypes type, Vector3 value) : this(name, type, new Variant(value)) { }

        public object GetRawVariable()
        {
            return variable.GetRawValue();
        }

        public T GetVariable<T>()
        {
            return variable.GetValue<T>();
        }
        
        public VariantTypes GetVariableType()
        {
            return variable.GetType();
        }
        
        public Container Clone()
        {
            // Variant is a struct => value-copy is safe
            return new Container(name, type, variable);
        }
    }
}