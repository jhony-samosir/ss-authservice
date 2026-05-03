using MediatR;
using SS.AuthService.Application.Auth.Commands;
using SS.AuthService.Application.Auth.DTOs;
using SS.AuthService.Application.Interfaces;
using SS.AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace SS.AuthService.Application.Auth.Handlers;

public class EnableMfaCommandHandler : IRequestHandler<EnableMfaCommand, MfaEnableResult>
{
    private readonly IMfaService _mfaService;
    private readonly ICacheService _cacheService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public EnableMfaCommandHandler(
        IMfaService mfaService,
        ICacheService cacheService,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _mfaService = mfaService;
        _cacheService = cacheService;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<MfaEnableResult> Handle(EnableMfaCommand request, CancellationToken cancellationToken)
    {
        // 1. Get Secret from Cache
        string cacheKey = $"mfa_setup_{request.UserId}";
        string? secret = await _cacheService.GetAsync<string>(cacheKey);

        if (string.IsNullOrEmpty(secret))
        {
            return new MfaEnableResult(false, "MFA setup session expired. Please restart setup.");
        }

        // 2. Verify Code
        bool isValid = _mfaService.VerifyCode(secret, request.Code);
        if (!isValid)
        {
            return new MfaEnableResult(false, "Invalid MFA code.");
        }

        // 3. Update User & Generate Recovery Codes (Transactional)
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null) throw new Exception("User not found");

        var rawRecoveryCodes = GenerateRecoveryCodes(10);
        
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            // Update User
            user.MfaEnabled = true;
            user.MfaSecret = secret;
            _unitOfWork.Users.Update(user);

            // Insert Recovery Codes
            var recoveryCodeEntities = rawRecoveryCodes.Select(code => new MfaRecoveryCode
            {
                UserId = user.Id,
                CodeHash = _passwordHasher.HashPassword(code),
                IsUsed = false
            }).ToList();

            await _unitOfWork.MfaRecoveryCodes.AddRangeAsync(recoveryCodeEntities, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            // 4. Remove from Cache
            await _cacheService.RemoveAsync(cacheKey);

            return new MfaEnableResult(true, "MFA enabled successfully.", rawRecoveryCodes);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    private List<string> GenerateRecoveryCodes(int count)
    {
        var codes = new List<string>();
        for (int i = 0; i < count; i++)
        {
            // Format: XXXXX-XXXXX
            codes.Add($"{GenerateRandomString(5)}-{GenerateRandomString(5)}".ToUpper());
        }
        return codes;
    }

    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789"; // Exclude similar looking chars
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
    }
}
