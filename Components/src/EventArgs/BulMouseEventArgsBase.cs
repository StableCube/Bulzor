using System;
using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components
{
    public abstract class BulMouseEventArgsBase : EventArgs
    {
        public MouseEventArgs MouseEventArgs { get; set; }
    }
}