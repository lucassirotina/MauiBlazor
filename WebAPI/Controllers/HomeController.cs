using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAPI.Database;
using WebAPI.Database.Repository;
using WebAPI.Models;
using WebAPI.Controllers;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpLogging;
using ApiClient.Models.ApiModels;
using System.Net.Http;
using ApiClient.Models;
using WebAPI.UserServices;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : Controller
{
    private readonly IClientService _service;

    public HomeController(IClientService service)
    {
        _service = service;
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ActionResult<ServiceResponse>> LoginUserAsync(HomeModel homeModel)
    {      
        if (homeModel is null) return BadRequest();
        var result = await _service.LoginUserAsync(homeModel);
        return Ok(result);       
        
    }

    //public IActionResult Logout()
    //{      
    //    HttpContext.Session.Clear();

    //    return RedirectToAction("Login", "Home");

    //}

    [HttpPost("Register")]
    [AllowAnonymous]
    public ActionResult<ServiceResponse> RegisterUser(RegistrationModel model)
    {
        if (model is null) return BadRequest("Alle Felder sind leer.");
        var result = _service.RegisterUser(model);
        return Ok(result);
    }
}

//public IActionResult EmailVerification(string email)
//{
//    using (UnitOfWork unitOfWork = new UnitOfWork())
//    {
//        if (!unitOfWork.UserRepository.EmailExists(email))
//        {
//            ViewBag.ErrorMessage = "Email not found. Please give a valid email address.";
//            return View("ForgotPassword");
//        }

//        var user = unitOfWork.UserRepository.GetUserByEmail(email);
//        string subject = "Email Verification";
//        string code = VerificationCodeService.GenerateVerificationCode(user.UserId);
//        string body = "Please enter the following code. The code is valid for 10 minutes: " + code;

//        // Übermittlung des Bestätigungscodes an die Benutzer-E-Mail
//        EmailSender.SendEmail(user.Email,  subject, body);
//        ViewBag.SuccessMessage = "The email has been sent successfully. Please check your email and enter the verification code below with your new password.";
//        return View("EnterNewPassword", new HomeModel(user.UserName, user.UserId));
//    }
//}

//public IActionResult CheckNewPassword(string newPassword, string confirmedNewPassword, int userId, string verificationCode)
//{
//    using (UnitOfWork unitOfWork = new UnitOfWork())
//    {
//        var user = unitOfWork.UserRepository.GetUserById(userId);

//        // Überprüfen, ob der Bestätigungscode korrekt ist und ob die eingegebenen Passwörter übereinstimmen
//        if (verificationCode != null && VerificationCodeService.IsVerificationCodeValid(userId, verificationCode))
//        {
//            if (newPassword == confirmedNewPassword)
//            {
//                if (newPassword.Length < 8)
//                {
//                    ViewBag.ErrorMessage = "The password must be at least 8 characters long.";
//                    return View("EnterNewPassword", new HomeModel(user.UserName, user.UserId));
//                }
//                user.Password = WebAPI.Database.User.Hash(newPassword, user.Salt);
//                unitOfWork.Save();
//                ViewBag.SuccessMessage = "Password changed successfully.";
//                return View("Login");
//            }

//            ViewBag.ErrorMessage = "Passwords do not match. Please try again."; 
//            return View("EnterNewPassword", new HomeModel(user.UserName, user.UserId));
//        }

//        ViewBag.ErrorMessage = "Invalid verification code."; 
//        return View("EnterNewPassword", new HomeModel(user.UserName, user.UserId));
//    }
//}
