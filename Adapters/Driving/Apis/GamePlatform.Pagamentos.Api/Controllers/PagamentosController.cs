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
    private readonly IUsuarioContextService _usuarioContext;

    public PagamentosController(IPagamentoService pagamentoService, IUsuarioContextService usuarioContext)
    {
        _pagamentoService = pagamentoService;
        _usuarioContext = usuarioContext;
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

    /// <summary>
    /// Obtém lista de pagamentos realizados pelo usuário logado
    /// </summary>
    /// <response code="200">Lista de pagamentos do usuário</response>
    [ProducesResponseType(typeof(DataResponseDto<List<PagamentoDto>>), 200)]
    [HttpGet("usuario")]
    [Authorize]
    public async Task<IActionResult> GetPagamentosUsuarioAsync()
    {
        var usuarioId = _usuarioContext.GetUsuarioId();
        var resultado = await _pagamentoService.ObterPagamentosDoUsuarioAsync(usuarioId);
        return !resultado.Sucesso ? BadRequest(resultado) : Ok(resultado);
    }
}