
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoIdentity.Identity;

public class MyIdentiryDbContext:IdentityDbContext<MyUser,MyRol,string>    
{

    public MyIdentiryDbContext(DbContextOptions<MyIdentiryDbContext> options) : base(options)
    {

    }


}
