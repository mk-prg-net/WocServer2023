using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ArticleEdit.Models;

using System.Collections.Concurrent;

// Steht nur bereit, wenn das Nuget- Paket Mocrosoft.AspNet.Identity.Core installiert wurde
using Microsoft.AspNet.Identity;

using MKPRG.DatatypeHandling;

namespace ArticleEdit.Controllers.AccessMgmt
{
    public class AppManager : IUserStore<Models.AppUser>
    {
        ConcurrentDictionary<long, AppUser> Users = new ConcurrentDictionary<long, AppUser>();        

        public async Task Init()
        {
            await new Task(() =>
            {
                Users.TryAdd(0, new AppUser(0, "Anton"));
                Users.TryAdd(1, new AppUser(0, "Berta"));
                Users.TryAdd(2, new AppUser(0, "Cäsar"));
            });
        }

        public async Task CreateAsync(AppUser user)
        {            
            await new Task(() =>
            {
                Users[user.Id] = user;
            });
        }

        public async Task DeleteAsync(AppUser user)
        {
            await new Task(() => {
                if (Users.ContainsKey(user.Id))
                {
                    int i = 3;
                    while (!Users.TryRemove(user.Id, out AppUser appUser) && i > 0)
                    {
                        i--;
                        Task.Delay(10);
                    }
                }
            });            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        AppUser CreateNullUser()
            => new AppUser(0, "none");

        public async Task<AppUser> FindByIdAsync(string userId)
        {
            return await new Task<AppUser>(() =>
            {
                if (!long.TryParse(userId, out long lngUserId))
                {
                    return CreateNullUser();
                }
                else if (!Users.ContainsKey(lngUserId))
                {
                    return CreateNullUser();
                }
                else
                {
                    return Users[lngUserId];
                }
            });
        }

        public async Task<AppUser> FindByNameAsync(string userName)
        {
            return await new Task<AppUser>(() =>
            {
                var user = Users.Values.AsParallel().FirstOrDefault(r => r.UserName == userName);

                if(user == null)
                {
                    return CreateNullUser();
                }
                else
                {
                    return user;
                }
            });
        }

        public async Task UpdateAsync(AppUser user)
        {
            await new Task(() =>
            {
                Users[user.Id] = user;
            });
        }
    }
}