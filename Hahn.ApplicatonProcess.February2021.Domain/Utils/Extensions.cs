using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hahn.ApplicatonProcess.February2021.Domain.Utils
{
    /// <summary>
    /// Encapsulates the validation execution result
    /// </summary>
    public class ValidationMessage
    {
        public ValidationMessage()
        {
            Property = String.Empty;
            Message = String.Empty;
        }

        ///<summary>
        /// Property validated 
        ///</summary>
        ///<example>assetName</example>
        public string Property { get; set; }

        ///<summary>
        /// Description what's wrong
        ///</summary>
        ///<example>assetName required</example>
        public string Message { get; set; }
    }
    public static class Extensions
    {
        public static ValidationMessage FormatMessage(this ValidationFailure validationFailure)
        {
            return new ValidationMessage () { Property = validationFailure.PropertyName, Message = validationFailure.ErrorMessage };
        }

        public static IList<ValidationMessage> FormatErrors(this IList<ValidationFailure> list)
        {
            return list.Select(x => x.FormatMessage()).ToList();

        }
    }
}
