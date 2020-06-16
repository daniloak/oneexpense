using System;
using System.Collections.Generic;
using System.Text;

namespace OneExpense.Business.Notifications
{
    public class Notification
    {
        public Notification(string property, string message)
        {
            Property = property;
            Message = message;
        }

        public string Property { get; }
        public string Message { get; }
    }
}
