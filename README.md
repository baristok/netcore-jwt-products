<!DOCTYPE html>
<html lang="tr">
<head>
</head>
<body>
    <h1>JWT Tabanlı Ürün Yönetim Sistemi</h1> 
    <p>Bu proje, <code>ASP.NET Core 8.0 Onion Architecture (Soğan Mimarisi) ve SOLID prensiplerine</code> uygun olarak geliştirilmiş 
        bir web uygulamasıdır. 
        JWT (JSON Web Token) kimlik doğrulama sistemine sahip
         bir ürün ve kategori 
        yönetim uygulamasıdır. Proje, Onion mimari yapısı uygun
         olarak tasarlanmıştır.</p>
         <h2>Teknolojiler ve Araçlar</h2>
    <ul>
        <li>.NET 8.0</li>
        <li>ASP.NET Core MVC</li>
        <li>Entity Framework Core 8.0</li>
        <li>JWT (JSON Web Token)</li>
        <li>MediatR</li>
        <li>AutoMapper</li>
        <li>SQL Server</li>
        <li>Bootstrap</li>
    </ul>
    <h2>Proje Yapısı</h2>
    <p>Uygulama, Onion Architecture (Soğan Mimarisi) prensiplerine göre tasarlanmış ve iki ana projeden oluşmaktadır:</p>
    <ol>
        <li>
            <h3>JwtApp.Back: Backend API uygulaması</h3>
            <ul>
                <li><strong>Core</strong>: Mimarinin merkezi
                    <ul>
                        <li>Domain: Temel varlık modelleri (AppUser, AppRole, Product, Category)</li>
                        <li>Application: Uygulama servisleri, CQRS/Mediator, DTO'lar, business rules</li>
                    </ul>
                </li>
                <li><strong>Infrastructure</strong>: Dış servisler ve araçlar (JWT yapılandırması vb.)</li>
                <li><strong>Persistence</strong>: Veritabanı işlemleri ve repository implementasyonları</li>
                <li><strong>Controllers</strong>: API kontrolcüleri</li>
            </ul>
        </li>
        <li>
            <h3>JwtApp.Front: Frontend MVC uygulaması</h3>
            <ul>
                <li>Controllers: MVC kontrolcüleri</li>
                <li>Models: View modelleri</li>
                <li>Views: Kullanıcı arayüzü şablonları</li>
                <li>wwwroot: Statik dosyalar (CSS, JS, resimler)</li>
            </ul>
        </li>
    </ol>
    <h2>Özellikler</h2>
    <h3>Kullanıcı Yönetimi</h3>
    <ul>
        <li>JWT tabanlı kimlik doğrulama</li>
        <li>Rol tabanlı yetkilendirme (Admin, Kullanıcı)</li>
        <li>Giriş ve çıkış işlemleri</li>
    </ul>  
    <h3>Ürün Yönetimi</h3>
    <ul>
        <li>Ürün listeleme, ekleme, güncelleme, silme</li>
        <li>Kategoriye göre ürün filtreleme</li>
    </ul>   
    <h3>Kategori Yönetimi</h3>
    <ul>
        <li>Kategori listeleme, ekleme, güncelleme, silme</li>
    </ul>
    <h3>Veritabanı</h3>
    <ul>
        <li>Microsoft SQL Server</li>
    </ul>  
    <h2>Gereksinimler</h2>
    <ul>
        <li>.NET 8.0 SDK</li>
        <li>Microsoft SQL Server (LocalDB veya Express)</li>
    </ul>
    <h2>Kurulum</h2>
    <ol>
        <li>
            <p>Projeyi klonlayın:</p>
            <pre><code>git clone https://github.com/[kullanıcı-adınız]/JWT-Tabanli-Urun-Yonetim-Sistemi.git</code></pre>
        </li>
        <li>
            <p>Gerekli yapılandırmaları düzenleyin:</p>
            <ul>
                <li><strong>Veritabanı Bağlantısı:</strong> <code>appsettings.json</code> dosyasında veritabanı bağlantı adresini düzenleyin</li>
                <li><strong>JWT Token Ayarları:</strong> <code>JwtApp.Back.Infrastructure.Tools</code> bölümünde token geçerlilik süresi ve güvenlik anahtarını yapılandırın</li>
                <li><strong>CORS Ayarları:</strong> İhtiyaç duyulursa izin verilen kaynakları yapılandırın</li>
                <li><strong>Port Yapılandırması:</strong> Gerekirse <code>launchSettings.json</code> dosyasında port numaralarını düzenleyin</li>
            </ul>
        </li>        
        <li>
            <p>Backend projesini çalıştırın:</p>
            <pre><code>cd UdemyJwtApp.Back
dotnet run</code></pre>
        </li>
        <li>
            <p>Frontend projesini çalıştırın:</p>
            <pre><code>cd UdemyJwtApp.Front
dotnet run</code></pre>
        </li>        
    </ol>
    <h2>API Endpoints</h2>
    <ul>
        <li><strong>/api/auth</strong>: Kimlik doğrulama işlemleri</li>
        <li><strong>/api/products</strong>: Ürün işlemleri</li>
        <li><strong>/api/categories</strong>: Kategori işlemleri</li>
    </ul>
    <img src="https://github.com/baristok/netcore-jwt-products/blob/main/Udemy.jwtApp/JwtApp.Front/wwwroot/images/images.png" alt="API Endpoints">
    <h2>Bağlantılar</h2>
    <ul>
        <li>ASP.NET Core Dokümantasyonu: <a href="https://learn.microsoft.com/tr-tr/aspnet/core/">Microsoft Docs</a></li>
        <li>JWT Dokümantasyonu: <a href="https://jwt.io/">jwt.io</a></li>
    </ul>    
    <h2>İletişim</h2>
    <p>Barış Tok - <a href="https://github.com/baristok">GitHub Profili</a></p>
    <p>Proje Linki: <a href="https://github.com/baristok/netcore-jwt-products">netcore-jwt-products</a></p>
    <p>Not: Bu proje, Yavuz Selim Kahramanın Udemy kursunda yaptığı bir projedir. Üzerinde kendimce değişiklikler yapılmıştır. Kendisine katkılarından dolayı teşekkürlerimi sunuyorum.</p>
</body>
</html>
