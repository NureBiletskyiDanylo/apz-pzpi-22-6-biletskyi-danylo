namespace MediStoS.Controllers;

using AutoMapper;
using MediStoS.Database.Models;
using MediStoS.Database.Repository.MedicineRepository;
using MediStoS.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("api/[controller]")]
[ApiController]
public class MedicineController(IMedicineRepository medicineRepository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Handles a request of getting a medicine object by its id
    /// </summary>
    /// <param name="id">Medicine id<see cref="int"/></param>
    /// <returns>Ok with medicine object or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
    [Route("{id}")]
    [Authorize]
    [SwaggerOperation("Get medicine object by it's id")]
    [ProducesResponseType(typeof(Medicine), 200)]
    public async Task<IActionResult> GetMedicine(int id)
    {
        var medicine = await medicineRepository.GetMedicine(id);
        if (medicine == null)
        {
            return NotFound($"Medicine with Id={id} was not found");
        }
        return Ok(medicine);
    }

    /// <summary>
    /// Handles a request of creating a medicine
    /// </summary>
    /// <param name="medicineDto">Medicine model<see cref="MedicineCreateDto"/></param>
    /// <returns>Ok or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpPost]
    [Authorize(Roles = "Manager,Admin,DBAdmin")]
    [Route("")]
    [SwaggerOperation("Create a new medicine")]
    public async Task<IActionResult> CreateMedicine([FromBody] MedicineCreateDto medicineDto)
    {
        if (medicineDto == null)
        {
            return BadRequest($"Medicine was not added, because data in request body was corrupted");
        }

        Medicine medicine = mapper.Map<Medicine>(medicineDto);
        var result = await medicineRepository.AddMedicineAsync(medicine);
        if (!result) return BadRequest("Medicine was not added");
        return Ok();
    }

    /// <summary>
    /// Handles a request of deleting a medicine by its id
    /// </summary>
    /// <param name="id">Medicine id<see cref="int"/></param>
    /// <returns>Ok or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpDelete]
    [Authorize(Roles = "Manager,Admin,DBAdmin")]
    [Route("{id:int}")]
    [SwaggerOperation("Delete medicine by it's id")]
    public async Task<IActionResult> DeleteMedicine(int id)
    {
        var result = await medicineRepository.DeleteMedicineByIdAsync(id);
        if (!result) return BadRequest("Medicine was not deleted");
        return Ok();
    }

    /// <summary>
    /// Handles a request of updating a medicine with new data by its id
    /// </summary>
    /// <param name="model">Updated medicine model<see cref="MedicineCreateDto"/></param>
    /// <param name="id">Medicine id<see cref="int"/></param>
    /// <returns>Ok with an updated medicine, or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpPut]
    [Authorize(Roles = "Manager,Admin,DBAdmin")]
    [Route("edit")]
    [SwaggerOperation("Update medcine specified by it's id")]
    [ProducesResponseType(typeof(Medicine), 200)]
    public async Task<ActionResult> UpdateMedicine(MedicineDto medicineDto)
    {
        if (medicineDto == null)
        {
            return BadRequest("Coudln't get updated medicine. Data was corrupted");
        }

        var result = await medicineRepository.UpdateMedicineAsync(medicineDto);
        if (!result) return BadRequest("Medicine was not updated");
        return Ok();
    }

    /// <summary>
    /// Handles a request of getting a list of all medicines
    /// </summary>
    /// <returns>Ok with a list of medicines<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
    [Authorize]
    [Route("")]
    [SwaggerOperation("Get all medicines in a system")]
    [ProducesResponseType(typeof(List<Medicine>), 200)]
    public async Task<IActionResult> GetMedicines()
    {
        return Ok(await medicineRepository.GetMedicines());
    }

    [HttpGet]
    [Authorize]
    [Route("{id:int}-with")]
    [SwaggerOperation("Get medicine with batches")]
    public async Task<ActionResult> GetMedicineWithBatches(int id)
    {
        return Ok(await medicineRepository.GetMedicine(id, true));    
    }

}
