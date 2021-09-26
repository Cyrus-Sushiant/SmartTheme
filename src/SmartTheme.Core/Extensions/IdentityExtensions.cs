using SmartTheme.Core.Domain.Identity;
using System;
using System.Security.Claims;
using System.Security.Principal;

namespace SmartTheme.Core.Extensions
{
    public static class IdentityExtensions
    {
        public static T GetUserId<T>(this ClaimsPrincipal user)
        {
            return (T)Convert.ChangeType(user.FindFirstValue(ClaimTypes.NameIdentifier), typeof(T));
        }

        public static T GetUserId<T>(this IIdentity identity)
        {
            return (T)Convert.ChangeType((identity as ClaimsIdentity).FirstOrNull(ClaimTypes.NameIdentifier), typeof(T));
        }

        public static string GetUserId(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).FirstOrNull(ClaimTypes.NameIdentifier);
        }

        public static string GetGivenName(this IIdentity identity)
        {
            if (identity == null)
                return null;

            return (identity as ClaimsIdentity).FirstOrNull(ClaimTypes.GivenName);
        }

        public static string GetSurname(this IIdentity identity)
        {
            if (identity == null)
                return null;

            return (identity as ClaimsIdentity).FirstOrNull(ClaimTypes.Surname);
        }

        public static Gender GetGender(this IIdentity identity)
        {
            if (identity == null)
                return Gender.NotSet;

            return Enum.Parse<Gender>((identity as ClaimsIdentity).FirstOrNull(ClaimTypes.Gender));
        }

        #region Utilities

        private static string FirstOrNull(this ClaimsIdentity identity, string claimType)
        {
            var val = identity.FindFirst(claimType);

            return val == null ? null : val.Value;
        }

        #endregion
    }
}
