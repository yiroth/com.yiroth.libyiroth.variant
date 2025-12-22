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
        String,
        Vector2,
        Vector3
    }

    [System.Serializable]
    public struct Variant
    {
        [SerializeField] private VariantTypes type;

        [SerializeField] private int intValue;
        [SerializeField] private float floatValue;
        [SerializeField] private bool boolValue;
        [SerializeField] private string stringValue;
        [SerializeField] private Vector2 vector2Value;
        [SerializeField] private Vector3 vector3Value;

        public Variant(object value) : this()
        {
            if (value == null) { type = VariantTypes.Empty; return; }
            
            switch (value)
            {
                case int i: Set<int>(i); break;
                case float f: Set<float>(f); break;
                case bool b: Set<bool>(b); break;
                case string s: Set<string>(s); break;
                case Vector2 s: Set<Vector2>(s); break;
                case Vector3 s: Set<Vector3>(s); break;
                default: type = VariantTypes.Empty; break;
            }
        }

        public Variant(int value) : this() => Set<int>(value);
        public Variant(float value) : this() => Set<float>(value);
        public Variant(bool value) : this() => Set<bool>(value);
        public Variant(string value) : this() => Set<string>(value);
        public Variant(Vector2 value) : this() => Set<Vector2>(value);
        public Variant(Vector3 value) : this() => Set<Vector3>(value);

        public void Set<T>(T v)
        {
            switch (v)
            {
                case int i:
                    type = VariantTypes.Int;
                    intValue = i;
                    return;
                case float f:
                    type = VariantTypes.Float;
                    floatValue = f;
                    return;
                case bool b:
                    type = VariantTypes.Bool;
                    boolValue = b;
                    return;
                case string s:
                    type = VariantTypes.String;
                    stringValue = s;
                    return;
                case Vector2 v2:
                    type = VariantTypes.Vector2;
                    vector2Value = v2;
                    return;
                case Vector3 v3:
                    type = VariantTypes.Vector3;
                    vector3Value = v3;
                    return;
                default:
                    type = VariantTypes.Empty;
                    return;
            }
        }

        public new VariantTypes GetType() => type;

        public object GetRawValue() => type switch
        {
            VariantTypes.Int => intValue,
            VariantTypes.Float => floatValue,
            VariantTypes.Bool => boolValue,
            VariantTypes.String => stringValue,
            VariantTypes.Vector2 => vector2Value,
            VariantTypes.Vector3 => vector3Value,
            _ => null
        };
        
        public T GetValue<T>()
        {
            if (typeof(T) == typeof(int)) return (T)(object)intValue;
            if (typeof(T) == typeof(float)) return (T)(object)floatValue;
            if (typeof(T) == typeof(bool)) return (T)(object)boolValue;
            if (typeof(T) == typeof(string)) return (T)(object)stringValue;
            if (typeof(T) == typeof(Vector2)) return (T)(object)vector2Value;
            if (typeof(T) == typeof(Vector3)) return (T)(object)vector3Value;

            try { return (T)System.Convert.ChangeType(GetRawValue(), typeof(T)); }
            catch { return default; }
        }
        
        public static VariantTypes FindVariantType<T>()
        {
            System.Type t = typeof(T);

            if (t == typeof(int)) return VariantTypes.Int;
            if (t == typeof(float)) return VariantTypes.Float;
            if (t == typeof(bool)) return VariantTypes.Bool;
            if (t == typeof(string)) return VariantTypes.String;
            if (t == typeof(Vector2)) return VariantTypes.Vector2;
            if (t == typeof(Vector3)) return VariantTypes.Vector3;

            return VariantTypes.Empty;
        }

        public static implicit operator int(Variant v) => v.GetValue<int>();
        public static implicit operator Variant(int i) => new Variant(i);
        public static implicit operator float(Variant v) => v.GetValue<float>();
        public static implicit operator Variant(float f) => new Variant(f);
        public static implicit operator bool(Variant v) => v.GetValue<bool>();
        public static implicit operator Variant(bool b) => new Variant(b);
        public static implicit operator string(Variant v) => v.GetValue<string>();
        public static implicit operator Variant(string s) => new Variant(s);
        public static implicit operator Vector2(Variant v) => v.GetValue<Vector2>();
        public static implicit operator Variant(Vector2 v) => new Variant(v);
        public static implicit operator Vector3(Variant v) => v.GetValue<Vector3>();
        public static implicit operator Variant(Vector3 v) => new Variant(v);
    }
}
