using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TestApplication.config
{
    public class QuizJSONReader
    {
        public List<QuizCategory> Categories { get;  set; }

        public async Task ReadJSON()
        {
            using (StreamReader sr = new StreamReader("Quizes.json"))
            {
                string json = await sr.ReadToEndAsync();
                QuizData data = JsonConvert.DeserializeObject<QuizData>(json);
                this.Categories = data.Quizzes;
            }
        }
    }

    public class QuizData
    {
        [JsonProperty("quizzes")]
        public List<QuizCategory> Quizzes { get; set; }
    }

    public class QuizCategory
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("questions")]
        public List<Question> Questions { get; set; }
    }

    public class Question
    {
        [JsonProperty("question")]
        public string Text { get; set; }

        [JsonProperty("options")]
        public List<string> Options { get; set; }

        [JsonProperty("answer")]
        public string Answer { get; set; }
    }
}
