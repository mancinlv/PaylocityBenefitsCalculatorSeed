import React from "react";
import Modal from 'react-modal';
import {format} from 'date-fns';
import { currencyFormat } from "../utils/constants"; //TODO make this work with input fields - LVM
import { addEmployee, updateEmployee } from "../services/employee.service";

function AddEditEmployeeModal(props){
    const [employee, setEmployee] = React.useState(null);  // todo add type

    
    function afterModalOpen(e){
        setEmployee({
            ...props.data,
            dateOfBirth: format(new Date(props.data?.dateOfBirth), 'yyyy-MM-dd')
        });
        props.onAfterOpen(e, "opened!")
    }

    function onModalClose(e){
        
        props.onCloseModal(e);
    }

    function handleChange(event){
        const target = event.target;
        const value = target.value;
        const name = target.name;
    
        setEmployee({
            ...employee,
          [name]: value
        });
    }

    function onSave(e){
        const _setEmployee = async () => {
            const response = employee.id === undefined ? await addEmployee(employee) : await updateEmployee(employee.id, employee);
            props.onSaveEmployee(response.data);
            
         }
         _setEmployee();  
    }
    
    return (
        <div>
            <Modal
            isOpen={props.IsModalOpen}
            onAfterOpen={e => afterModalOpen(e)}
            ariaHideApp={false}
            >
            <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                        <h1 className="modal-title fs-5" id="add-employee-modal-label">Edit Employee</h1>
                    </div>
                    <div className="modal-body">
                        <form>
                        <label>
                            First Name: 
                        <input type="text" name="firstName" value={employee?.firstName} onChange={(e) => handleChange(e)}/>
                        </label>
                        <label>
                            Last Name: 
                            <input type="text" name="lastName" value={employee?.lastName} onChange={(e) => handleChange(e)}/>
                        </label>
                        <label>
                            Date Of Birth: 
                            <input type="date" name="dateOfBirth" value={employee?.dateOfBirth} onChange={(e) => handleChange(e)}/>
                        </label>
                        <label>
                            Salary: 
                            <input type="text" name="salary" value={employee?.salary} onChange={(e) => handleChange(e)}/>
                        </label>
                        </form>
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary"  onClick={e=> onModalClose(e)}>Close</button>
                        <button type="button" className="btn btn-primary" onClick={e=> onSave(e)}>Save changes</button>
                    </div>
                </div>
        </div>
            </Modal>
        </div>
    );
}

export default AddEditEmployeeModal;