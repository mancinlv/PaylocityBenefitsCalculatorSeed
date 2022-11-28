import React from "react";
import Modal from 'react-modal';
import Select from 'react-select';
import {format} from 'date-fns';
import { addDependent, updateDependent } from "../services/dependent.service";

function AddEditDependentModal(props){
    const [dependent, setDependent] = React.useState(null);  // todo add type
    const [currentEmployeeId, setEmployeeId] = React.useState(0);

    const relationships = [
        {label: "Spouse", value: 1},
        {label: "Domestic Partner", value: 2},
        {label: "Child", value: 3}
    ]
    
    function afterModalOpen(e){
        if(props.data !== null){
            setDependent({
                ...props.data,
                dateOfBirth: format(new Date(props?.data?.dateOfBirth), 'yyyy-MM-dd')
            });
        }
        
        setEmployeeId(props.employeeId);
        props.onAfterOpen(e, "opened!")
    }

    function onModalClose(e){
        props.onCloseModal(e);
    }

    function handleChange(event){
        const target = event.target;
        const value = target.value;
        const name = target.name;
    
        setDependent({
            ...dependent,
          [name]: value
        });
    }

    function onSave(e){
        const _saveDependent = async () => {
            dependent.employeeId = currentEmployeeId;
            const response = dependent.id === undefined ? await addDependent(dependent) : await updateDependent(dependent.id, dependent);
            const filtered = response.data.filter(x => x.employeeId === currentEmployeeId);
            props.onSave(filtered);
            
         }
         _saveDependent();  
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
                        <h1 className="modal-title fs-5" id="add-dependent-modal-label">Add or Edit Dependent</h1>
                    </div>
                    <div className="modal-body">
                        <form>
                        <label>
                            First Name: 
                        <input type="text" name="firstName" value={dependent?.firstName} onChange={(e) => handleChange(e)}/>
                        </label>
                        <label>
                            Last Name: 
                            <input type="text" name="lastName" value={dependent?.lastName} onChange={(e) => handleChange(e)}/>
                        </label>
                        <label>
                            Date Of Birth: 
                            <input type="date" name="dateOfBirth" value={dependent?.dateOfBirth} onChange={(e) => handleChange(e)}/>
                        </label>
                        <label>
                            Relationship:
                            <select name="relationship" value={dependent?.relationship} onChange={(e) => handleChange(e)}>
                                <option name="Spouse" value="1"> Spouse</option>
                                <option name="DomesticPartner" value="2">Domestic Partner</option>
                                <option name="Child" value="3">Child</option>
                            </select>
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

export default AddEditDependentModal;