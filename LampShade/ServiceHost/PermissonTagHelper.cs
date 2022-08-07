using System.Linq;
using _0_Framework.Application;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ServiceHost
{


    [HtmlTargetElement(Attributes = "Permission")]
    public class PermissionTagHelper : TagHelper
    {
        public int Permission { get; set; }

        private readonly IAuthHelper _authHelper;

        public PermissionTagHelper(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            ///age autorize nabod kolan namayesh nade va return kkon
            if (!_authHelper.IsAuthenticated())
            {
                output.SuppressOutput();
                return;
            }
            //hala permission karbar mojod ro migirim
            var permissions = _authHelper.GetPermission();
            if (permissions.All(x => x != Permission))
            {
                output.SuppressOutput();
                return;
            }
            //da gheir in sorat on element prossesesh ejra beshe
            base.Process(context, output);
        }
    }
}
