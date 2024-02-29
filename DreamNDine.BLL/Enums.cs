using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamNDine.BLL
{
    public enum PaymentStatus
    { 
        Unknown = 0,
        Paid = 1,
        PaidHalf = 2,
        NotPaid = 3
    }

    public enum LogType
    {
        Unknown = 0,
        Error = 1,
        Warning = 2,
        Information = 3
    }

    public enum BookingStatus
    {
        Unknown = 0,
        Confirmed = 1,
        Cancelled = 2,
        Pending = 3
    }

    public enum Role
    {
        Unknown = 0,
        Admin = 1,
        Client = 2,
        Owner = 3
    }

}
