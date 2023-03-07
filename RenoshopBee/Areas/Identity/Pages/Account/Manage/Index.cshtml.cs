// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RenoshopBee.Data;
using RenoshopBee.Models;

namespace RenoshopBee.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;            _webHostEnvironment = webHostEnvironment;

        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

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
        public ApplicationUser Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel : ApplicationUser
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            //var userinit = await _userManager.GetUserAsync(User);
            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            //Username = userName;
             Input = await _userManager.GetUserAsync(User);
            Address address = await _context.Address.Where(ad => ad.ApplicationUserId == Input.Id).FirstOrDefaultAsync();
            Input.address = address;
            //Input = new InputModel
            //{
            //    PhoneNumber = phoneNumber
            //};
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            //var Input = await _userManager.GetUserAsync(User);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile? ImgFile)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (ModelState.HasReachedMaxErrors)
            {
                await LoadAsync(user);
                return Page();
            }
            if (ImgFile != null)
            {
                if (user.Img_Url != "\\images\\No_Image.png")
                {
                    var OldImg = _webHostEnvironment + user.Img_Url;
                    if (System.IO.File.Exists(OldImg))
                    {
                        System.IO.File.Delete(OldImg);
                    }
                }
                string imgExtension = Path.GetExtension(ImgFile.FileName);
                Guid imgGuid = Guid.NewGuid();
                string imgName = imgGuid + imgExtension;
                string imgUrl = "\\images\\" + imgName;
                user.Img_Url = imgUrl;

                string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                ImgFile.CopyTo(imgStream);
                imgStream.Dispose();
            }
            else
            {
                var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.PhoneNumber = Input.PhoneNumber;
            }
                var Result = await _userManager.UpdateAsync(user);
                //var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!Result.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }         

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
