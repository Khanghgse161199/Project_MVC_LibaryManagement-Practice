using Autofac;
using Autofac.Extensions.DependencyInjection;
using DataService.Entities;
using Service.AuthService;
using Service.BookServices;
using Service.CardServices;
using Service.CategoryServices;
using Service.HistoryServices;
using Service.OrderServices;
using Service.StudentServices;
using Service.UserServices;

namespace Libary
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
				builder.RegisterType<BookService>().As<IBookService>();
				builder.RegisterType<StudentService>().As<IStudentService>();
				builder.RegisterType<HistoryService>().As<IHistoryService>();
				builder.RegisterType<CardService>().As<ICardService>();
				builder.RegisterType<OrderService>().As<IOrderService>();
			});
			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Login}");

            app.Run();
        }
    }
}