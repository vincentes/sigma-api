﻿using System;
using System.Collections.Generic;

namespace AdminTools.Models
{
    public partial class Posts
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
    }
}
