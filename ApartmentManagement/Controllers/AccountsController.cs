using ApartmentManagement.Application.ViewModels;
using ApartmentManagement.Data.EF;
using ApartmentManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.WebUtilities;

namespace ApartmentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApartmentManagementDbContext _context;
        private readonly UserManager<Account> _userManager;


        public AccountsController(ApartmentManagementDbContext context, UserManager<Account> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpPost("register")]
        public async Task<ActionResult> PostAccount(RegisterAccountViewModel registerAccount)
        {
            var account = _context.Accounts.Where(x => x.UserName == registerAccount.UserName).ToList();

            if (0 != account.Count)
                return BadRequest("Tài khoản đã tồn tại!");

            if (registerAccount.Password != registerAccount.ConfirmPassword)
                return BadRequest("Mật khẩu xác nhận không trùng khớp!");

            var email = _context.Accounts.Where(x => x.Email == registerAccount.Email).ToList();

            if (0 != email.Count)
                return BadRequest("Email đã được sử dụng!");

            // một vài điều kiện nữa...

            var newAccount = new Account()
            {
                UserName = registerAccount.UserName,
                Email = registerAccount.Email,
                PhoneNumber = registerAccount.Phone
            };

            var result = await _userManager.CreateAsync(newAccount, registerAccount.Password);

            var response = new ResultOfRegisterAccount();
            if (result.Succeeded)
            {
                var user = _context.Accounts.FirstOrDefault(x => x.UserName == newAccount.UserName);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code = code },
                    protocol: Request.Scheme);

                response.UserName = user.UserName;
                response.UrlConfirmEmail = callbackUrl;
            }
            else
            {
                return BadRequest("Lỗi không đúng định dạng mật khẩu!");
            }

            await _context.SaveChangesAsync();

            return Ok(response);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Account>> DeleteAccount(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return account;
        }

        private bool AccountExists(Guid id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.password))
            {

                var permissions = (from a in _context.Accounts
                                   join p in _context.Permissions on a.Id equals p.AccountId
                                   join f in _context.Functions on p.FunctionId equals f.Id
                                   where user.Id == a.Id
                                   select new
                                   {
                                       f.Name
                                   }).ToList();

                string result = "[";

                for (int i = 0; i < permissions.Count; i++)
                {
                    if (i == permissions.Count - 1)
                        result += permissions[i].Name + "]";
                    else
                        result += permissions[i].Name + ", ";
                }



                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("AccountId", user.Id.ToString()),
                        new Claim("UserName", user.UserName),
                        new Claim("Email", user.Email),
                        new Claim("Phone", user.PhoneNumber),
                        new Claim("Permission",result)
                    }),

                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890ABCDEF")), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }
    }
}
