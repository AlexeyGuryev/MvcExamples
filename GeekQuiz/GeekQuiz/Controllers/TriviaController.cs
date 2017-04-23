using GeekQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Description;

namespace GeekQuiz.Controllers
{
    [Authorize]
    public class TriviaController : ApiController
    {
        private TriviaContext db = new TriviaContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        // GET /api/Trivia
        [ResponseType(typeof(TriviaQuestion))]
        public async Task<IHttpActionResult> Get()
        {
            var userId = User.Identity.Name;

            var nextQuestion = await NextQuestionAsync(userId);
            if (nextQuestion == null)
            {
                return NotFound();
            }
            return Ok(nextQuestion);
        }

        [ResponseType(typeof(TriviaAnswer))]
        public async Task<IHttpActionResult> Post(TriviaAnswer answer)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            answer.UserId = User.Identity.Name;

            var isCorrect = await StoreAsync(answer);
            return Ok(isCorrect);
        }

        private async Task<TriviaQuestion> NextQuestionAsync(string userId)
        {
            var lastQuestionId = await this.db.TriviaAnswers.Where(c => c.UserId == userId)
                .GroupBy(c => c.QuestionId)
                .Select(c => new { QuestionId = c.Key, Count = c.Count() })
                .OrderByDescending(c => new { c.Count, QuestionId = c.QuestionId })
                .Select(c => c.QuestionId)
                .FirstOrDefaultAsync();

            var questionsCount = await db.TriviaQuestions.CountAsync();

            var nextQuestionId = (lastQuestionId % questionsCount) + 1;
            return await db.TriviaQuestions.FindAsync(CancellationToken.None, nextQuestionId);
        }

        private async Task<bool> StoreAsync(TriviaAnswer answer)
        {
            db.TriviaAnswers.Add(answer);

            await this.db.SaveChangesAsync();
            var selectedOption = await db.TriviaOptions.FirstOrDefaultAsync(c => c.Id == answer.OptionId && c.QuestionId == answer.QuestionId);
            return selectedOption.IsCorrect;
        }
    }
}
