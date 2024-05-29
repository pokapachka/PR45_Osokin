using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using ПР45_Осокин.Context;
using ПР45_Осокин.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ПР45_Осокин.Controllers
{
    [Route("api/TasksController")]

    [ApiExplorerSettings(GroupName = "v1")]
    public class TasksController : Controller
    {
        /// <summary>
        /// Получение списка задач
        /// </summary>
        /// <remarks>Данный метод получает список задач, находящийся в базе данных</remarks>
        /// <response code="200">Список успешно получен</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("List")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Tasks>), 200)]
        [ProducesResponseType(500)]
        public ActionResult List()
        {
            try
            {
                IEnumerable<Tasks> Tasks = new TaskContext().Tasks;
                return Json(Tasks);
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }
        /// <summary>
        /// Получение задачи
        /// </summary>
        /// <remarks>Данный метод получает задачу, находящуюся в базе данных</remarks>
        /// <response code="200">Задача успешно получена</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("Item")]
        [HttpGet]
        [ProducesResponseType(typeof(Tasks), 200)]
        [ProducesResponseType(500)]
        public ActionResult Item(int id)
        {
            try
            {
                Tasks Task = new TaskContext().Tasks.First(x => x.Id == id);
                return Json(Task);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Метод добавления задачи
        /// </summary>
        /// <param name="task">Данные о задаче</param>
        /// <returns>Статус выполнения задачи</returns>
        /// <remarks>Данный метод добавляет задачу, находящуюся в базе данных</remarks>
        [Route("Add")]
        [HttpPut]
        [ApiExplorerSettings(GroupName = "v3")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]

        public ActionResult Add([FromForm] Tasks task)
        {
            try
            {
                TaskContext taskContext = new TaskContext();
                taskContext.Tasks.Add(task);
                taskContext.SaveChanges();
                return StatusCode(200);
            }
            catch (Exception exp)
            {
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Метод изменения задачи
        /// </summary>
        /// <param name="task">Данные о задаче</param>
        /// <returns>Статус выполнения задачи</returns>
        /// <remarks>Данный метод изменяет задачу, находящуюся в базе данных</remarks>
        [Route("Update")]
        [HttpPut]
        [ApiExplorerSettings(GroupName = "v3")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult Update([FromForm] Tasks task)
        {
            try
            {
                using (var taskContext = new TaskContext())
                {
                    var existingTask = taskContext.Tasks.FirstOrDefault(x => x.Id == task.Id);
                    if (existingTask == null)
                    {
                        return StatusCode(404, "Задачи не существует");
                    }

                    existingTask.Name = task.Name;
                    existingTask.Priority = task.Priority;
                    existingTask.DateExecute = task.DateExecute;
                    existingTask.Comment = task.Comment;
                    existingTask.Done = task.Done;

                    taskContext.SaveChanges();
                    return StatusCode(200);
                }
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }
        /// <summary>
        /// Метод удаления задачи
        /// </summary>
        /// <param name="id">ID задачи</param>
        /// <returns>Статус выполнения задачи</returns>
        /// <remarks>Данный метод удаляет задачу, находящуюся в базе данных</remarks>
        [Route("Delete/{id}")]
        [HttpDelete]
        [ApiExplorerSettings(GroupName = "v4")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult Delete(int id)
        {
            try
            {
                using (var taskContext = new TaskContext())
                {
                    var existingTask = taskContext.Tasks.FirstOrDefault(x => x.Id == id);
                    if (existingTask == null)
                    {
                        return NotFound("Задачи не существует, проверьте запрос и повторите попытку.");
                    }
                    taskContext.Tasks.Remove(existingTask);
                    taskContext.SaveChanges();
                    return Ok("Успешное удаление.");
                }
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }

        /// <summary>
        /// Метод удаления всей таблицы
        /// </summary>
        /// <param name="id">ID задачи</param>
        /// <returns>Статус выполнения задачи</returns>
        /// <remarks>Данный метод удаляет задачу, находящуюся в базе данных</remarks>
        [Route("DeleteAll")]
        [HttpDelete]
        [ApiExplorerSettings(GroupName = "v4")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult DeleteAll()
        {
            try
            {
                using (var taskContext = new TaskContext())
                {
                    taskContext.Database.ExecuteSqlRaw("TRUNCATE TABLE Tasks;");
                    return Ok("Все задачи выполнены успешно.");
                }
            }
            catch (Exception exp)
            {
                return StatusCode(500, exp.Message);
            }
        }
    }
}
