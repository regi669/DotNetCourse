namespace DotNetCourseNew.Configuration;

public class AuthConfig
{
    public string JWTKey { get; set; }
    
    public int JWTExpireDays { get; set; }
    
    public string JWTIssuer { get; set; }
}