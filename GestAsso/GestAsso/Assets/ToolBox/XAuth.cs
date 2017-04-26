namespace GestAsso.Assets.ToolBox
{
    public abstract class XAuth
    {
        #region Google
        protected const string StrGoogleKey = "962936376748-v87jg2b9eg39j5bcmqj79t7ilhlb8829.apps.googleusercontent.com";
        protected const string StrGoogleScope = "https://www.googleapis.com/auth/userinfo.email";
        protected const string StrGoogleAuthUrl = "https://accounts.google.com/o/oauth2/auth";
        protected const string StrGoogleRtrUrl = "https://www.googleapis.com/plus/v1/people/me";
        #endregion

        #region Facebook
        protected const string StrFacebookKey = "337371049934482";
        protected const string StrFacebookScope = "";
        protected const string StrFacebookAuthUrl = "https://m.facebook.com/dialog/oauth/";
        protected const string StrFacebookRtrUrl = "http://www.facebook.com/connect/login_success.html";
        #endregion
    }
}