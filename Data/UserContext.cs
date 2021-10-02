using Microsoft.EntityFrameworkCore;

//The only thing our DbContext does 
//is connect us to the MySQL database
//And provides a conduit between
//The User Model (Model)
//the User Table,
//And the UserController (Controller)
//The app connecting to this API is the (View)

public class UserContext : DbContext
    {
        public UserContext (DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
