using System.Text;
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

        protected IDictionary<string, object> CombinedAdditionalAttributes { get; set; } = new Dictionary<string, object>();

        private StringBuilder _combinedClassSb = new StringBuilder();

        protected abstract void BuildBulma();

        /// <summary>
        /// Merges or creates given class string into AdditionalAttributes
        /// </summary>
        protected void MergeOrCreateClassAttribute(string classString)
        {
            _combinedClassSb.Clear();
            CombinedAdditionalAttributes.Clear();

            if(AdditionalAttributes.ContainsKey("class"))
                _combinedClassSb.Append(AdditionalAttributes["class"] + " ");

            _combinedClassSb.Append(classString.Trim());

            if(!CombinedAdditionalAttributes.ContainsKey("class"))
            {
                CombinedAdditionalAttributes.Add("class", _combinedClassSb.ToString());
            }
            else
            {
                CombinedAdditionalAttributes["class"] = _combinedClassSb.ToString();
            }

            //Copy any other attributes
            foreach (var pair in AdditionalAttributes)
            {
                if(pair.Key == "class")
                    continue;

                CombinedAdditionalAttributes.Add(pair);
            }
        }

        /// <summary>
        /// Merges the builder class into AdditionalAttributes
        /// </summary>
        protected void MergeBuilderClassAttribute(BulmaClassBuilder builder)
        {
            MergeOrCreateClassAttribute(builder.ClassString);
        }
    }
}