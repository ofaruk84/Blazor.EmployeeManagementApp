using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Lib.Utilities.LocalStorage
{
    
    public class BlazoredLocalServiceManager : IAppLocalStorageService
    {
        private readonly ILocalStorageService _blazoredLocalStorageService;
        
        public BlazoredLocalServiceManager(ILocalStorageService blazoredLocalStorageService)
        {
            _blazoredLocalStorageService = blazoredLocalStorageService;
        }

        public async Task<string?> GetItem(string key)
        {
           return await _blazoredLocalStorageService.GetItemAsStringAsync(key);
        }

        public async Task RemoveItem(string key)
        {
           await _blazoredLocalStorageService.RemoveItemAsync(key);
        }

        public async Task SetItem(string key, string value)
        {
            await _blazoredLocalStorageService.SetItemAsStringAsync(key,value);
        }
    }
}
