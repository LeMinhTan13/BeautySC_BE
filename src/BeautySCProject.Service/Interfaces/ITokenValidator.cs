using BeautySCProject.Common.Helpers;

namespace BeautySCProject.Service.Interfaces
{
    public interface ITokenValidator
    {
        bool ValidateRefreshToken(string token);
        MethodResult<string> GetEmailFromExpiredAccessToken(string token);
        MethodResult<string> ValidateEmailVerificationToken(string token);
        MethodResult<(string, string)> ValidateEmailUpdateToken(string token);
        MethodResult<string> ValidatePasswordResetToken(string token);

    }
}
