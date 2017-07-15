﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JFB.Business.Domain.Model
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string QAnswer { get; set; }
        public string QAnswerCode { get; set; }
        public int IsRight { get; set; }
        public DateTime CreateTime { get; set; }
        public int IsValid { get; set; }
    }
}
