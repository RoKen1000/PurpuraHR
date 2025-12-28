// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.Common.Enums;
using Purpura.Utility;
using Purpura.Utility.Helpers;
using PurpuraWeb.Models.Entities;

namespace PurpuraWeb.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICompanyEmployeeService _companyEmployeeService;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ICompanyEmployeeService companyEmployeeService)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _companyEmployeeService = companyEmployeeService;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string? Role { get; set; }
            public IEnumerable<SelectListItem> RoleList { get; set; }

            [Required]
            public string FirstName { get; set; }
            public string? MiddleName { get; set; }
            [Required]
            public string LastName { get; set; }

            [DataType(DataType.Date)]
            public DateTime DateOfBirth { get; set; } = DateTime.Now;
            public string Address { get; set; }
            [Required]
            public string PostCode { get; set; }
            [Required]
            public string AddressLine1 { get; set; }
            [Required]
            public string AddressLine2 { get; set; }
            [Required]
            public string AddressLine3 { get; set; }
            [Required]
            public Genders Gender { get; set; }
            public IEnumerable<SelectListItem> GenderList { get; set; }
            [Required]
            public Titles Title { get; set; }
            public IEnumerable<SelectListItem> TitleList { get; set; }

            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!_roleManager.RoleExistsAsync(UserRoles.EmployeeRole).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.EmployeeRole));
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.ManagerRole));
            }

            Input = new InputModel()
            {
                RoleList = GenerateRoleSelectList(),
                GenderList = EnumHelpers.GenerateGenderSelectList(),
                TitleList = EnumHelpers.GenerateTitleSelectList()
            };

            //ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Home/Dashboard");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            this.Input.Address = AddressHelpers.ConstructAddressString(new string[4] {this.Input.AddressLine1, this.Input.AddressLine2, this.Input.AddressLine3, this.Input.PostCode});

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.Address = this.Input.Address;
                user.Email = this.Input.Email;
                user.FirstName = this.Input.FirstName;
                user.MiddleName = this.Input.MiddleName;
                user.LastName = this.Input.LastName;
                user.Gender = this.Input.Gender;
                user.Title = this.Input.Title;
                user.PhoneNumber = this.Input.PhoneNumber;
                user.DateOfBirth = this.Input.DateOfBirth;
                user.AnnualLeaveDays = 28;
                user.DateCreated = DateTime.Now;

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var companyEmployeeLinkResult = await _companyEmployeeService.AssignUserToCompanyEmployeeAsync(user.Email);

                    if (!companyEmployeeLinkResult.IsSuccess)
                    {
                        _logger.LogWarning(companyEmployeeLinkResult.Error);
                    }
                    else
                    {
                        _logger.LogInformation("User has been matched to a pre-existing Company Employee.");
                    }

                    if (!String.IsNullOrEmpty(Input.Role))
                    {
                        await _userManager.AddToRoleAsync(user, Input.Role);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.EmployeeRole);
                    }

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form

            Input = new InputModel()
            {
                RoleList = GenerateRoleSelectList(),
                GenderList = EnumHelpers.GenerateGenderSelectList(),
                TitleList = EnumHelpers.GenerateTitleSelectList()
            };

            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }

        private IEnumerable<SelectListItem> GenerateRoleSelectList()
        {
            return _roleManager.Roles.Select(r => r.Name).Select(s => new SelectListItem
            {
                Text = s,
                Value = s
            });
        }
    }
}
