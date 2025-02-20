using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.Configuration
{
    public class BirthdayValidation : ValidationAttribute
    {
        private int _minAge;
        private int _maxAge;
        public BirthdayValidation(int minAge, int maxAge)
        {
            _minAge = minAge;
            _maxAge = maxAge;
        }
        public override bool IsValid(object? value)
        {
            if (value is DateOnly)
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                int age = today.Year - ((DateOnly)value).Year;

                // Adjust the age if the birthday hasn't occurred yet this year.
                if ((DateOnly)value > today.AddYears(-age))
                {
                    age--;
                }
                if (age >= _minAge && age <= _maxAge)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
