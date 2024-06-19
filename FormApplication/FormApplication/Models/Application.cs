﻿using Newtonsoft.Json;

namespace FormApplication.Models
{
    public class Application
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string programId { get; set; }
        public string CandidateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public string CurrentResidence { get; set; }
        public string IDNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public Dictionary<string, string> Responses { get; set; }
        public List<Question> Questions { get; set; }
    }
}
