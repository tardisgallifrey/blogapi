//This is the data model for the USER table in the DB
//Also the data model for variables in this app

public class User
    {
        //A user ID number, auto set by the DB
        public long UserId { get; set; }

        public string UserName { get; set; }

        //First and Last name variables
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }