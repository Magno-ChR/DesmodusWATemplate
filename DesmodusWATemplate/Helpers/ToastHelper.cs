using DesmodusWATemplate.Shared.Components;
using Microsoft.JSInterop;
namespace DesmodusWATemplate.Helpers
{
    public class ToastHelper : IToast
    {
        private readonly IJSRuntime js;

        public ToastHelper(IJSRuntime js)
        {
            this.js = js;
        }
        public async Task show()
        {
            await js.InvokeVoidAsync("showToast");
        }
    }
}
