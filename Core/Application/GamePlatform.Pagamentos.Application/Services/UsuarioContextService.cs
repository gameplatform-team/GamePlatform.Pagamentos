using System.Security.Claims;
using GamePlatform.Pagamentos.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace GamePlatform.Pagamentos.Application.Services;

public class UsuarioContextService : IUsuarioContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUsuarioId()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(userId, out var id) ? id : throw new UnauthorizedAccessException("ID do usuário inválido.");
    }
}