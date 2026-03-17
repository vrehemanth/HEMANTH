using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.WebAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EGI_Backend.WebAPI.Tests.Controllers
{
    public class NotificationsControllerTests
    {
        private readonly Mock<INotificationService> _mockNotificationSvc = new();
        private readonly NotificationsController _controller;

        public NotificationsControllerTests()
        {
            _controller = new NotificationsController(_mockNotificationSvc.Object);
        }

        private void SetupUser(Guid userId)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new System.Security.Claims.Claim[]
            {
                new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task GetNotifications_ValidUser_ReturnsOkWithList()
        {
            var userId = Guid.NewGuid();
            SetupUser(userId);
            var expectedList = new List<Notification> { new Notification() };
            _mockNotificationSvc.Setup(x => x.GetUserNotificationsAsync(userId, 20)).ReturnsAsync(expectedList);

            var result = await _controller.GetNotifications(false);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<Notification>>(ok.Value);
        }

        [Fact]
        public async Task GetUnreadCount_ValidUser_ReturnsOkWithCount()
        {
            var userId = Guid.NewGuid();
            SetupUser(userId);
            _mockNotificationSvc.Setup(x => x.GetUnreadCountAsync(userId)).ReturnsAsync(5);

            var result = await _controller.GetUnreadCount();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(ok.Value);
            var propertyInfo = ok.Value.GetType().GetProperty("count");
            var countValue = propertyInfo.GetValue(ok.Value, null);
            Assert.Equal(5, countValue);
        }

        [Fact]
        public async Task MarkAsRead_ValidId_ReturnsOk()
        {
            var userId = Guid.NewGuid();
            SetupUser(userId);
            var id = Guid.NewGuid();
            _mockNotificationSvc.Setup(x => x.MarkAsReadAsync(userId, id)).Returns(Task.CompletedTask);

            var result = await _controller.MarkAsRead(id);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task MarkAllAsRead_ValidRequest_ReturnsOk()
        {
            var userId = Guid.NewGuid();
            SetupUser(userId);
            _mockNotificationSvc.Setup(x => x.MarkAllAsReadAsync(userId)).Returns(Task.CompletedTask);

            var result = await _controller.MarkAllAsRead();

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetNotifications_UserMissing_ThrowsArgumentNullException()
        {
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity()) }
            };

            await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.GetNotifications(false));
        }
    }
}
