# Identity Without Email

This project is a POC that aims to use Microsoft Identity without an e-mail, using other auth methods like phoneNumber/pin. 

## Main focus relies in these changes:

```c#
services.AddIdentity<IdentityUser, IdentityRole>(options => 
{ 
     options.SignIn.RequireConfirmedAccount = false; 
     options.User.RequireUniqueEmail = false; 
     options.Password.RequireLowercase = false; 
     options.Password.RequireNonAlphanumeric = false; 
     options.Password.RequireUppercase = false; 
})
.AddEntityFrameworkStores<ApplicationDbContext>();

```

## Contributing
Not open to contributions at the moment