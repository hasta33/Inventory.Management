# InventoryManagement

Bu proje .net core 7 web api üzerine inşa edilmiştir. 
  Choreography Design pattern üzerine; N katmanlı, repository pattern ve UnitOfWork kullanılarak dizayn edilmiştir.

- InventoryManagement.Gateway     : Ocelot, Microsoft.AspNetCore.Authentication.JwtBearer
- InventoryManagement.API         : Autofac
- InventoryManagement.Repository  : Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Design, Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Tools
- InventoryManagement.Services    : AutoMapper.Extensions.Microsoft.DependencyInjection, FluentValidation.AspNetCore
- Email.API                       : MailKit, MaasTransit.AspNetCore, MaasTransit.RabbitMQ



## Migrations 
-Migrations: Migrate için InventoryManagement.Repository içinde aşağıdaki komutların çalıştırılması yeterlidir.
dotnet ef migrations add InitialCreate
dotnet ef database update



## Serilog-Seq docker kurulumu
Docker üzerinden seq için dashboard aşağıdaki komut ile kurulum yapılır.
komut                   : docker pull datalust/seq
dashboard url adresi    : http://localhost:5341/#/events


Gateway                 : https://localhost:2001; http://localhost:2000
Email.API               : https://localhost:2005; http://localhost:2004
InventoryManagement.API : https://localhost:2003; http://localhost:2002
Keycloak                : https://localhost:2010; https://localhost:2011
RabbitMq                : https://localhost:2012; https://localhost:2011






Açıklamalar

## InventoryManagement.Core
Database ile etkieşime geçen kısım, Dto, Model katmanları. Repository, Services, UnitOfWork interface'leri.


## InventoryManagement.Repository
-Configuration: Veritabanı tablo ayarları
-Repositories : GenericRepository yer alıyor ek olarak custom generic repository'leri yer almaktadır.
-Seeds        : Veri tabanı ilk oluşturulurken standart veri ekleniyor.
-UnitOfWork   : InventoryManagement.Core katmanında yer alan UnitOfWork interface'inden implemente ediliyor.
-DataContext  : Veritabanı bağlantısını ve tablo tanımlarını oluşturan sınıf.


## InventoryManagement.Services
-Exceptions   : Hataların fırlatıldığı sınıflar
-Mapping      : Modellerin dto'lara, Dto'ların modellere dönüştürüldüğü sınıf.
-Services     : interface'den türetilen sınıfın; commit, add, any, remove, getall, where gibi işlemlerin yapıldığı sınıf.
-Validations  : Dto'ların gerekli alanları işaretleyip kullanıcıya response edildiği alan.