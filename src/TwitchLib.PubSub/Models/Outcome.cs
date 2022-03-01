using System;
using System.Collections.Generic;

namespace TwitchLib.PubSub.Models
{
    public class Outcome
    {
        public Guid Id { get; set; }
        public string Color { get; set; }
        public string Title { get; set; }
        public long TotalPoints { get; set; }
        public long TotalUsers { get; set; }
        public ICollection<Predictor> TopPredictors { get; set; } = new List<Predictor>();

        public class Predictor
        {
            public long Points { get; set; }
            public string UserId { get; set; }
            public string DisplayName { get; set; }
        }
    }
}
