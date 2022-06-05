using AzurePlayApi.Models.WeightTracker;
using AzurePlayApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzurePlayApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeightsController : ControllerBase
{
    private readonly WeightsService _weightsService;

    public WeightsController(WeightsService weightsService) =>
        _weightsService = weightsService;

    [HttpGet]
    public async Task<List<Weight>> Get() =>
        await _weightsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Weight>> Get(string id)
    {
        var weight = await _weightsService.GetAsync(id);

        if (weight is null)
        {
            return NotFound();
        }

        return weight;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Weight newWeight)
    {
        await _weightsService.CreateAsync(newWeight);

        return CreatedAtAction(nameof(Get), new { id = newWeight.Id }, newWeight);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Weight updatedWeight)
    {
        var weight = await _weightsService.GetAsync(id);

        if (weight is null)
        {
            return NotFound();
        }

        updatedWeight.Id = weight.Id;

        await _weightsService.UpdateAsync(id, updatedWeight);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var weight = await _weightsService.GetAsync(id);

        if (weight is null)
        {
            return NotFound();
        }

        await _weightsService.RemoveAsync(id);

        return NoContent();
    }
}