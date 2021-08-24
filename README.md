# ApiHostWithIdUpdate
Add project:
  - Volo.Abp.Account.Application.Contracts
  - Volo.Abp.Account.HttpApi
  - Volo.Abp.Account.Web
  - Volo.Abp.Account.Web.IdentityServer
1. Chuyển database từ sqlserver sang mysql
  - Project MyCompanyName.MyProjectName.EntityFrameworkCore: Gỡ package Volo.Abp.EntityFrameworkCore.SqlServer, cài package Volo.Abp.EntityFrameworkCore.MySQL 
  - Thay đổi ConnectionString trong appsettings.json
  - Update code: Replace UseSqlServer() with UseMySQL()
2. Thêm tính năng đăng ký có captcha
  - Project MyCompanyName.MyProjectName.HttpApi.HostWithIds: thêm SiteKey, SecretKey trong appsettings.json
  - Project Volo.Abp.Account.Application.Contracts --> thư mục: Volo\Abp\Account: thêm class ReCaptcha.cs để valid Captcha
  - Project Volo.Abp.Account.Web --> Thư mục: \Pages\Account: Update file Register.cshtml, Register.cshtml.cs để hiển thị Captcha trên form và valid Captcha khi submit
3. Thêm tính năng đăng nhập có captcha
  - Project Volo.Abp.Account.Web --> Thư mục: \Pages\Account: Update file Login.cshtml để hiển thị Captcha trên form.
  - Project Volo.Abp.Account.Web.IdentityServer --> Thư mục: Pages\Account: Update file IdentityServerSupportedLoginModel.cs để valid Captcha khi login
