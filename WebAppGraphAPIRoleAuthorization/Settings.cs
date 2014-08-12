namespace WebAppGraphAPIRoleAuthorization
{
    using System.Configuration;
    using System.Globalization;

    public static class Settings
    {
        public const string UsersRole = "UsersRole";
        public const string AdminRole = "AdminRole";

        internal const string RoleClaimIssuer = "GRAPH";
        internal const string TenantIdClaimType = "http://schemas.microsoft.com/identity/claims/tenantid";
        internal const string ObjectIdentifierClaimType = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        internal const string AccessTokenKey = "AccessTokenKey";

        private const string RedirectUriSettingName = "ida:RedirectUri";
        private const string AADInstanceSettingName = "ida:AADInstance";
        private const string TenantSettingName = "ida:Tenant";
        private const string ClientIdSettingName = "ida:ClientId";
        private const string PasswordSettingName = "ida:Password";
        private const string GraphResourceIDSettingName = "ida:GraphResourceID";
        private const string GraphApiVersionSettingName = "ida:GraphApiVersion";
        private const string UsersGroupIdSettingName = "ida:UsersGroupId";
        private const string AdminGroupIdSettingName = "ida:AdminGroupId";
        
        internal static string RedirectUri
        {
            get { return ConfigurationManager.AppSettings[RedirectUriSettingName]; }
        }

        internal static string AADInstance
        {
            get { return ConfigurationManager.AppSettings[AADInstanceSettingName]; }
        }

        internal static string Tenant
        {
            get { return ConfigurationManager.AppSettings[TenantSettingName]; }
        }

        internal static string Authority
        {
            get { return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", AADInstance, Tenant); }
        }

        internal static string ClientId
        {
            get { return ConfigurationManager.AppSettings[ClientIdSettingName]; }
        }

        internal static string Password
        {
            get { return ConfigurationManager.AppSettings[PasswordSettingName]; }
        }

        internal static string GraphResourceID
        {
            get { return ConfigurationManager.AppSettings[GraphResourceIDSettingName]; }
        }

        internal static string GraphApiVersion
        {
            get { return ConfigurationManager.AppSettings[GraphApiVersionSettingName]; }
        }

        internal static string UsersGroupId
        {
            get { return ConfigurationManager.AppSettings[UsersGroupIdSettingName]; }
        }

        internal static string AuditGroupId
        {
            get { return ConfigurationManager.AppSettings[AdminGroupIdSettingName]; }
        }
    }
}
