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
 * Purpose: A structure to keep mostly used values all together to provide dynamic variable management
 */

using UnityEngine;

namespace LibYiroth.Variant
{
    public enum VariantTypes
    {
        Empty,
        Int,
        Float,
        Bool,
        String
    }

    [System.Serializable]
    public struct Variant
    {
        [SerializeField] private VariantTypes type;

        [SerializeField] private float floatValue;
        [SerializeField] private int intValue;
        [SerializeField] private bool boolValue;
        [SerializeField] private string stringValue;

        public Variant(object value) : this()
        {
            if (value == null) { type = VariantTypes.Empty; return; }
            
            switch (value)
            {
                case float f: Set<float>(f); break;
                case int i: Set<int>(i); break;
                case bool b: Set<bool>(b); break;
                case string s: Set<string>(s); break;
                default: type = VariantTypes.Empty; break;
            }
        }

        public Variant(float value) : this() => Set<float>(value);
        public Variant(int value) : this() => Set<int>(value);
        public Variant(bool value) : this() => Set<bool>(value);
        public Variant(string value) : this() => Set<string>(value);

        public void Set<T>(T v)
        {
            if (v.GetType() is T direct)
            {
                switch (direct)
                {
                    case float f:
                        type = VariantTypes.Float;
                        floatValue = f;
                        return;
                    case int i:
                        type = VariantTypes.Int;
                        intValue = i;
                        return;
                    case bool b:
                        type = VariantTypes.Bool;
                        boolValue = b;
                        return;
                    case string s:
                        type = VariantTypes.String;
                        stringValue = s;
                        return;
                }
            }

            type = VariantTypes.Empty;
        }

        public new VariantTypes GetType() => type;

        public object GetRawValue() => type switch
        {
            VariantTypes.Int => intValue,
            VariantTypes.Float => floatValue,
            VariantTypes.Bool => boolValue,
            VariantTypes.String => stringValue,
            _ => null
        };
        
        public T GetValue<T>()
        {
            if (typeof(T) == typeof(float)) return (T)(object)floatValue;
            if (typeof(T) == typeof(int)) return (T)(object)intValue;
            if (typeof(T) == typeof(bool)) return (T)(object)boolValue;
            if (typeof(T) == typeof(string)) return (T)(object)stringValue;

            try { return (T)System.Convert.ChangeType(GetRawValue(), typeof(T)); }
            catch { return default; }
        }

        public static implicit operator float(Variant v) => v.GetValue<float>();
        public static implicit operator Variant(float f) => new Variant(f);
        public static implicit operator int(Variant v) => v.GetValue<int>();
        public static implicit operator Variant(int i) => new Variant(i);
        public static implicit operator bool(Variant v) => v.GetValue<bool>();
        public static implicit operator Variant(bool b) => new Variant(b);
        public static implicit operator string(Variant v) => v.GetValue<string>();
        public static implicit operator Variant(string s) => new Variant(s);
    }
}