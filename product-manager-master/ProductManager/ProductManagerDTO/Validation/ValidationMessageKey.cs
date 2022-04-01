using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Validators
{
    /// <summary>
    /// Helping to use strongly typed validation message keys.
    /// </summary>
    public enum ValidationMessageKey : int
    {
        Required,
        TooLong,
        NotExists,
        InvalidEmailAdresssFormat,
        InvalidPhoneNumberFormat,
        InternalServerError,
        Limited,
        NotEnoughStock,
        InvalidDeleteAttempt
    }
}
