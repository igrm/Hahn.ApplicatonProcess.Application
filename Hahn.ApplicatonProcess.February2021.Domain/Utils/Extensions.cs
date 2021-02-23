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

    [AttributeUsage(AttributeTargets.Property)]
    public class MatchParentAttribute : Attribute
    {
        public readonly string ParentPropertyName;
        public MatchParentAttribute(string parentPropertyName)
        {
            ParentPropertyName = parentPropertyName;
        }
    }

    public static class Extensions
    {
        public static ValidationMessage FormatMessage(this ValidationFailure validationFailure)
        {
            return new ValidationMessage() { Property = validationFailure.PropertyName, Message = validationFailure.ErrorMessage };
        }

        public static IList<ValidationMessage> FormatErrors(this IList<ValidationFailure> list)
        {
            return list.Select(x => x.FormatMessage()).ToList();

        }

        public static void CopyPropertiesFrom(this object self, object parent)
        {
            var fromProperties = parent.GetType().GetProperties();
            var toProperties = self.GetType().GetProperties();

            foreach (var fromProperty in fromProperties)
            {
                foreach (var toProperty in toProperties)
                {
                    if (fromProperty.Name == toProperty.Name && fromProperty.PropertyType == toProperty.PropertyType)
                    {
                        toProperty.SetValue(self, fromProperty.GetValue(parent));
                        break;
                    }
                }
            }
        }

        public static void MatchPropertiesFrom(this object self, object parent)
        {
            var childProperties = self.GetType().GetProperties();
            foreach (var childProperty in childProperties)
            {
                var attributesForProperty = childProperty.GetCustomAttributes(typeof(MatchParentAttribute), true);
                var isOfTypeMatchParentAttribute = false;

                MatchParentAttribute? currentAttribute = null;

                foreach (var attribute in attributesForProperty)
                {
                    if (attribute.GetType() == typeof(MatchParentAttribute))
                    {
                        isOfTypeMatchParentAttribute = true;
                        currentAttribute = (MatchParentAttribute)attribute;
                        break;
                    }
                }

                if (isOfTypeMatchParentAttribute)
                {
                    var parentProperties = parent.GetType().GetProperties();
                    object? parentPropertyValue = null;
                    foreach (var parentProperty in parentProperties)
                    {
                        if (currentAttribute is not null && parentProperty.Name == currentAttribute.ParentPropertyName)
                        {
                            if (parentProperty.PropertyType == childProperty.PropertyType)
                            {
                                parentPropertyValue = parentProperty.GetValue(parent);
                            }
                        }
                    }
                    childProperty.SetValue(self, parentPropertyValue);
                }
            }

        }
    }
}
