using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace MyTutoring.Client.Services.Refresh
{
    public interface IRefreshService
    {
        Task Refresh();
    }
}