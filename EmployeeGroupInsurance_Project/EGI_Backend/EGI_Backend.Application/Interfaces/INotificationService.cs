using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface INotificationService
    {
        Task CreateNotificationAsync(Guid userId, string title, string message, string type);
        Task<List<Notification>> GetUserNotificationsAsync(Guid userId, int? take = null);
        Task MarkAsReadAsync(Guid notificationId);
        Task MarkAllAsReadAsync(Guid userId);
        Task<int> GetUnreadCountAsync(Guid userId);
    }
}
