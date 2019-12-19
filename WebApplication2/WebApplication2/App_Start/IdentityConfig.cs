using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using WebApplication2.Models;

namespace WebApplication2
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 電子メールを送信するための電子メール サービスをここにプラグインします。
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // テキスト メッセージを送信するための SMS サービスをここにプラグインします。
            return Task.FromResult(0);
        }
    }

    public class ApplicationUserStore : IUserStore<ApplicationUser>
    {
        public ApplicationUserStore() { }

        public Task CreateAsync(ApplicationUser user)
        {
            // 何もしない
            return Task.Delay(0);
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            // 何もしない
            return Task.Delay(0);
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            // 今回、ユーザーIDは利用しないので、呼び出されたらException吐く
            throw new NotSupportedException("UserId is not supported");
        }

        private bool ExistUser(string userName)
        {
            if (userName.ToLower() == "admin" || userName.ToLower() == "root")
                return false;

            return true;
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            // spec:「管理者っぽい名前(adminまたはroot)」は弾く
            return Task.Run<ApplicationUser>(() =>
            {
                return !ExistUser(userName) ? new ApplicationUser() { UserName = userName } : null;
            });
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            // 何もしない
            return Task.Delay(0);
        }

        public void Dispose()
        {
        }
    }

    // このアプリケーションで使用されるアプリケーション ユーザー マネージャーを構成します。
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new ApplicationUserStore());
            // var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            
            // ユーザー名の検証ロジックを設定します
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // パスワードの検証ロジックを設定します
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // ユーザー ロックアウトの既定値を設定します。
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // 2 要素認証プロバイダーを登録します。このアプリケーションでは、電話と電子メールをユーザー検証用コード受け取りのステップとして使用します。
            // 独自のプロバイダーをプログラミングしてここにプラグインできます。
            manager.RegisterTwoFactorProvider("電話コード", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "あなたのセキュリティ コードは {0} です。"
            });
            manager.RegisterTwoFactorProvider("電子メール コード", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "セキュリティ コード",
                BodyFormat = "あなたのセキュリティ コードは {0} です。"
            });

            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }
    
    // このアプリケーションで使用されるアプリケーション サインイン マネージャーを構成します。
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager)
        { }

        /*
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }*/

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
