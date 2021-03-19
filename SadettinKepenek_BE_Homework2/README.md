# week2-homework

Herkesten bugun işlediğimiz konular hakkinda bir yazi (makale) yazmasini istiyorum.

WebApi projesi yapilacak
Get - Post işlemleri (  postman uzerinden request ile test etmek dahil )
Mapping 
Extensions 
Interface ve / veya abstract
Validation olacak
EFCore In-Memory kullanilabilir

Makaleniz bu konulari içerebilir. 

Herkesin bir sensryo olusturmasini istiyorum. 
O senaryo üzerinden yukaridaki konulari içeren bir makale olacak.

Makaleyi yayinlamak zorunda değilsiniz, eğer yayınlarsanız reponuza pushlarken README.md dosyasını düzenleyip en alta linki yerleştirin. Kolay gelsin.

# User Management

Birçok web sitesinde kullanıcı yönetimi olan sitelerde kullanıcıların kayıt,giriş ve yetkilendirme işlemleri önemlidir.Kullanıcı yönetiminin çok iyi yazılması bu yüzden çok önemlidir.

## Projenin Oluşturulması
Proje iki temel alt projeden oluşmaktadır.API ve UserService.Bu alt projelerin ayrı ayrı olmasının sebebi hem kodun temiz olması hem de ölçeklendirilebilmesidir.Projeleri oluşturduktan sonra Servis projesinin altında Domain klasörü açılmalıdır.Bu klasörün isminin Domain olmasının sebebi DDD(Domain Driven Design) yazılım prensibidir.

## User Service
Daha sonra Services.User projesinin altına dosya yapısı kurulmaılıdır.Projede kullanılacak herşey klasörlenmelidir.Bu kodu daha temiz yapacaktır.İhtiyaca göre Data,Entities,Enums,Extensions vs. klasörler oluşturulur.İlk olarak User Entity'sini oluşturalım.
``` csharp
    public class User  
    {  
	    public int Id { get; set; }  
	    public string Email { get; set; }  
	    public string Password { get; set; }  
	    public string Firstname { get; set; }  
	    public string Lastname { get; set; }  
	    public int RoleId { get; set; }  
    }
```
User Entity'si oluşturulduktan sonra gerekli Dto'lar oluşturulmalıdır.Kullanıcının kayıt,giriş vs. işlemleri için gerekli Dto'lar oluşturulur.Daha sonra bu Dto'larla User Entity'sini map etmemiz için MappingExtensions class'ını oluşturmamız lazım.

  ```` csharp
    public static class MappingExtensions  
    {  
	    public static User MapToUser(this RegisterRequest request)  
	    {  
	        return new User()  
	        {  
	            Email = request.Email,  
	            Password = request.Password,  
	            Firstname = request.Firstname,  
	            Lastname = request.Lastname,  
	            RoleId = request.RoleId  
	        };  
	    }   
	    public static User MapToUser(this LoginRequest request)  
	    {  
	        return new User()  
	        {  
	            Email = request.Email,  
	            Password = request.Password  
	        };  
	    }  
	      
	    public static List<UserDto> MapToUserDto(this List<User> users)  
	    {  
	        return users.Select(u => new UserDto()  
	        {  
	            Email = u.Email,  
	            Firstname = u.Firstname,  
	            Lastname = u.Lastname,  
	            Id = u.Id  
	        }).ToList();  
	    }  
    }
````

Mapping işlemleri bittikten sonra UserService oluşturulur.Bu class bir IUserService interface'inden kalıtım almalıdır.UserService class'ı,veritabanı ve iş akışımızın olduğu bir servistir.Controller'dan bu servise istek atılır.

Bir kullanıcı sisteminde kullanıcılar yetkilendirilebilmelidir.Bu yüzden her kullanıcıda RoleId alanı vardır.Kullanıcı sisteme giriş yaptığında daha sonraki işlemlerinde yetkisinin kontrol edilebilmesi için verisi Session'da saklanmalıdır.Session'da saklanacak veri,bu kullanıcının email ve RoleId'sidir.

HttpContext nesnesi içinde Session saklanabilir ve yönetilebilir.HttpContext nesnesine ulaşmak için IHttpContext servisinden faydalanırız.Session işlemlerini gerçekleştirmek için SessionManager sınıfı oluşturulmalıdır.

