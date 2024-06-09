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

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : Controller
{
    private readonly DataContext _dataContext;
    private readonly HttpClient? _httpClient;
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, DataContext dataContext)
    {
        _dataContext = dataContext;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    //public IActionResult Index()
    //{
    //    return View();
    //}

    //public IActionResult Privacy()
    //{
    //    return View();
    //}

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //public IActionResult Error()
    //{
    //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //}


    [HttpPost("Login")]
    public IActionResult Anmeldung(int UserId, string password)
    {
        if (password == "" || UserId == null)
        {
            //ViewBag.ErrorMessage = "Bitte Passwort und User ID eingeben!";
            return BadRequest("Bitte Passwort und User ID eingeben!");
            //return View("Login");
        }

        using (UnitOfWork unitOfWork = new UnitOfWork())
        {
            var userId = unitOfWork.UserRepository.GetUserById(UserId);

            if (userId == null)
            {              
                //return View("Login");
                return BadRequest("Ungültige User ID!");
            }

            Console.WriteLine(userId);
            var Password = ApiClient.Models.ApiModels.User.Hash(password, userId.Salt);
            if (UserId == userId.UserId &&
                Password == userId.Password)
            {
                
                _httpContextAccessor.HttpContext.Session.SetInt32("UserId", UserId);
                _httpContextAccessor.HttpContext.Session.SetString("UserRole", userId.Role);
                if (userId.Role == "student")
                    return RedirectToAction("GetAllProjects", "Project");
                if (userId.Role == "teacher")
                    return RedirectToAction("GetAllProjects", "Project", new { userId = userId.UserId });
            }

            //ViewBag.ErrorMessage = "Ungültige User ID oder Passwort!";
            return BadRequest();
            //return View("Login");
        }
    }

    // Die Logout Funktion funktioniert noch nicht so ganz
    //public IActionResult Logout()
    //{      
    //    HttpContext.Session.Clear();

    //    return RedirectToAction("Login", "Home");

    //}

    //public IActionResult Registrierung() { return View(); }
    //public IActionResult AddUser(int matrikelnummer, string password, string passwordRepeat, string studiengang ,string studienabschluss,int semester,
    //                             string nachname, string vorname, string userType, string email, string emailRepeat, string lehrstuhl, int mitarbeiterNr, string position,
    //                             int fakultaet, string raumnummer)
    //{
    //    if (password == null || passwordRepeat == null || email == null || emailRepeat == null || userType == null || nachname == null || vorname == null || fakultaet == null || (userType == "student" && (matrikelnummer == null || studiengang == null || studienabschluss == null || semester == null)) || (userType == "teacher" && (mitarbeiterNr == null || position == null || lehrstuhl == null || raumnummer == null)))
    //    {
    //        ViewBag.ErrorMessage = "Please fill in all fields";
    //        return View("Registrierung");
    //    }

    //    if (password.Length < 8)
    //    {
    //        ViewBag.ErrorMessage = "The password must be at least 8 characters long";
    //        return View("Registrierung");
    //    }

    //    if(password != passwordRepeat)
    //    {
    //        ViewBag.ErrorMessage = "The repeated password is wrong";
    //        return View("Registrierung");
    //    }

    //    if(email != emailRepeat)
    //    {
    //        ViewBag.ErrorMessage = "The repeated email is wrong";
    //        return View("Registrierung");
    //    }

    //    int userId = 0;

    //    using (UnitOfWork unitOfWork = new()) 
    //    { 
    //        if(userType == "student") 
    //        { 
    //            userId = matrikelnummer;
    //        }
    //        else if(userType == "teacher") 
    //        { 
    //            userId = mitarbeiterNr;
    //        }

    //        User addUser = new User(nachname + "," + vorname, userId, password, email, userType); 
    //        IUserRepository user = unitOfWork.UserRepository; 
    //        user.CreateUser(addUser);

    //        if (userType == "student") 
    //        { 
    //            Student addStudent = new Student(matrikelnummer, studiengang, studienabschluss, semester, nachname, vorname, fakultaet); 
    //            IStudentRepository student = unitOfWork.StudentRepository; 
    //            student.CreateStudent(addStudent);
    //        }
    //        else if(userType == "teacher") 
    //        { 
    //            userId = mitarbeiterNr; 
    //            Supervisor addLehrender = new Supervisor(mitarbeiterNr, position, lehrstuhl, nachname, vorname, fakultaet, raumnummer); 
    //            ILehrenderRepository lehrender = unitOfWork.LehrenderRepository; 
    //            lehrender.CreateSupervisor(addLehrender);
    //        } 
    //        unitOfWork.Save(); 
    //        return View("Login");
    //    }

    //}

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


    //public IActionResult ForgotPassword()
    //{
    //    return View("ForgotPassword");
    //}
}
