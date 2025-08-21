using System.ComponentModel.DataAnnotations;
using GamePlatform.Pagamentos.Application.DTOs;
using GamePlatform.Pagamentos.Application.DTOs.Pagamento;
using GamePlatform.Pagamentos.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamePlatform.Pagamentos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(500)]
public class PagamentosController : ControllerBase
{
    private readonly IPagamentoService _pagamentoService;

    public PagamentosController(IPagamentoService pagamentoService)
    {
        _pagamentoService = pagamentoService;
    }

    /// <summary>
    /// Obtém um pagamento pelo ID
    /// </summary>
    /// <param name="id">ID do pagamento</param>
    /// <response code="200">Pagamento buscado</response>
    /// <response code="404">Pagamento não encontrado</response>
    [ProducesResponseType(typeof(DataResponseDto<PagamentoDto>), 200)]
    [ProducesResponseType(typeof(BaseResponseDto), 404)]
    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetPagamentoAsync([FromRoute, Required] Guid id)
    {
        var resultado = await _pagamentoService.ObterPorIdAsync(id);
        return !resultado.Sucesso ? NotFound(resultado) : Ok(resultado);
    }
}