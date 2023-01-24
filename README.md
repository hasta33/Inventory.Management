# InventoryManagement

Bu proje .net core 7 web api üzerine inşa eilmiştir. N katmanlı, repository pattern ve UnitOfWork kullanılarak dizayn edilmiştir.
- InventoryManagement.API içinde Autofac
- InventoryManagement.Repository içinde Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Design, Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Tools
- InventoryManagement.Services içinde AutoMapper.Extensions.Microsoft.DependencyInjection, FluentValidation.AspNetCore

Yerel adres
https://localhost:2001;
http://localhost:2000

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


## Migrations 
-Migrations: Migrate işlemi için InventoryManagement.Repository içinde aşağıdaki komutların çalıştırılması gereklidir.
dotnet ef migrations add InitialCreate
dotnet ef database update