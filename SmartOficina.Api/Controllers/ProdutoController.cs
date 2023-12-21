﻿namespace SmartOficina.Api.Controllers;

/// <summary>
/// Controller de produto
/// </summary>
[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class ProdutoController : MainController
{
    private readonly IMapper _mapper;
    private readonly IProdutoRepository _repository;

    public ProdutoController(IMapper mapper, IProdutoRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    /// <summary>
    /// Adicionar um produto
    /// </summary>
    /// <param name="produto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Add(ProdutoDto produto)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        MapearLogin(produto);

        var result = await _repository.Create(_mapper.Map<Produto>(produto));

        if (result == null)
            NoContent();

        return Ok(_mapper.Map<ProdutoDto>(result));
    }

    private void MapearLogin(ProdutoDto produto)
    {
        if (!produto.PrestadorId.HasValue)
            produto.PrestadorId = PrestadorId;

        produto.UsrCadastroDesc = UserName;
        produto.UsrCadastro = UserId;
    }

    /// <summary>
    /// Recuperar todos os produtos com parametros opcionais
    /// </summary>
    /// <param name="marca"></param>
    /// <param name="nome"></param>
    /// <param name="modelo"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(string? marca, string? nome, string? modelo)
    {
        var result = await _repository.GetAll(PrestadorId, new Produto() { Marca = marca, Nome = nome, Modelo = modelo, PrestadorId = PrestadorId, Valor_Compra = 0, Valor_Venda = 0 });

        if (result == null || !result.Any())
            NoContent();

        return Ok(_mapper.Map<ICollection<ProdutoDto>>(result));
    }

    /// <summary>
    /// Recuperar um produto por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(Guid id)
    {
        if (!ModelState.IsValid || id == null)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        var result = await _repository.FindById(id);

        if (result == null)
            NoContent();

        return Ok(_mapper.Map<ProdutoDto>(result));
    }

    /// <summary>
    /// Atualizar produto
    /// </summary>
    /// <param name="produto"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> AtualizarProduto(ProdutoDto produto)
    {
        if (!ModelState.IsValid || !produto.Id.HasValue)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        MapearLogin(produto);

        var result = await _repository.Update(_mapper.Map<Produto>(produto));
        if (result == null)
            NoContent();
        return Ok(_mapper.Map<ProdutoDto>(result));
    }

    /// <summary>
    /// Desativar do produto
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("DesativarPrestador")]
    public async Task<IActionResult> DesativarProduto(Guid id)
    {
        if (!ModelState.IsValid || id == null)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        var result = await _repository.Desabled(id);
        
        if (result == null)
            NoContent();
        
        return Ok(_mapper.Map<ProdutoDto>(result));
    }

    /// <summary>
    /// Deletar um produto
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeletarProduto(Guid id)
    {
        try
        {
            if (!ModelState.IsValid || id == null)
            {
                if (ModelState.ErrorCount < 1)
                    ModelState.AddModelError("error", "Id invalid");

                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            await _repository.Delete(id);
            return Ok("Deletado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
