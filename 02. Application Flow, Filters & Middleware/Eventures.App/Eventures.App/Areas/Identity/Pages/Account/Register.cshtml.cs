namespace Eventures.App.Areas.Identity.Pages.Account
{
    using Eventures.Domain;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<EventuresUser> _signInManager;
        private readonly UserManager<EventuresUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            UserManager<EventuresUser> userManager,
            SignInManager<EventuresUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [MinLength(3, ErrorMessage = "Invalid name! Must be at least {1} symbols.")]
            [RegularExpression(@"^([\.\*\`\-_A-Z-a-z]+)$")]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "UCN")]
            [Phone]
            [RegularExpression("[0-9]{10}")]
            public string UCN { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [MinLength(5, ErrorMessage = "The {0} must be at least {1} characters long.")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new EventuresUser
                {
                    UserName = Input.Username,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    UCN = Input.UCN
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (this._userManager.Users.Count() == 1)
                {
                    await this._userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await this._userManager.AddToRoleAsync(user, "User");
                }

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
