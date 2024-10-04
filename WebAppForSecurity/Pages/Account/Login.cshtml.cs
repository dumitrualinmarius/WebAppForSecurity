using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebAppForSecurity.Authorization;

namespace WebAppForSecurity.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; } = new Credential();
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            //Verify credentials
            if(Credential.UserName =="admin" && Credential.Password =="password")
            {
                //Create security context
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@mywebsite.com"),
                    new Claim("Department", "HR"),
                    new Claim("Admin", "true"),
                    new Claim("Manager", "true"),
                    new Claim("EmploymentDate", "2023-05-01"),
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                var authenticationProperties = new AuthenticationProperties
                {
                    IsPersistent = Credential.RememberMe,
                };

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authenticationProperties);

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }    
}
