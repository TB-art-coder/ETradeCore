1) Entity'ler oluşturulur.
2) Microsoft.EntityFrameworkCore.SqlServer paketi AppCore projesine, Microsoft.EntityFrameworkCore.Tools paketi ise DataAccess projesine, 
Microsoft.EntityFrameworkCore.Design paketi de MVC veya WebApi projelerine NuGet'ten indirilir.
3) DbContext'ten türeyen Context ve içerisindeki DbSet'ler oluşturulur.
4) 
4.1.1) 1. yöntem (uygun değil): Context içerisindeki override edilen OnConfiguring methodunda connection string 
server=.\\SQLEXPRESS;database=ETradeCore;user id=sa;password=sa;multipleactiveresultsets=true; formatta yazılır.
4.1.2) 2. yöntem: Context class'ının DbContextOptions tipinde parametre alan constructor'ı oluşturulur.
4.2) appsettings.json dosyasına ConnectionStrings ile veritabanı bağlantı bilgisi yazılır. İstenirse appsettings.development.json'da da 
connection string aynı şekilde yazılabilir. 
4.3) Program.cs'de IoC Container içerisinde önce builder.Configuration üzerinden connection string okunur, 
sonra da builder.Services üzerinden AddDbContext methodu ile okunan connection string kullanılarak context'in bağımlılığı yönetilir.
5) Tools -> NuGet Package Manager -> Package Manager Console açılır ve önce add-migration v1 daha sonra 
update-database komutları çalıştırılır.
6) İstenirse ilk verileri oluşturmak için Database gibi bir controller oluşturulup içerisine Seed gibi bir action yazılarak
veritabanında ilk verilerin oluşturulması sağlanabilir.
7) Entity model dönüşümlerini gerçekleştirecek servis class'ları önce interface üzerinden methodlar tanımlanarak oluşturulur.
Tanımlanabilecek methodlar CRUD işlemlerine karşılık gelecek Query, Add, Update ve Delete methodlarıdır.
Bu aşamada entity'lere karşılık model'ler de oluşturulmalıdır.
8) appsettings.json ve istenirse appsettings.Development.json içerisine ConnectionStrings altına projenin veritabanı bağlantı bilgisi yazılır. 
Program.cs altında builder.Configuration.GetConnectionString methodu kullanılarak bağlantı bilgisi AppCore altındaki static
ConnectionConfig class'ının static ConnectionString özelliğine set edilir. Daha sonra ConnectionConfig.ConnectionString özelliği context 
class'ının OnConfiguring methodunda UseSqlServer methoduna parametre olarak gönderilir.
9) Program.cs altında IoC Container içerisinde service interface'leri için servis class'ları tanımları yapılır.
10) İlgili model için Controller oluşturulur, dependency injection için ilgili servisin interface'i tipinde parametreli 
constructor yazılır, daha sonra Index, Details, Create, Edit ve Delete aksiyonları yazılır.
11) Bu aksiyonlar sonucunda ilgili view'lar oluşturulur.
12) Bazı controller aksiyon'larını çağırabilmek için view'larda veya layout view'ında link'ler yazılır.
13) View'larda yapılan değişikliklerin proje çalışırken tarayıcıdan sayfanın yenilenmesi durumunda sayfaya yansıması için
NuGet'ten Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation paketi indirilir ve projede Properties -> launchSettings.json
dosyasına "ASPNETCORE_ENVIRONMENT" altına "ASPNETCORE_HOSTINGSTARTUPASSEMBLIES": "Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation"
tüm profiller için eklenir.
14) İstenirse launchSettings.json'daki profiles altında IIS Express development (DEV), MvcWebUI production (PROD) olarak ayarlanabilir.