using System.Security.Claims;
using Mailing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.DataContext;
using RentACar.DataContext.Entities;
using RentACar.Models;

namespace RentACar.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMailService _mailService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly AppDbContext _context;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMailService mailService, AppDbContext context = null)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mailService = mailService;
        _context = context;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var user = new AppUser
        {
            UserName = registerViewModel.Username,
            Email = registerViewModel.Email,
            FullName = registerViewModel.FullName,
        };

        var result = await _userManager.CreateAsync(user, registerViewModel.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }

        var emailConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var emailConfirmLink = Url.Action("ConfirmEmail", "Account", new { emailConfirmToken, user.UserName }, Request.Scheme, Request.Host.ToString());

        var htmlBody = $@"
          <html>
            <body style=""font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f0f2f5; padding: 40px 0; margin: 0;"">
              <div style=""max-width: 580px; margin: auto; background: #fff; border-radius: 12px; box-shadow: 0 8px 20px rgba(0,0,0,0.1); padding: 35px;"">
      
                   <h1 style=""color: #222; font-weight: 700; font-size: 28px; text-align: center; margin-bottom: 20px;"">
                      Email ünvanınızı təsdiqləyin
                   </h1>
      
               <p style=""font-size: 17px; color: #444; line-height: 1.6; margin-bottom: 25px;"">
                  Salam,<br />
                  Email ünvanınızı təsdiqləmək üçün aşağıdakı düyməyə klikləyin.
               </p>
      
             <div style=""text-align: center; margin-bottom: 40px;"">
                  <a href=""{emailConfirmLink}"" 
                    style=""background: linear-gradient(90deg, #6a11cb 0%, #2575fc 100%);
                  color: #fff;
                  font-weight: 600;
                  padding: 14px 50px;
                  border-radius: 50px;
                  text-decoration: none;
                  font-size: 18px;
                  box-shadow: 0 6px 15px rgba(101, 41, 255, 0.4);
                  display: inline-block;
                  transition: background 0.3s ease;"">
          Email ünvanınızı təsdiqləyin
         </a>
         </div>
      
          <p style=""font-size: 14px; color: #888; margin-bottom: 30px; text-align: center;"">
          Əgər bu sorğunu siz göndərməmisinizsə, bu emaili nəzərə almayın.
          </p>
      
          <hr style=""border: none; border-top: 1px solid #eee; margin-bottom: 30px;"" />
      
         <div style=""text-align: center;"">
        <a href=""https://facebook.com/yourpage"" style=""margin: 0 8px; text-decoration: none;"">
          <img src=""https://cdn-icons-png.flaticon.com/24/733/733547.png"" alt=""Facebook"" />
        </a>
        <a href=""https://twitter.com/yourpage"" style=""margin: 0 8px; text-decoration: none;"">
          <img src=""https://cdn-icons-png.flaticon.com/24/733/733579.png"" alt=""Twitter"" />
        </a>
        <a href=""https://instagram.com/yourpage"" style=""margin: 0 8px; text-decoration: none;"">
          <img src=""https://cdn-icons-png.flaticon.com/24/733/733558.png"" alt=""Instagram"" />
        </a>
      </div>
      
      <p style=""font-size: 13px; color: #bbb; text-align: center; margin-top: 30px;"">
        © 2025 Rentaly. Bütün hüquqlar qorunur.
      </p>
      
    </div>
  </body>
</html>";


        try
        {
            _mailService.SendMail(new Mail
            {
                ToEmail = user.Email,
                Subject = "Email confirmation",
                HtmlBody = htmlBody
            });
        }
        catch (Exception ex)
        {
            // Log error (optional): _logger.LogError(ex, "Email send failed");
            TempData["EmailError"] = "Email could not be sent. Please try again later.";
            // Optionally show message or continue without breaking
        }

        return RedirectToAction(nameof(Login));
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Username or password is incorrect");
            return View();
        }

        var existUser = await _userManager.FindByNameAsync(loginViewModel.UserName);

        if (existUser == null)
        {
            ModelState.AddModelError("", "Username or password is incorrect");
            return View();
        }

        var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(existUser);
        if (!isEmailConfirmed)
        {
            ModelState.AddModelError("", "Email not confirmed");
            return View();
        }

        var result = await _signInManager.PasswordSignInAsync(
            existUser,
            loginViewModel.Password,
            loginViewModel.RememberMe,
            lockoutOnFailure: true
        );

        if (result.IsLockedOut)
        {
            ModelState.AddModelError("", $"You are banned {(existUser.LockoutEnd.Value - DateTimeOffset.UtcNow).Minutes} minutes left.");
            return View();
        }

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Username or password is incorrect");
            return View();
        }

        if (!string.IsNullOrEmpty(loginViewModel.ReturnUrl) && Url.IsLocalUrl(loginViewModel.ReturnUrl))
        {
            return Redirect(loginViewModel.ReturnUrl);
        }

        return RedirectToAction("Dashboard", "Account");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction(nameof(Login));
    }

    // [HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Logout()
    //{
    //    await _signInManager.SignOutAsync();
    //    return RedirectToAction("Login", "Account");
    //}

    public IActionResult AccessDenied()
    {
        return View();
    }

    [Authorize]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }
        var result = await _userManager.ChangePasswordAsync(user, changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }

        return RedirectToAction(nameof(Login));
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        if (string.IsNullOrEmpty(email))
            return BadRequest();

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            ModelState.AddModelError("", "Email doesnt found");
            return View();
        }

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        var resetLink = Url.Action("ResetPassword", "Account", new { resetToken, email }, Request.Scheme, Request.Host.ToString());

        var htmlBody = $@"
