﻿using System;

namespace Parser.Core.Models
{
    public class TopFilterDataModel
    {
        public int N { get; set; } = 10;
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
