using System;

namespace blog
{
    public class BlogEntry
    {
        public long BlogEntryId { get; set; }
        public string blogTitle { get; set; }
        public DateTime blogDate { get; set; }
        public string blogText { get; set; }
        public long blogUserId { get; set; }
    }

    
}