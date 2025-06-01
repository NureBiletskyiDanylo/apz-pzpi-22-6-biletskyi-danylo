using AutoMapper;
using MediStoS.Database.DatabaseContext;
using MediStoS.Database.Models;
using MediStoS.DataTransferObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace MediStoS.Database.Repository.WarehouseRepository;

public class WarehouseRepository(ApplicationDbContext context, IMapper mapper) : IWarehouseRepository
{
    public async Task<WarehouseDto?> GetWarehouse(int id)
    {
        var warehouse = await context.Warehouses.FindAsync(id);
        if (warehouse == null) throw new ArgumentNullException("Warehouse was not found");
        var warehouseDto = mapper.Map<WarehouseDto>(warehouse);
        return warehouseDto;
    }

    public async Task<bool> AddWarehouseAsync(Warehouse warehouse)
    {
        await context.Warehouses.AddAsync(warehouse);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> DeleteWarehouse(Warehouse warehouse)
    {
        context.Remove(warehouse);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> DeleteWarehouseByIdAsync(int id)
    {
        var warehouse = await context.Warehouses.FindAsync(id);
        if (warehouse == null) return false;
        context.Warehouses.Remove(warehouse);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateWarehouse(Warehouse warehouse)
    {
        context.Warehouses.Update(warehouse);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> UpdateWarehouseAsync(WarehouseDto warehouseDto, string emailOfChanger)
    {
        Warehouse? warehouse = await context.Warehouses.AsNoTracking().FirstOrDefaultAsync(w => w.Id == warehouseDto.Id);
        if (warehouse == null) return false;

        Warehouse updatedWarehouse = mapper.Map<Warehouse>(warehouseDto);
        updatedWarehouse.CreatedAt = warehouse.CreatedAt;
        updatedWarehouse.Batches = warehouse.Batches;
        updatedWarehouse.StorageViolations = warehouse.StorageViolations;
        updatedWarehouse.Sensors = warehouse.Sensors;

        context.Warehouses.Update(updatedWarehouse);
        bool success = await context.SaveChangesAsync() > 0;
        if (success)
        {
            LogChanges(warehouse, updatedWarehouse, emailOfChanger);
            return true;
        }
        return false;
    }

    public async Task<List<WarehouseDto>> GetWarehouses()
    {
        var warehouses = await context.Warehouses.ToListAsync();
        if (warehouses == null) return new List<WarehouseDto>();
        var warehouseDtos = mapper.Map<List<WarehouseDto>>(warehouses); 
        return warehouseDtos;
    }

    private void LogChanges(Warehouse oldWarehouse, Warehouse newWarehouse, string emailofChanger)
    {
        if ((newWarehouse.MaxTemperature != oldWarehouse.MaxTemperature) ||
    (newWarehouse.MinTemperature != oldWarehouse.MaxTemperature) ||
    (newWarehouse.MaxHumidity != oldWarehouse.MaxHumidity) ||
    (newWarehouse.MinHumidity != oldWarehouse.MinHumidity))
        {
            Log.Information(
                "Storage requirements of warehouse - {Warehouse} were changed by user - {User}. Old values: MaxTemperature: {OldMaxTemp}, MinTemperature: {OldMinTemp}, MaxHumidity: {OldMaxHum}, MinHumidity: {OldMinHum}. New values: MaxTemperature: {NewMaxTemp}, MinTemperature: {NewMinTemp}, MaxHumidity: {NewMaxHum}, MinHumidity: {NewMinHum}",
                oldWarehouse.Name,
                emailofChanger,
                oldWarehouse.MaxTemperature,
                oldWarehouse.MinTemperature,
                oldWarehouse.MaxHumidity,
                oldWarehouse.MinHumidity,
                newWarehouse.MaxTemperature,
                newWarehouse.MinTemperature,
                newWarehouse.MaxHumidity,
                newWarehouse.MinHumidity);
        }

    }
}
