using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoppinWeb.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string Message { get; set; } = default!;

        public void OnGetContact()
        {
            Message = "Your email was sent.";
        }

        public void OnGetOrderSubmitter()
        {
            Message = "Your order submitted successfully.";
        }
    }
}
