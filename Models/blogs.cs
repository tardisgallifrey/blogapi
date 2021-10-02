using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog
{
    //A blog entry class/Table

    public class BlogEntry
    {
        //ID is autoset by the DB
        public long BlogEntryId { get; set; }
        public string blogTitle { get; set; }

        //These are used because of conflicts
        //between standard EF Core functions
        //that follow sqlserver instead of MySql
        [Column(TypeName = "DATETIME")]
        public DateTime blogDate { get; set; }

        [Column(TypeName = "TEXT(65500)")]
        public string blogText { get; set; }
        
        //Connects the post to a User
        public long blogUserId { get; set; }
    }

    
}