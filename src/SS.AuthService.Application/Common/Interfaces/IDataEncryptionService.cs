namespace SS.AuthService.Application.Common.Interfaces;

public interface IDataEncryptionService
{
    string Protect(string plainText);
    string Unprotect(string cipherText);
}
