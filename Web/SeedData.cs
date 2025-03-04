using ApplicationCore.Interfaces.Repository;
using BackendLab01;

namespace Infrastructure.Memory;
public static class SeedData
{
    public static void Seed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            var quizRepo = provider.GetService<IGenericRepository<Quiz, int>>();
            var quizItemRepo = provider.GetService<IGenericRepository<QuizItem, int>>();
            
            if (quizRepo != null && quizItemRepo != null)
            {
                // Quiz o zwierzętach
                var zwierzetaQuestion1 = new QuizItem(
                    1,
                    "Co miałczy?", 
                    new List<string> { "Pies", "Krowa", "Ryba" }, 
                    "Kot");
                
                var zwierzetaQuestion2 = new QuizItem(
                    2,
                    "Co szczeka?", 
                    new List<string> { "Krowa", "Kot", "Koń" }, 
                    "Pies");
                
                var zwierzetaQuestion3 = new QuizItem(
                    3,
                    "Co muczy?", 
                    new List<string> { "Pies", "Kot", "Koń" }, 
                    "Krowa");
                
                quizItemRepo.Add(zwierzetaQuestion1);
                quizItemRepo.Add(zwierzetaQuestion2);
                quizItemRepo.Add(zwierzetaQuestion3);
                
                var zwierzetaQuiz = new Quiz(
                    1,
                    new List<QuizItem> { zwierzetaQuestion1, zwierzetaQuestion2, zwierzetaQuestion3 },
                    "Quiz o zwierzętach");
                
                quizRepo.Add(zwierzetaQuiz);
                
                // Quiz historyczny
                var historiaQuestion1 = new QuizItem(
                    4,
                    "Kiedy skończyła się II wojna światowa?", 
                    new List<string> { "1944", "1946", "1950" }, 
                    "1945");
                
                var historiaQuestion2 = new QuizItem(
                    5,
                    "Kto był pierwszym człowiekiem na Księżycu?", 
                    new List<string> { "Buzz Aldrin", "Jurij Gagarin", "John Glenn" }, 
                    "Neil Armstrong");
                
                var historiaQuestion3 = new QuizItem(
                    6,
                    "Która starożytna budowla znajdowała się w Aleksandrii?", 
                    new List<string> { "Wiszące ogrody", "Kolos", "Świątynia Artemidy" }, 
                    "Latarnia morska");
                
                quizItemRepo.Add(historiaQuestion1);
                quizItemRepo.Add(historiaQuestion2);
                quizItemRepo.Add(historiaQuestion3);
                
                var historiaQuiz = new Quiz(
                    2,
                    new List<QuizItem> { historiaQuestion1, historiaQuestion2, historiaQuestion3 },
                    "Quiz historyczny");
                
                quizRepo.Add(historiaQuiz);
            }
        }
    }
}