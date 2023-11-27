﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Academy.Core.Services.Interfaces;
using Academy.Model.Models.TicketModels;

namespace Academy.Web.Controllers
{
    public class ForumController : Controller
    {
        private IForumService _forumService;

        public ForumController(IForumService forumService)
        {
            _forumService = forumService;
        }

        public IActionResult Index(int? courseId,string filter="")
        {
            ViewBag.CourseId = courseId;
            return View(_forumService.GetQuestions(courseId, filter));
        }

        #region Create Question

        [Authorize]
        public IActionResult CreateQuestion(int id)
        {
            Question question = new Question()
            {
                CourseId = id
            };
            return View(question);
        }


        [HttpPost]
        public IActionResult CreateQuestion(Question question)
        {
            if (!ModelState.IsValid)
            {
                return View(question);
            }

            question.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            int questionId = _forumService.AddQuestion(question);
            return Redirect($"/Forum/ShowQuestion/{questionId}");
        }

        #endregion

        #region Show Question

        public IActionResult ShowQuestion(int id)
        {
            return View(_forumService.ShowQuestion(id));
        }

        #endregion

        #region Answer
        [Authorize]
        public IActionResult Answer(int id, string body)
        {
            if (!string.IsNullOrEmpty(body))
            {
                var sanitizer = new HtmlSanitizer();
                body = sanitizer.Sanitize(body);
                _forumService.AddAnswer(new Answer()
                {
                    BodyAnswer = body,
                    CreateDate = DateTime.Now,
                    UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()),
                    QuestionId = id
                });
            }
            return RedirectToAction("ShowQuestion", new {id = id});
        }

        [Authorize]
        public IActionResult SelectIsTrueAnswer(int questionId, int answerId)
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var question = _forumService.ShowQuestion(questionId);
            if (question.Question.UserId == currentUserId)
            {
                _forumService.ChangeIsTrueAnswer(questionId,answerId);
            }
            return RedirectToAction("ShowQuestion", new { id = questionId });
        }
        #endregion
    }
}
