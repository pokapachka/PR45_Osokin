using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ПР45_Осокин.Context;
using ПР45_Осокин.Model;

namespace ПР45_Осокин.Controllers
{
    [Route("api/UsersController")]
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiController]
    public class UsersController : Controller
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        /// <returns>Данный метод преднозначен для авторизации пользователя на сайте</returns>
        /// <response code="200">Пользователь успешно авторизован</response>
        /// <response code="401">Ошибка запроса, логин или пароль неверны!</response>
        /// <response code="403">Ошибка запроса, данные не указаны</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [HttpPost("SignIn")]
        public ActionResult SingIn([FromForm] string Login, [FromForm] string Password)
        {
            if (Login == null || Password == null)
                return StatusCode(403);
            try
            {
                Users User = new UsersContext().Users.Where(x => x.Login == Login && x.Password == Password).First();
                return Json(User);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        /// <returns>Данный метод преднозначен для авторизации пользователя на сайте</returns>
        /// <response code="200">Пользователь успешно зарегестрирован</response>
        /// /// /// <response code="401">Ошибка запроса, логин или пароль неверны!</response>
        /// <response code="403">Ошибка запроса, данные не указаны</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [HttpPost("Register")]
        public ActionResult Register([FromForm] string Login, [FromForm] string Password)
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
                return StatusCode(400, "Логин и пароль неверны!");

            try
            {
                using (var context = new UsersContext())
                {
                    if (context.Users.Any(x => x.Login == Login))
                    {
                        return StatusCode(400, "Логин уже используется");
                    }

                    Users newUser = new Users
                    {
                        Login = Login,
                        Password = Password
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges();

                    return Ok(newUser);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
