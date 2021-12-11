using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace MyTutoring.Client.Services
{
    public interface IRefreshService
    {
        Task Refresh();
    }
}