import React from "react";
import { format } from 'date-fns';
import { currencyFormat } from "../Constants";
import PaycheckModal from "./paycheck.modal";
import AddEditEmployeeModal from "./addeditEmployee.modal";

const Employee = (props) => {
    const firstName = props.firstName || '';
    const lastName = props.lastName || '';
    const salary = props.salary || 0;
    const dateOfBirth = format(new Date(props.dateOfBirth), 'yyyy-MM-dd');

    const [paycheckOpen, setIsOpen] = React.useState(false);
    const [editOpen, setEditOpen] = React.useState(false);

    function openPaycheckModal(){
        setIsOpen(true);
    }

    function openEditModal(){
        setEditOpen(true);
    }

    function handleCloseModal(event){
        setIsOpen(false);
        setEditOpen(false);
    }

    function handleAfterOpen(event, data){
        console.log(event, data);
    }
    
    function handleAfterEdit(data){
        props.onUpdateEmployees(data);
        setEditOpen(false);
    }

    return (
        <tr>
            <th scope="row">{props.id}</th>
            <td>{lastName}</td>
            <td>{firstName}</td>
            <td>{dateOfBirth}</td>
            <td>{currencyFormat(salary)}</td>
            <td>{props.dependents?.length || 0}</td>
            <td>
                <AddEditEmployeeModal 
                data={props}
                IsModalOpen={editOpen}
                onCloseModal={handleCloseModal}
                onAfterOpen={handleAfterOpen}
                onSaveEmployee={handleAfterEdit}
                />
                <button onClick={openEditModal}>Edit</button>

            </td>
            <td>
                <PaycheckModal 
                data={props}
                IsModalOpen={paycheckOpen}
                onCloseModal={handleCloseModal}
                onAfterOpen={handleAfterOpen}
                />
                <button onClick={openPaycheckModal}>Paycheck</button>

            </td>
            <td><button className="btn btn-danger btn-sm" onClick={(e) => props.onDelete(e)}>Delete</button></td>
        </tr>
    );
};

export default Employee;