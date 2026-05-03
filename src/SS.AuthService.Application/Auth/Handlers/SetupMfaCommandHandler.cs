using MediatR;
using SS.AuthService.Application.Auth.Commands;
using SS.AuthService.Application.Auth.DTOs;
using SS.AuthService.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SS.AuthService.Application.Auth.Handlers;

public class SetupMfaCommandHandler : IRequestHandler<SetupMfaCommand, MfaSetupResult>
{
    private readonly IMfaService _mfaService;
    private readonly ICacheService _cacheService;
    private readonly IUnitOfWork _unitOfWork;

    public SetupMfaCommandHandler(
        IMfaService mfaService,
        ICacheService cacheService,
        IUnitOfWork unitOfWork)
    {
        _mfaService = mfaService;
        _cacheService = cacheService;
        _unitOfWork = unitOfWork;
    }

    public async Task<MfaSetupResult> Handle(SetupMfaCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            throw new Exception("User not found"); // Should be handled by global exception filter
        }

        // 1. Generate Secret
        string secret = _mfaService.GenerateSecret();

        // 2. Generate URI & QR Code
        string uri = _mfaService.GenerateQrCodeUri(user.Email, secret);
        string qrCodeBase64 = _mfaService.GenerateQrCodeBase64(uri);

        // 3. Store in Cache (TTL 10m)
        string cacheKey = $"mfa_setup_{user.Id}";
        await _cacheService.SetAsync(cacheKey, secret, TimeSpan.FromMinutes(10));

        return new MfaSetupResult(secret, uri, qrCodeBase64);
    }
}
