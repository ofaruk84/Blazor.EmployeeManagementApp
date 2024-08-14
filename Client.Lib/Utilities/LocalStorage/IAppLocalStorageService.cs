using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Lib.Utilities.LocalStorage
{
    public interface IAppLocalStorageService
    {
        Task<string?> GetItem(string key);
        Task SetItem(string key, string value);
        Task RemoveItem(string key);
    }
}
