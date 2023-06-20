using Autofac;
using Autofac.Extensions.DependencyInjection;
using DataService.Entities;
using Service.AuthService;
using Service.BookServices;
using Service.CategoryServices;
using Service.HistoryServices;
using Service.OrderServices;
using Service.UserServices;

namespace AdminLibary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddSession();
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterType<BookContext>().AsSelf();
                builder.RegisterType<AuthService>().As<IAuthService>();
                builder.RegisterType<UserService>().As<IUserService>();
                builder.RegisterType<CategoryService>().As<ICategoryService>();
                builder.RegisterType<BookService>().As<IBookService>();
				builder.RegisterType<OrderService>().As<IOrderService>();
                builder.RegisterType<HistoryService>().As<IHistoryService>();
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Index}");

            app.Run();
        }
    }
}