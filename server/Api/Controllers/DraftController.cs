using Api.Services;
using Api.Models.Dtos.Requests;
using Api.Models.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/draft")]
public class DraftController(IDraftService service) : ControllerBase
{
    private readonly IDraftService service = service;

    [HttpGet]
    [Route("")]
    public IEnumerable<Draft> List() => service.List();

    [HttpPost]
    [Route("")]
    public async Task<long> Create(DraftFormData data) => await service.Create(data);

    [HttpGet]
    [Route("{id}")]
    public DraftDetail Get(long id) => service.GetById(id);

    [HttpPut]
    [Route("{id}")]
    public async Task Update(long id, DraftFormData data) => await service.Update(id, data);

    [HttpDelete]
    [Route("{id}")]
    public async Task Delete(long id) => await service.Delete(id);
}
