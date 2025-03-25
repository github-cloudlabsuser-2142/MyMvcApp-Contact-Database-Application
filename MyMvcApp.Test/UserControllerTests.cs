using Xunit;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyMvcApp.Test
{
    public class UserControllerTests
    {
        private UserController _controller;
        private List<User> _users;

        public UserControllerTests()
        {
            _users = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane Doe", Email = "jane@example.com" }
            };
            _controller = new UserController();
            UserController.Users = _users;
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfUsers()
        {
            var result = _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Details_ReturnsViewResult_WithUser()
        {
            var result = _controller.Details(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<User>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public void Create_ReturnsViewResult()
        {
            var result = _controller.Create();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            var newUser = new User { Id = 3, Name = "Sam Smith", Email = "sam@example.com" };
            var result = _controller.Create(newUser);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal(3, _users.Count);
        }

        [Fact]
        public void Edit_Get_ReturnsViewResult_WithUser()
        {
            var result = _controller.Edit(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<User>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public void Edit_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            var updatedUser = new User { Id = 1, Name = "John Smith", Email = "johnsmith@example.com" };
            var result = _controller.Edit(1, updatedUser);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("John Smith", _users.First(u => u.Id == 1).Name);
        }

        [Fact]
        public void Delete_Get_ReturnsViewResult_WithUser()
        {
            var result = _controller.Delete(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<User>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public void Delete_Post_ReturnsRedirectToActionResult()
        {
            var result = _controller.DeleteConfirmed(1);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal(1, _users.Count);
        }
    }
}