``` csharp
    public class SessionManager:ISessionManager  
    {  
	    private IHttpContextAccessor _httpContextAccessor;  
	  
	    public SessionManager(IHttpContextAccessor httpContextAccessor)  
	    {  
	        _httpContextAccessor = httpContextAccessor;  
	    }  
	  
	  
	    public void SetSession(string sessionName, string value)  
	    {  
	        _httpContextAccessor.HttpContext.Session.Set(sessionName,Encoding.UTF8.GetBytes(value));  
	    }  
	  
	    public string GetSession(string sessionName)  
	    {  
	        _httpContextAccessor.HttpContext.Session.TryGetValue(sessionName,out var data);  
	        return Encoding.UTF8.GetString(data);  
	    }  
    }
````

Veritabanı işlemleri için Entityframework Core In Memory kullanılacaktır.Bunun için Ef Core In Memory paketi kurulur.Daha sonra DbContext ve DbSet oluşturulur.Daha sonra API projesinde DbContext inject edilir.
Injection'ların StartUp'da karışmaması için her biri extension olarak oluşturulmuştur. 

Kayıt işlemleri için UserService sınıfındaki Register metodu aşağıdagi gibi olmalıdır.
```` csharp
public ServiceResponseModel Register(RegisterRequest request)  
{  
    var isUserExist = IsUserExist(request.Email);  
    if (isUserExist)  
    {  
        return new ServiceResponseModel("User is already exist",false);  
    }  
    var user = request.MapToUser();  
    _dbContext.Users.Add(user);  
    _dbContext.SaveChanges();  
    return new ServiceResponseModel("User registered successfully",true);  
  
}  
  
private bool IsUserExist(string email)  
{  
    var user = _dbContext.Users.FirstOrDefault(u => u.Email.Equals(email));  
    return user != null;  
}
````


> SessionManager sınıfı HttpContext nesnesindeki Session bilgisini güncelleyebilir ya da yeni bir Session ekleyebilir.

 HttpContext nesnesindeki user bilgisini doldurmak için UserService'te Login metodu doldurulur.Bu metod'da Session'a giriş yapan kullanıcının verisi eklenmelidir.

```csharp 
    public ServiceResponseModel Login(LoginRequest request)  
    {  
	    var user  = _dbContext.Users.FirstOrDefault(u =>  
	        u.Email.Equals(request.Email) && u.Password.Equals(request.Password));  
	      
	    if (user != null)  
	    {  
	        var sessionData = JsonConvert.SerializeObject(new UserSession()  
	        {  
	            Email = user.Email,  
	            RoleId = user.RoleId  
	        });  
	        _sessionManager.SetSession("User", sessionData);  
	        return new ServiceResponseModel("Login success",false);  
	    }  
		    return new ServiceResponseModel("Email or password is not correct",false); 
    } 
   ```

   
  Kullanıcı yetkilendirmesi için Action filter kullanılmıştır.
  
   ``` csharp
	  [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]  
	  public class RoleAccessAttribute : ActionFilterAttribute  {  
	  
		  public UserRole Role { get; set; }  
		  public RoleAccessAttribute(UserRole role)  
		  {  
	        Role = role;  
		  }  
	  
	    public override void OnActionExecuting(ActionExecutingContext context)  
	    {  
	        var isAuthorized = IsAuthorized(context);  
	        if (!isAuthorized)  
	        {  
	            throw new UnauthorizedAccessException("You dont have access");  
	        }  
	    }  
	  
	    private bool IsAuthorized(ActionExecutingContext context)  
	    {  
	        var userString = context.HttpContext.Session.GetString("User");  
	        if (userString == null)  
	        {  
	            throw new AuthenticationException("Please login inorder to use this endpoint");  
	        }  
	  
	        var user = JsonConvert.DeserializeObject<UserSession>(userString);  
	        return user.RoleId == (int) Role;  
	    }  
    }
```
Her bir modelin için Tag Attributeler ile validation sağlanmıştır.Action filter ile daha istek actiona düşmeden validasyon kontrolü yapılır.

```csharp
    public class ValidationFilterAttribute:ActionFilterAttribute  
    {  
	    public override void OnActionExecuting(ActionExecutingContext context)  
	    {  
	        if(!context.ModelState.IsValid)  
	        {  
	            context.Result = new BadRequestObjectResult(context.ModelState);  
	        }  
	    }  
    }
```
Bunlara ek olarak validasyonda iyileştirmeler yapılabilir.Kullanıcının bilgileri session'da saklanırken şifrelenmelidir.Bu projenin kapsamında olmadığı için şuanlık yapılmamıştır.


    



 

 


 

    



 

 


 

    
    



 

 


 
