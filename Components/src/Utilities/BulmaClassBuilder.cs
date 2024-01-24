using System;
using System.Text;

namespace StableCube.Bulzor.Components;

public class BulmaClassBuilder : IBulmaClassBuilder
{
    private readonly StringBuilder _sb = new StringBuilder();
    private readonly string _baseClass;

    public string ClassString { get { return ToString(); } }

    public BulColor? Color { get; set; }
    public BulSchemeColor? SchemeColor { get; set; }
    public BulSize? Size { get; set; }
    public BulSize? SizeChild { get; set; }
    public BulColor? TextColor{ get; set; }
    public BulColor? BackgroundColor { get; set; }
    public BulTextSize? TextSize { get; set; }
    public BulTextWeight? TextWeight { get; set; }
    public BulColumnSize? ColSize { get; set; }
    public BulSeparator? Separator { get; set; }
    public BulRatio? Ratio { get; set; }
    public BulDimension? Dimension { get; set; }
    public bool? IsRounded { get; set; }
    public bool? IsHoverable { get; set; }
    public bool? IsHovered { get; set; }
    public bool? IsFocused { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsLoading { get; set; }
    public bool? IsExpanded { get; set; }
    public bool? IsStatic { get; set; }
    public bool? HasIconsLeft { get; set; }
    public bool? HasIconsRight { get; set; }
    public bool? IsBoxed { get; set; }
    public bool? IsToggle { get; set; }
    public bool? IsToggleRounded { get; set; }
    public bool? HasAddons { get; set; }
    public bool? IsCentered { get; set; }
    public bool? HasDropdown { get; set; }
    public bool? HasDropdownUp { get; set; }
    public bool? IsArrowless { get; set; }
    public bool? IsRight { get; set; }
    public bool? IsLeft { get; set; }
    public bool? IsMultiple { get; set; }
    public bool? IsSelected { get; set; }
    public bool? IsSpaced { get; set; }
    public bool? IsLight { get; set; }
    public bool? IsOutlined { get; set; }
    public bool? IsFullWidth { get; set; }
    public bool? IsInverted { get; set; }
    public bool? HasName { get; set; }
    public bool? IsCircle { get; set; }
    public bool? HasRatio { get; set; }
    public bool? IsDelete { get; set; }
    public bool? IsVertical { get; set; }
    public bool? IsUp { get; set; }
    public bool? IsHidden { get; set; }
    public bool? IsInvisible { get; set; }
    public bool? IsCursorHidden { get; set; }
    public bool? IsTransparent { get; set; }
    public bool? IsFixedTop { get; set; }
    public bool? IsFixedBottom { get; set; }
    public bool? HasShadow { get; set; }

    public BulmaClassBuilder(){}

    public BulmaClassBuilder(string baseClass)
    {
        _baseClass = baseClass;
    }

    protected void ClearClassString()
    {
        _sb.Clear();
    }

    protected string GetClassString()
    {
        return _sb.ToString().Trim();
    }

    protected virtual void BuildClassString()
    {
        if(_baseClass != null)
            Append(_baseClass);

        if(Color.HasValue && Color.Value != BulColor.Default)
            Append(BulmaVariableMap.Color(Color.Value));

        if(SchemeColor.HasValue && SchemeColor.Value != BulSchemeColor.Default)
            Append(BulmaVariableMap.SchemeColor(SchemeColor.Value));

        if(Size.HasValue && Size.Value != BulSize.Default)
            Append(BulmaVariableMap.Size(Size.Value));

        if(SizeChild.HasValue && SizeChild.Value != BulSize.Default)
            Append(BulmaVariableMap.ChildSize(SizeChild.Value));
        
        if(TextColor.HasValue && TextColor.Value != BulColor.Default)
            Append(BulmaVariableMap.TextColor(TextColor.Value));

        if(BackgroundColor.HasValue && BackgroundColor.Value != BulColor.Default)
            Append(BulmaVariableMap.BackgroundColor(BackgroundColor.Value));
        
        if(TextSize.HasValue && TextSize.Value != BulTextSize.Default)
            Append(BulmaVariableMap.TextSize(TextSize.Value));
        
        if(TextWeight.HasValue && TextWeight.Value != BulTextWeight.Default)
            Append(BulmaVariableMap.TextWeight(TextWeight.Value));

        if(ColSize.HasValue && ColSize.Value != BulColumnSize.Default)
            Append(BulmaVariableMap.ColumnSize(ColSize.Value));

        if(Ratio.HasValue && Ratio.Value != BulRatio.Default)
            Append(BulmaVariableMap.Ratio(Ratio.Value));

        if(Dimension.HasValue && Dimension.Value != BulDimension.Default)
            Append(BulmaVariableMap.Dimension(Dimension.Value));

        if(Separator.HasValue)
            Append($"has-{Separator.Value.ToString().ToLower()}-separator");

        AppendIfTrue(IsRounded, "is-rounded");
        AppendIfTrue(IsHoverable, "is-hoverable");
        AppendIfTrue(IsHovered, "is-hovered");
        AppendIfTrue(IsFocused, "is-focused");
        AppendIfTrue(IsActive, "is-active");
        AppendIfTrue(IsLoading, "is-loading");
        AppendIfTrue(IsExpanded, "is-expanded");
        AppendIfTrue(HasIconsLeft, "has-icons-left");
        AppendIfTrue(HasIconsRight, "has-icons-right");
        AppendIfTrue(IsStatic, "is-static");
        AppendIfTrue(IsBoxed, "is-boxed");
        AppendIfTrue(IsToggle, "is-toggle");
        AppendIfTrue(IsToggleRounded, "is-toggle-rounded");
        AppendIfTrue(HasAddons, "has-addons");
        AppendIfTrue(IsCentered, "is-centered");
        AppendIfTrue(HasDropdown, "has-dropdown");
        AppendIfTrue(IsArrowless, "is-arrowless");
        AppendIfTrue(HasDropdownUp, "has-dropdown-up");
        AppendIfTrue(IsLeft, "is-left");
        AppendIfTrue(IsRight, "is-right");
        AppendIfTrue(IsMultiple, "is-multiple");
        AppendIfTrue(IsSelected, "is-selected");
        AppendIfTrue(IsSpaced, "is-spaced");
        AppendIfTrue(IsLight, "is-light");
        AppendIfTrue(IsOutlined, "is-outlined");
        AppendIfTrue(IsFullWidth, "is-fullwidth");
        AppendIfTrue(IsInverted, "is-inverted");
        AppendIfTrue(HasName, "has-name");
        AppendIfTrue(IsCircle, "is-circle");
        AppendIfTrue(HasRatio, "has-ratio");
        AppendIfTrue(IsDelete, "is-delete");
        AppendIfTrue(IsVertical, "is-vertical");
        AppendIfTrue(IsUp, "is-up");
        AppendIfTrue(IsHidden, "is-hidden");
        AppendIfTrue(IsInvisible, "is-invisible");
        AppendIfTrue(IsCursorHidden, "is-cursor-hidden");
        AppendIfTrue(IsTransparent, "is-transparent");
        AppendIfTrue(IsFixedTop, "is-fixed-top");
        AppendIfTrue(IsFixedBottom, "is-fixed-bottom");
        AppendIfTrue(HasShadow, "has-shadow");
    }

    public override string ToString()
    {
        ClearClassString();
        BuildClassString();

        return GetClassString();
    }

    protected void Append(string value)
    {
        _sb.Append(" ");
        if(!string.IsNullOrEmpty(CssConfig.Prefix))
            _sb.Append(CssConfig.Prefix);

        _sb.Append(value);
    }

    protected void AppendIfTrue(bool? doAppend, string value)
    {
        if(doAppend.HasValue && doAppend.Value == true)
            Append(value);
    }

    /// <summary>
    /// Adds a raw class value to the builder. 
    /// </summary>
    /// <param name="value">Class attribute to be added</param>
    public void SetRaw(string value)
    {
        Append(value);
    }
}
