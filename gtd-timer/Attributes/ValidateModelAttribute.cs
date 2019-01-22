//-----------------------------------------------------------------------
// <copyright file="ValidateModelAttribute.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GtdTimer.Attributes
{
    /// <summary>
    /// class for validation model attributes
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// overriding on action executing method from action filter attribute class
        /// </summary>
        /// <param name="context">instance of context</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
