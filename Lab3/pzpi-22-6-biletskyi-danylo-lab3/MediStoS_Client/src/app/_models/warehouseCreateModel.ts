export interface WarehouseCreateModel {
    name: string;
    address: string;
    max_temperature: number;
    min_temperature: number;
    max_humidity: number;
    min_humidity: number;
}