using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.config;

namespace TestApplication.Other
{
    public class QuizSystem
    {

        public QuizCategory Category { get; private set; }
        // Use this async method to initialize the QuizSystem

        public QuizSystem()
        {
            
        }
        public  async Task<QuizSystem> CreateAsync()
        {
            var quizSystem = new QuizSystem();
            await quizSystem.InitializeAsync();
            return quizSystem;
        }

        public async Task InitializeAsync()
        {
            var quizJSONReader = new QuizJSONReader();
            await quizJSONReader.ReadJSON(); // Asynchronously read the quiz data

            Random random = new Random();
            int randomIndex = random.Next(quizJSONReader.Categories.Count);

            // Set the random category
            this.Category = quizJSONReader.Categories[randomIndex];
        }


    }
}
