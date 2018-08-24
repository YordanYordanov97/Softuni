using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NbaStore.App.Infrastructure.Validations
{
    public class SizeValidationAttirubte : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var size  = value as string;
            if (size == null)
            {
                return true;
            }
            
            if(size!="Small" && size!="Medium" && size != "Large" && size != "XXL")
            {
                return false;
            }

            return true;
        }
    }
}
