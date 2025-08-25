namespace WpfDemo.Attributes
{
    public class CustomDataGridAttributes
    {
        public class GridEntityAttribute : Attribute
        {
            public string Label { get; set; } = "";
        }

        public class GridColumnAttribute : Attribute
        {
            public bool IsVisible { get; set; } = true;
            public bool IsAddable { get; set; } = false;
            public bool IsEditable { get; set; } = false;
            public string Label { get; set; } = "";
            public Type? Type { get; set; }
        }

        public class EnableSelectionAttribute : Attribute
        {
            public bool EnableCheckbox { get; set; } = true;
        }
    }
}
