export interface BatchCreateModel {
    batch_number: string;
    quantity: number;
    manufacture_date: Date;
    expiration_date: Date;
    warehouse_id: number;
    medicine_id: number;
    user_id: number;
}