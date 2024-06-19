using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ApiClient.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Database;
using ApiClient.Models.ApiModels;
using WebAPI.Database.Repository;

namespace WebAPI.UserServices;

public class ClientService : IClientService
{
    private readonly DataContext appDbContext;
    private readonly IConfiguration config;
    public ClientService(DataContext appDbContext, IConfiguration config)
    {
        this.appDbContext = appDbContext;
        this.config = config;
    }

    public ServiceResponse RegisterUser(RegistrationModel model)
    {
        string userType = model.UserType;
        string password = model.Password;
        string passwordRepeat = model.PasswordRepeat;
        string email = model.Email;
        string emailRepeat = model.EmailRepeat;

        if (model.Password == null || model.PasswordRepeat == null || model.Email == null ||
            model.EmailRepeat == null || model.UserType == null || model.Nachname == null ||
            model.Vorname == null || model.Fakultaet == null ||
            (model.UserType == "student" && (model.Matrikelnummer == null || model.Studiengang == null ||
            model.Studienabschluss == null || model.Semester == null)) ||
            (model.UserType == "teacher" && (model.MitarbeiterNr == null || model.Position == null ||
            model.Lehrstuhl == null || model.Raumnummer == null)))
        {
            return new ServiceResponse() { Flag = false, Message = "Bitte füllen Sie alle Felder aus!" };
        }

        if (password.Length < 8)
        {
            return new ServiceResponse() { Flag = false, Message = "Das Passwort muss mindestens 8 Zeichen lang sein!" };
        }

        if (password != passwordRepeat)
        {
            return new ServiceResponse() { Flag = false, Message = "Das wiederholte Passwort ist falsch!" };
        }

        if (email != emailRepeat)
        {
            return new ServiceResponse() { Flag = false, Message = "Die wiederholte E-Mail ist falsch!" };
        }

        int userId = 0;

        using (UnitOfWork unitOfWork = new())
        {
            if (userType == "student")
            {
                userId = model.Matrikelnummer;
            }
            else if (userType == "teacher")
            {
                userId = model.MitarbeiterNr;
            }

            User addUser = new User(model.Nachname + "," + model.Vorname, userId, password, email, userType);
            IUserRepository user = unitOfWork.UserRepository;
            user.CreateUser(addUser);

            if (userType == "student")
            {
                Student addStudent = new Student(model.Matrikelnummer, model.Studiengang, model.Studienabschluss, model.Semester, model.Nachname, model.Vorname, model.Fakultaet);
                IStudentRepository student = unitOfWork.StudentRepository;
                student.CreateStudent(addStudent);
            }
            else if (userType == "teacher")
            {
                userId = model.MitarbeiterNr;
                Supervisor addLehrender = new Supervisor(model.MitarbeiterNr, model.Position, model.Lehrstuhl, model.Nachname, model.Vorname, model.Fakultaet, model.Raumnummer);
                ILehrenderRepository lehrender = unitOfWork.LehrenderRepository;
                lehrender.CreateSupervisor(addLehrender);
            }
            unitOfWork.Save();
            return new ServiceResponse() { Flag = true, Message = "Benutzer erfolgreich registriert!" };
        }
    }

    public async Task<ServiceResponse> LoginUserAsync(HomeModel model)
    {
        using (UnitOfWork unitOfWork = new UnitOfWork())
        {
            int UserId = model.UserId;
            string password = model.Password;

            var getUser = unitOfWork.UserRepository.GetUserById(model.UserId);
            if (getUser == null) return new ServiceResponse() { Flag = false, Message = "Ungültige User ID!" };

            var Password = User.Hash(password, getUser.Salt);

            if (UserId == getUser.UserId &&
                Password == getUser.Password)
            {
                string getUserRole = getUser.Role;

                // Generate token with the role, and userId.
                var token = GenerateToken(getUser.UserId, getUserRole);

                var checkIfTokenExist = await appDbContext.TokenInfo.Where(_ => _.UserId == getUser.UserId).FirstOrDefaultAsync();
                if (checkIfTokenExist == null)
                {
                    appDbContext.TokenInfo.Add(new TokenInfo() { Token = token, UserId = getUser.UserId });
                    await appDbContext.SaveChangesAsync();
                    return new ServiceResponse() { Flag = true, Token = token };
                }
                checkIfTokenExist.Token = token;
                await appDbContext.SaveChangesAsync();
                return new ServiceResponse() { Flag = true, Token = token };
            }
        }
        return new ServiceResponse() { Flag = false, Message = "Ungültige User ID oder Passwort!" };
    }

    private string GenerateToken(int id, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Role, role)
        };
        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
