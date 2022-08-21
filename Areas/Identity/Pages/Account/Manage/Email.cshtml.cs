﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ChernabogJailApp.Areas.Identity.Data;
using ChernabogJailApp.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace ChernabogJailApp.Areas.Identity.Pages.Account.Manage
{
    public class EmailModel : PageModel
    {
        private readonly UserManager<ChernabogJailAppUser> _userManager;
        private readonly SignInManager<ChernabogJailAppUser> _signInManager;
        private readonly IEmailService _emailService;

        public EmailModel(
            UserManager<ChernabogJailAppUser> userManager,
            SignInManager<ChernabogJailAppUser> signInManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

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
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "New email")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(ChernabogJailAppUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            Input = new InputModel
            {
                NewEmail = email,
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, email = Input.NewEmail, code = code },
                    protocol: Request.Scheme);
                var message = new EmailMessage();
                message.FromAddresses = new List<EmailAddress>() { new EmailAddress("Узилище Чернобога", "chernabog@akusov.xyz") };
                message.ToAddresses = new List<EmailAddress>() { new EmailAddress(user.UserName, Input.NewEmail) };
                message.Subject = "Подтверждение почты";
                message.Content = $"Пожалуйста, подтвердите свой аккаунт перейдя по <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>ссылке</a>.";
                await _emailService.SendAsync(message);

                StatusMessage = "Письмо подтверждения нового почтового адреса было отправлено. Пожалуйста, проверьте ваш почтовый ящик.";
                return RedirectToPage();
            }

            StatusMessage = "Ваш адрес не изменился.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
                protocol: Request.Scheme);
            var message = new EmailMessage();
            message.FromAddresses = new List<EmailAddress>() { new EmailAddress("Узилище Чернобога", "chernabog@akusov.xyz") };
            message.ToAddresses = new List<EmailAddress>() { new EmailAddress(user.UserName, email) };
            message.Subject = "Подтверждение почты";
            message.Content = $"Пожалуйста, подтвердите свой аккаунт перейдя по <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>ссылке</a>.";
            await _emailService.SendAsync(message);

            StatusMessage = "Письмо подтверждения нового почтового адреса было отправлено. Пожалуйста, проверьте ваш почтовый ящик.";
            return RedirectToPage();
        }
    }
}
