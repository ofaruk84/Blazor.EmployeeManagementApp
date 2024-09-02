using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Lib.Utilities
{
    public class DialogService
    {
        private readonly SfDialogService _sfDialogService;

        public DialogService(SfDialogService sfDialogService)
        {
            _sfDialogService = sfDialogService;
        }

        public async Task DisplayDialog(string content, string title)
        {
            await _sfDialogService.AlertAsync(content, title);
        }
    }
}
