﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AuditExams.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Variant1 { get; set; }
        public string Variant2 { get; set; }
        public string Variant3 { get; set; }
        public int RightAnswer { get; set; }
        public string Course { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public List<Result> Results { get; set; }
    }
}
