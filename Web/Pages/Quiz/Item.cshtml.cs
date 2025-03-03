using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01.Pages
{
    
    public class QuizModel : PageModel
    {
        private readonly IQuizUserService _userService;

        private readonly ILogger<QuizModel> _logger;
        public QuizModel(IQuizUserService userService, ILogger<QuizModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [BindProperty]
        public string? Question { get; set; }
        [BindProperty]
        public List<string>? Answers { get; set; }
        
        [BindProperty]
        public string? UserAnswer { get; set; }
        
        [BindProperty]
        public int QuizId { get; set; }
        
        [BindProperty]
        public int ItemId { get; set; }
        
        [BindProperty]
        public int UserId { get; set; } = 1;
        
        public void OnGet(int quizId, int itemId)
        {
            QuizId = quizId;
            ItemId = itemId;
            var quiz = _userService.FindQuizById(quizId);
            
            if (quiz != null && itemId <= quiz.Items.Count)
            {
                var quizItem = quiz.Items[itemId - 1];
                Question = quizItem.Question;
                Answers = new List<string>();
                Answers.AddRange(quizItem.IncorrectAnswers);
                Answers.Add(quizItem.CorrectAnswer);
            }
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(UserAnswer))
            {
                _userService.SaveUserAnswerForQuiz(QuizId, UserId, ItemId, UserAnswer);
            }
            
            var quiz = _userService.FindQuizById(QuizId);
            
            // Check if this is the last question
            if (quiz != null && ItemId >= quiz.Items.Count)
            {
                // If it's the last question, redirect to the summary page
                return RedirectToPage("Summary", new { quizId = QuizId });
            }
            
            // Otherwise, go to the next question
            return RedirectToPage("Item", new { quizId = QuizId, itemId = ItemId + 1 });
        }
    }
}
