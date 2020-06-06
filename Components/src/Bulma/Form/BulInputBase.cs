﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public abstract class BulInputBase<TValue> : ComponentBase
    {
        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the created element.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] 
        public IDictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets or sets the value of the input. This should be used with two-way binding.
        /// </summary>
        [Parameter]
        public TValue Value { get; set; }

        /// <summary>
        /// Gets or sets a callback that updates the bound value.
        /// </summary>
        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        /// <summary>
        /// Gets or sets an expression that identifies the bound value.
        /// </summary>
        [Parameter]
        public Expression<Func<TValue>> ValueExpression { get; set; }

        [Parameter]
        public BulPrimaryColor? Color { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        /// <summary>
        /// Gets or sets whether to show the loading icon
        /// </summary>
        [Parameter]
        public bool? Loading { get; set; }
    }
}