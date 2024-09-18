using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppForSecurity.Pages
{
    [Authorize(Policy = "MustBelongToHRDeparment")]
    public class HumanResourceModel : PageModel
    {
        private readonly ILogger<HumanResourceModel> _logger;

        public HumanResourceModel(ILogger<HumanResourceModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
