using MediatR;
using OneExpense.Business.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneExpense.Business.Events
{
    public class Event : Message, INotification
    {
    }
}
