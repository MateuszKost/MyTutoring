using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyTutoring.Client;
using MyTutoring.Client.Services;
using MyTutoring.Client.Services.Authentication;
using MyTutoring.Client.Services.EditProfile;
using MyTutoring.Client.Services.Material;
using MyTutoring.Client.Services.MaterialGroup;
using MyTutoring.Client.Services.MaterialType;
using MyTutoring.Client.Services.MaterialVisibility;
using MyTutoring.Client.Services.Test;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, MyTutoringAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IEditProfileService, EditProfileService>();
builder.Services.AddScoped<IMaterialsGroupService, MaterialsGroupService>();
builder.Services.AddScoped<IMaterialTypeService, MaterialTypeService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IMaterialVisibilityService, MaterialVisibilityService>();

await builder.Build().RunAsync();
