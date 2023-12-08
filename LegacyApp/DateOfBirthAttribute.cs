using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class DateOfBirthAttribute : ValidationAttribute
    {
        readonly int _minimumAge;

        public DateOfBirthAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        public override bool IsValid(object value)
        {
            var dateOfBirth = (DateTime)value;

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < _minimumAge)
            {
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name);
        }
    }
}
