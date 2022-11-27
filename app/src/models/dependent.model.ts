import { Person } from "./person.model";

export interface Dependent extends Person {
    id: number;
    employeeId: number;
    
    relationship: number; //todo how will the enum work here?
}
