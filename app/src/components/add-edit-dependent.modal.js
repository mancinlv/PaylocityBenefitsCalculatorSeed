import React from "react";
import Modal from 'react-modal';
import {format} from 'date-fns';
import { addDependent, updateDependent } from "../services/dependent.service";

function AddEditDependentModal(props){
    const [dependent, setDependent] = React.useState(null);  // TODO add type - LVM
    const [currentEmployeeId, setEmployeeId] = React.useState(0);
    const [error, setError] = React.useState(null);

    //TODO Use instead of hardcoded options below - LVM
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
        setDependent(null);
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
        setError(null);
        const _saveDependent = async () => {
            dependent.employeeId = currentEmployeeId;
            dependent.relationship = dependent.relationship === undefined ? 1 : parseInt(dependent.relationship);

                const response = dependent.id === undefined ? await addDependent(dependent) : await updateDependent(dependent.id, dependent);
                if(response.success){
                    const filtered = response.data.filter(x => x.employeeId === currentEmployeeId);
                    props.onSave(filtered);
                    setDependent(null);
                }
                else{
                    setError(response.message)
                }
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
                {error && <h1>{error}</h1>}
        </div>
            </Modal>
        </div>
    );
}

export default AddEditDependentModal;