using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace StableCube.Bulzor.Components.Extended;

public class BulImageRibbon : BulComponentBase
{
    [Parameter]
    public IEnumerable<Uri> Images { get; set; }

    [Parameter]
    public int DisplayCount { get; set; } = 5;

    /// <summary>
    /// The current focused image index on the ribbon
    /// </summary>
    [Parameter]
    public int Value { get; set; }

    [Parameter]
    public EventCallback<int> ValueChanged { get; set; }
    
    [Parameter]
    public BulRatio Ratio { get; set; } = BulRatio.R1by1;

    private RibbonImage[] RibbonImages { get; set; }

    protected BulmaClassBuilder RootClassBuilder = new("image-ribbon-ratio-wrap");
    protected BulmaClassBuilder WrapperClassBuilder = new("image-ribbon-ratio-wrap-inner");
    protected BulmaClassBuilder ContentClassBuilder = new("image-ribbon");

    protected override void BuildBulma()
    {
        
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", RootClassBuilder.ClassString);
        builder.AddAttribute(2, "style", $"padding-top: {GetRatioPercentage()}%");
        builder.AddContent(3, (RenderFragment)((builder2) => {
            builder2.OpenElement(4, "div");
            builder2.AddAttribute(5, "class", WrapperClassBuilder.ClassString);
            builder2.AddContent(6, (RenderFragment)((builder3) => {
                builder3.OpenElement(7, "div");
                builder3.AddMultipleAttributes(8, CombinedAdditionalAttributes);
                builder3.AddAttribute(9, "class", ContentClassBuilder.ClassString);
                
                builder3.OpenComponent<CascadingValue<int>>(10);
                builder3.AddAttribute(11, "Value", Value);
                builder3.AddAttribute(12, "ChildContent", (RenderFragment)((builder4) => {
                    foreach (var image in RibbonImages)
                    {
                        builder4.OpenRegion(13);

                        builder4.OpenComponent<BulImageRibbonItem>(0);
                        builder4.AddAttribute(1, "Image", image);
                        builder4.AddAttribute(2, "Active", IsVisible(image.Index));
                        builder4.AddAttribute(3, "Ratio", Ratio);
                        builder4.AddAttribute(4, "OnClick", EventCallback.Factory.Create<BulImageRibbonItemClickEventArgs>(this, OnClickHandlerAsync));
                        builder4.CloseComponent();

                        builder4.CloseRegion();
                    }
                }));
                builder3.CloseComponent();
                builder3.CloseElement();
            }));
            builder2.CloseElement();
        }));
        builder.CloseElement();
    }

    protected override void OnParametersSet()
    {
        BuildBulma();

        if (Images == null || Images.Count() == 0)
            throw new IndexOutOfRangeException("Image count must be greater than zero");

        Value = Math.Clamp(Value, 0, Images.Count() - 1);
        DisplayCount = Math.Clamp(DisplayCount, 1, Images.Count());

        if(DisplayCount > Images.Count())
            DisplayCount = Images.Count();

        if(RibbonImages == null || RibbonImages.Length != Images.Count())
            RibbonImages = new RibbonImage[Images.Count()];
        
        for (int i = 0; i < Images.Count(); i++)
            RibbonImages[i] = new RibbonImage(i, Images.ElementAt(i));

        base.OnParametersSet();
    }

    /// <summary>
    /// Calculate the ribbon ratio using child ratios
    /// </summary>
    private double GetRatioPercentage()
    {
        double padPerc;
        double itemRatio = RatioHelper.FractionMap[Ratio];
        
        // double itemPerc = 100 / itemRatio;
        // int onLeft = VisibleOnSide(true);
        // int onRight = VisibleOnSide(false);

        double ribbonRatio = itemRatio * (double)DisplayCount / 1;

        padPerc = 100 / ribbonRatio;
        
        return padPerc;
    }

    /// <summary>
    /// Return the number of visible item to either side of focused index
    /// </summary>
    /// <param name="left">if true will return items to the left otherwise to the right</param>
    private int VisibleOnSide(bool left)
    {
        int halfDisp = (int)Math.Ceiling((double)(DisplayCount - 1) / 2);
        int overCount = Images.Count() - Value;
        int beginIdx = Value - halfDisp;
        if(Value < halfDisp)
            beginIdx = 0;

        if(overCount <= halfDisp)
            beginIdx = Images.Count() - DisplayCount;

        if(left)
            return Value - beginIdx;
        
        return beginIdx + DisplayCount - 1 - Value;
    }

    /// <summary>
    /// Returns true if the item at index should be visible
    /// </summary>
    private bool IsVisible(int index)
    {
        int halfDisp = (int)Math.Ceiling((double)(DisplayCount - 1) / 2);
        int overCount = Images.Count() - Value;
        int beginIdx = Value - halfDisp;
        if(Value < halfDisp)
            beginIdx = 0;

        if(overCount <= halfDisp)
            beginIdx = Images.Count() - DisplayCount;

        return index >= beginIdx && index <= beginIdx + DisplayCount - 1;
    }

    private async Task OnClickHandlerAsync(BulImageRibbonItemClickEventArgs args)
    {
        Value = args.Image.Index;
        await ValueChanged.InvokeAsync(Value);
    }
}
