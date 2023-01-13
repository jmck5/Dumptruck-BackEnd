using Dumptruck_v4.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dumptruck_v4 {

    public class ScoobyContext :  IdentityDbContext {

        public ScoobyContext(DbContextOptions<ScoobyContext> options):base(options){
        // The ScoobyContext class or any context class takes options when it is created.
        // This will be in the startup or DI or similar, and that is where we will determine what type of database and the connection string
        // e.g. services.AddDbContext<ScoobyDoobyDoo>(options => options.UseSqlServer(_config["DefaultConnectionString"]); where services is the IserviceCollection passed into ConfigureServices
        }
        public DbSet<Note> Notes { get; set; }
        public DbSet<ScoobyUser> ScoobyUsers { get; set; }
    }
}
