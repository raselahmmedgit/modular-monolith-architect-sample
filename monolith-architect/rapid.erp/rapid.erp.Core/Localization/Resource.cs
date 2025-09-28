using rapid.erp.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace rapid.erp.Core.Localization
{
    public class Resource : EntityBase
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(450)]
        public string Key { get; set; }

        public string Value { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string CultureId { get; set; }

        public Culture Culture { get; set; }
    }
}