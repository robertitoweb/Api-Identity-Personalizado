using Microsoft.AspNetCore.Identity;

namespace DemoIdentity.Identity;

public class MyRol: IdentityRole
{
    public string Seccion { get; set; }
    public DateTime Fechalta { get; set; }

}
