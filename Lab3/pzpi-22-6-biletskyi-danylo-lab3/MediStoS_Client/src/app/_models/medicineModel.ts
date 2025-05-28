import { BatchModel } from "./batchModel";

export interface MedicineModel{
    id: number;
    name: string;
    manufacturer: string;
    description: string;
    max_temperature: number;
    min_temperature: number;
    max_humidity: number;
    min_humidity: number;
    batches: BatchModel[];
}