<html>
  <body style=""font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f0f2f5; padding: 40px 0; margin: 0;"">
    <div style=""max-width: 580px; margin: auto; background: #fff; border-radius: 12px; box-shadow: 0 8px 20px rgba(0,0,0,0.1); padding: 35px;"">
      
      <h1 style=""color: #222; font-weight: 700; font-size: 28px; text-align: center; margin-bottom: 20px;"">
        Parolunuzu sıfırlayın
      </h1>
      
      <p style=""font-size: 17px; color: #444; line-height: 1.6; margin-bottom: 25px;"">
        Salam,<br />
        Parolunuzu yeniləmək üçün aşağıdakı düyməyə klikləyin.
      </p>
      
      <div style=""text-align: center; margin-bottom: 40px;"">
        <a href=""{resetLink}"" 
           style=""background: linear-gradient(90deg, #6a11cb 0%, #2575fc 100%);
                  color: #fff;
                  font-weight: 600;
                  padding: 14px 50px;
                  border-radius: 50px;
                  text-decoration: none;
                  font-size: 18px;
                  box-shadow: 0 6px 15px rgba(101, 41, 255, 0.4);
                  display: inline-block;
                  transition: background 0.3s ease;"">
          Şifrəni sıfırla
        </a>
      </div>
      
      <p style=""font-size: 14px; color: #888; margin-bottom: 30px; text-align: center;"">
        Əgər bu sorğunu siz göndərməmisinizsə, bu emaili nəzərə almayın.
      </p>
      
      <hr style=""border: none; border-top: 1px solid #eee; margin-bottom: 30px;"" />
      
      <div style=""text-align: center;"">
        <a href=""https://facebook.com/yourpage"" style=""margin: 0 8px; text-decoration: none;"">
          <img src=""https://cdn-icons-png.flaticon.com/24/733/733547.png"" alt=""Facebook"" />
        </a>
        <a href=""https://twitter.com/yourpage"" style=""margin: 0 8px; text-decoration: none;"">
          <img src=""https://cdn-icons-png.flaticon.com/24/733/733579.png"" alt=""Twitter"" />
        </a>
        <a href=""https://instagram.com/yourpage"" style=""margin: 0 8px; text-decoration: none;"">
          <img src=""https://cdn-icons-png.flaticon.com/24/733/733558.png"" alt=""Instagram"" />
        </a>
      </div>
      
      <p style=""font-size: 13px; color: #bbb; text-align: center; margin-top: 30px;"">
        © 2025 Rentaly. Bütün hüquqlar qorunur.
      </p>
      
    </div>
  </body>
</html>";

        _mailService.SendMail(new Mail
        {
            ToEmail = email,
            Subject = "Şifrə sıfırlama linki",
            HtmlBody = htmlBody
        });

        return View(nameof(EmailSimulyasiya), resetLink);
    }

    public IActionResult EmailSimulyasiya()
    {
        return View();
    }

    public IActionResult ResetPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(resetPasswordViewModel);
        }

        var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);

        if (user == null)
            return BadRequest();

        var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.ResetToken, resetPasswordViewModel.NewPassword);

        if (!result.Succeeded)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(resetPasswordViewModel);
        }

        return RedirectToAction(nameof(Login));
    }


    public async Task<IActionResult> ConfirmEmail(string emailConfirmToken, string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
            return BadRequest();

        var result = await _userManager.ConfirmEmailAsync(user, emailConfirmToken);

        if (!result.Succeeded)
        {
            return BadRequest();
        }

        return RedirectToAction(nameof(Login));
    }
    [Authorize]
    public async Task<IActionResult> Dashboard()
     {
         var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var bookings = await _context.Bookings
             .Where(b => b.UserId == userId)
             .Include(b => b.Car)
             .ToListAsync();

         var viewModelList = bookings.Select(b => new UserBookingViewModel
         {
             CarName = b.Car?.Name ?? b.CarType,
             PickupLocation = b.PickupLocation,
             DropoffLocation = b.DropoffLocation,
             PickupDate = b.PickupDate,
             ReturnDate = b.ReturnDate,
             TotalPrice = b.Car != null
                 ? ((b.ReturnDate - b.PickupDate).Days) * b.Car.PricePerDay
                 : 0,
             Status = b.Status
         }).ToList();

         return View(viewModelList);
     }
}
