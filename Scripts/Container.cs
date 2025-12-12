namespace LibYiroth.Variant
{
    [System.Serializable]
    public class Container
    {
        public string name;

        public VariantTypes type;

        public Variant variable;

        private Container(string name, VariantTypes type)
        {
            this.name = name;
            this.type = type;
        }

        public Container(string name, VariantTypes type, float variable) : this(name, type)
        {
            this.variable = new Variant(variable);
        }

        public Container(string name, VariantTypes type, int variable) : this(name, type)
        {
            this.variable = new Variant(variable);
        }

        public Container(string name, VariantTypes type, bool variable) : this(name, type)
        {
            this.variable = new Variant(variable);
        }

        public Container(string name, VariantTypes type, string variable) : this(name, type)
        {
            this.variable = new Variant(variable);
        }

        public object GetRawVariable()
        {
            return variable.GetRawValue();
        }

        public T GetVariable<T>()
        {
            return variable.GetValue<T>();
        }
        
        public VariantTypes GetVariantType()
        {
            return variable.GetType();
        }
    }
}