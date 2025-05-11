namespace MediStoS.Controllers;

using AutoMapper;
using MediStoS.Database.Models;
using MediStoS.Database.Repository.BatchRepository;
using MediStoS.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("api/[controller]")]
[ApiController]
public class BatchController(IBatchRepository batchRepository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Handles a request of getting a batch by its id 
    /// </summary>
    /// <param name="id">Batch id<see cref="int"/></param>
    /// <returns>Ok with a batch object, or an error<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
    [Authorize]
    [Route("{id:int}")]
    [SwaggerOperation("Get batch object by it's id")]
    [ProducesResponseType(typeof(BatchDto), 200)]
    public async Task<IActionResult> GetBatch(int id)
    {
        BatchDto? batch = await batchRepository.GetBatchByIdAsync(id);
        if (batch == null)
        {
            return NotFound($"Batch with the id of {id} was not found");
        }

        return Ok(batch);
    }

    /// <summary>
    /// Handles a request of creating a new batch
    /// </summary>
    /// <param name="batchDto">Batch model<see cref="BatchCreateDto"/></param>
    /// <returns>Ok or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpPost]
    [Route("")]
    [Authorize(Roles = "Manager,Admin,DBAdmin")]
    [SwaggerOperation("Create a batch object")]
    public async Task<IActionResult> CreateBatch([FromBody] BatchCreateDto batchDto)
    {
        if (batchDto == null)
        {
            return BadRequest($"Batch was not added, because data in request body was corrupted");
        }

        string response = await batchRepository.ValidateBatchCreateDtoAsync(batchDto);
        if (!string.IsNullOrEmpty(response))
        {
            return BadRequest($"Batch was not created. The reason:{response}");
        }

        Batch batch = mapper.Map<Batch>(batchDto);
        bool result = await batchRepository.AddBatchAsync(batch);
        if (!result) return BadRequest("Batch was not added");
        return Ok();
    }

    /// <summary>
    /// Handles a request of deleting a batch by its id
    /// </summary>
    /// <param name="id">Batch id<see cref="int"/></param>
    /// <returns>Ok or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "Manager,Admin,DBAdmin")]
    [SwaggerOperation("Delete a batch object by it's id")]
    public async Task<IActionResult> DeleteBatch(int id)
    {
        bool result = await batchRepository.DeleteBatchByIdAsync(id);
        if (!result) return BadRequest("Batch was not deleted");
        return Ok();
    }

    /// <summary>
    /// Handles a request of updating batch by its id with new model
    /// </summary>
    /// <param name="batchDto">Batch new model<see cref="BatchCreateDto"/></param>
    /// <param name="id">Batch id<see cref="int"/></param>
    /// <returns>Ok with an updated model, or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpPut]
    [Route("edit")]
    [Authorize(Roles = "Manager,Admin,DBAdmin")]
    [SwaggerOperation("Updates batch")]
    [ProducesResponseType(typeof(Batch), 200)]
    public async Task<ActionResult<BatchDto>> UpdateBatch(BatchDto batchDto)
    {
        if (batchDto == null)
        {
            return BadRequest("Couldn't get updated batch. Data was corrupted");
        }

        var result = await batchRepository.UpdateBatch(batchDto);
        if (!result) return BadRequest("Batch was not updated");
        return Ok(batchDto);
    }

    /// <summary>
    /// Handles a request of getting a list of batches for specific medicine by medicine id
    /// </summary>
    /// <param name="medicineId">Medicine id<see cref="int"/></param>
    /// <returns>Ok with a list of batches<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
    [Route("{medicineId:int}/batches")]
    [Authorize]
    [SwaggerOperation("Get all batches, made for specified medicine")]
    [ProducesResponseType(typeof(List<BatchDto>), 200)]
    public async Task<IActionResult> GetBatches(int medicineId)
    {
        return Ok(await batchRepository.GetBatchesByMedicineId(medicineId));
    }
}
