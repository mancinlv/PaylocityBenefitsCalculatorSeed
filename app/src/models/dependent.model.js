import { Person } from "./person.model";

export class Dependent extends Person {
    id;
    employeeId;
    relationship; //todo how will the enum work here?
}
