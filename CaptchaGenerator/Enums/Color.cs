using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CaptchaGenerator.Localization;


namespace CaptchaGenerator.Enums
{
    public enum Color
    {
        [Display(Name = "Red", ResourceType = typeof(Resources))]
        Red,

        [Display(Name = "Green", ResourceType = typeof(Resources))]
        Green,

        [Display(Name = "Blue", ResourceType = typeof(Resources))]
        Blue
    }
}