﻿namespace SmartOficina.Api.Controllers;

/// <summary>
/// Controller de veículo
/// </summary>
[Route("api/[controller]")]
[ApiController, Authorize]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class VeiculoController : MainController
{
    private readonly IVeiculoRepository _repository;
    private readonly IMapper _mapper;

    public VeiculoController(IVeiculoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Adicionar um veículo
    /// </summary>
    /// <param name="veiculo"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Add(VeiculoDto veiculo)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        MapearLogin(veiculo);

        var result = await _repository.Create(_mapper.Map<Veiculo>(veiculo));
        if (result == null)
            NoContent();
        return Ok(_mapper.Map<VeiculoDto>(result));


    }

    private void MapearLogin(VeiculoDto veiculo)
    {
        if (!veiculo.PrestadorId.HasValue)
            veiculo.PrestadorId = PrestadorId;

        veiculo.UsrCadastroDesc = UserName;
        veiculo.UsrCadastro = UserId;
    }

    /// <summary>
    /// Recuperar todos os veículos
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _repository.GetAll(PrestadorId, new Veiculo() { Marca = string.Empty, Modelo = string.Empty, Placa = string.Empty });
        if (result == null || !result.Any())
            NoContent();
        return Ok(_mapper.Map<ICollection<VeiculoDto>>(result));

    }

    /// <summary>
    /// Recuperar um veículo por Id
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
        return Ok(_mapper.Map<VeiculoDto>(result));

    }

    /// <summary>
    /// Atualizar um veículo
    /// </summary>
    /// <param name="veiculo"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> AtualizarVeiculo(VeiculoDto veiculo)
    {
        if (!ModelState.IsValid || !veiculo.Id.HasValue)
        {
            if (ModelState.ErrorCount < 1)
                ModelState.AddModelError("error", "Id invalid");

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        MapearLogin(veiculo);

        var result = await _repository.Update(_mapper.Map<Veiculo>(veiculo));
        if (result == null)
            NoContent();
        return Ok(_mapper.Map<VeiculoDto>(result));

    }

    /// <summary>
    /// Desativar um veículo
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("DesativarVeiculo")]
    public async Task<IActionResult> DesativarVeiculo(Guid id)
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
        return Ok(_mapper.Map<VeiculoDto>(result));
    }

    /// <summary>
    /// Deletar um veículo
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeletarVeiculo(Guid id)
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
