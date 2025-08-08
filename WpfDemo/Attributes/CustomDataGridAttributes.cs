namespace WpfDemo.Attributes
{
    public class CustomDataGridAttributes
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class GridColumnAttribute : Attribute
        {
            public bool IsVisible { get; set; } = true;
            public bool IsEditable { get; set; } = false;
            public string Label { get; set; } = "";
        }

        [AttributeUsage(AttributeTargets.Class)]
        public class EnableSelectionAttribute : Attribute
        {
            public bool EnableCheckbox { get; set; } = true;
        }
    }
}
