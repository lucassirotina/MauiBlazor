using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ApiClient.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Database;
using ApiClient.Models.ApiModels;

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
