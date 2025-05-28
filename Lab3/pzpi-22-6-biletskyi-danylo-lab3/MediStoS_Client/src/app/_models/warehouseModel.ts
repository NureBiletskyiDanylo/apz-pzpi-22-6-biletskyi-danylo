export interface WarehouseModel {
    id: number;
    name: string;
    address: string;
    max_temperature: number;
    min_temperature: number;
    max_humidity: number;
    min_humidity: number;
    created_at: Date;
}