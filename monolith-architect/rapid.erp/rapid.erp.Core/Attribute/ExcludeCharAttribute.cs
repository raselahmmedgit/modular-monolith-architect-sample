using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace rapid.erp.Core
{
    public sealed class ExcludeSpecialCharacter : ValidationAttribute, IClientModelValidator
    {
        private readonly string _value;
        public ExcludeSpecialCharacter(string chars)
            : base("{0} contains invalid character.")
        {
            _value = chars;
        }
        public ExcludeSpecialCharacter()
           : base("{0} contains invalid character.")
        {
            _value = "/.,!@#$%<>?$%^*";
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(
                context.Attributes, "data-val-exclude", GetErrorMessage()); //???
            MergeAttribute(
                context.Attributes, "data-val-exclude-value", _value);      //???
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key)) return false;

            attributes.Add(key, value);
            return true;
        }

        private string GetErrorMessage()
        {
            return $"Invalid character {_value}.";
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var valueAsString = value.ToString();
                for (int i = 0; i < _value.Length; i++)
                {
                    if (valueAsString.Contains(_value[i]))
                    {
                        var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                        return new ValidationResult(errorMessage);
                    }
                }
            }
            return ValidationResult.Success;
        }

    }
}
