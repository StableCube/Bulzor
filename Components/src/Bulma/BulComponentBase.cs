using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public abstract class BulComponentBase : ComponentBase
    {
        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the created element.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] 
        public IDictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();

        protected abstract void BuildBulma();

        /// <summary>
        /// Merges a 
        /// </summary>
        protected string MergeClassAttribute(string classString)
        {
            if(AdditionalAttributes == null || !AdditionalAttributes.ContainsKey("class"))
                return classString;

            return AdditionalAttributes["class"] + " " + classString;
        }
    }
}