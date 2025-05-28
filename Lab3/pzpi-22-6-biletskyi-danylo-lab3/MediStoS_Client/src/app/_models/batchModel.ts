export interface BatchModel {
    id: number;
    batch_number: string;
    quantity: number;
    manufacture_date: Date;
    expiration_date: Date;
    email: string;
}