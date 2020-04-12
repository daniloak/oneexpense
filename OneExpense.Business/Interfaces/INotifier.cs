using OneExpense.Business.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneExpense.Business.Interfaces
{
    public interface INotifier
    {
        bool NotifactionExists();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
