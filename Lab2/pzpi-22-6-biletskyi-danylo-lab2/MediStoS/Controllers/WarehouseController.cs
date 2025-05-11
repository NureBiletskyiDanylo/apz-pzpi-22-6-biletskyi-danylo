namespace MediStoS.Controllers;

using AutoMapper;
using MediStoS.Database.Models;
using MediStoS.Database.Repository.WarehouseRepository;
using MediStoS.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController(IWarehouseRepository warehouseRepository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Handles a request of getting a warehouse object by its id 
    /// </summary>
    /// <param name="id">Warehouse id<see cref="int"/></param>
    /// <returns>Warehouse object or a message with an error:<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
    [Route("{id:int}")]
    [Authorize]
    [SwaggerOperation("Get warehouse object by it's id")]
    [ProducesResponseType(typeof(WarehouseDto), 200)]
    public async Task<IActionResult> GetWarehouse(int id)
    {
        var warehouse = await warehouseRepository.GetWarehouse(id);
        if (warehouse == null)
        {
            return NotFound($"Warehouse by id {id} was not found");
        }

        return Ok(warehouse);
    }

    /// <summary>
    /// Handles a request of creating a warehouse object on the server.
    /// </summary>
    /// <param name="model">JSON warehouse model that transforms into an object<see cref="WarehouseCreateDto"/></param>
    /// <returns>Successful result or an error: <see cref="Task{IActionResult}"/></returns>
    [HttpPost]
    [Route("")]
    [Authorize(Roles = "Admin,DBAdmin")]
    [SwaggerOperation("Create warehouse object")]
    public async Task<IActionResult> CreateWarehouse([FromBody] WarehouseCreateDto model)
    {
        if (model == null)
        {
            return BadRequest($"Warehouse was not added, because data in request body was corrupted");
        }

        Warehouse warehouse = mapper.Map<Warehouse>(model);
        warehouse.CreatedAt = DateTime.UtcNow;
        bool result = await warehouseRepository.AddWarehouseAsync(warehouse);
        if (!result) return BadRequest("Warehouse was not added successfully");
        return Ok();
    }

    /// <summary>
    /// Handles a request of deleting warehouse object from the server by its id
    /// </summary>
    /// <param name="id">Warehouse id<see cref="int"/></param>
    /// <returns>Ok or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Admin,DBAdmin")]
    [SwaggerOperation("Delete warehouse object by it's id")]
    public async Task<IActionResult> DeleteWarehouse(int id)
    {
        bool result = await warehouseRepository.DeleteWarehouseByIdAsync(id);
        if (!result) return BadRequest("Warehouse was not deleted successfully");
        return Ok();
    }

    /// <summary>
    /// Handles a request of updating a warehouse by its id and new model
    /// </summary>
    /// <param name="model">Warehouse updated data<see cref="WarehouseCreateDto"/></param>
    /// <param name="id">Warehouse id<see cref="int"/></param>
    /// <returns>Ok with warehouse object, or an error: <see cref="Task{IActionResult}"/></returns>
    [HttpPut]
    [Route("edit")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation("Update warehouse object specified by it's id")]
    [ProducesResponseType(typeof(Warehouse), 200)]
    public async Task<ActionResult> UpdateWarehouse([FromBody] WarehouseDto model)
    {
        var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
        if (emailClaim == null)
        {
            return BadRequest("Can't update an warehouse by a user whose email is unknown");
        }
        string email = emailClaim.Value;

        if (model == null)
        {
            return BadRequest("Coudln't get updated warehouse. Data was corrupted");
        }


        var result = await warehouseRepository.UpdateWarehouseAsync(model, email);
        if (!result) return BadRequest("Warehouse was not updated");


        return Ok();
    }

    /// <summary>
    /// Handles a request of getting all warehouses
    /// </summary>
    /// <returns>Ok with a list of warehouses (inspite empty or not)<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
    [Route("")]
    [Authorize]
    [SwaggerOperation("Get all warehouse objects")]
    [ProducesResponseType(typeof(List<WarehouseDto>), 200)]
    public async Task<IActionResult> GetWarehouses()
    {
        return Ok(await warehouseRepository.GetWarehouses());
    }
}
