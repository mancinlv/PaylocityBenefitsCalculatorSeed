import React from "react";
import { format } from 'date-fns';
import AddEditDependentModal from './add-edit-dependent.modal';

const Dependent = (props) => {
    const firstName = props.data.firstName || '';
    const lastName = props.data.lastName || '';
    const dateOfBirth = format(new Date(props.data.dateOfBirth), 'yyyy-MM-dd');
    const relationship = setRelationshipString(props.data.relationship);

    const [editOpen, setEditOpen] = React.useState(false);

    //TODO handle this better - LVM
    function setRelationshipString(relationship){
        switch(relationship){
            case 1:
                return 'Spouse'
            case 2:
                return 'Domestic Partner'
            default:
                return 'Child'
        }
    }

    function openEditModal(){
        setEditOpen(true);
    }

    function handleCloseModal(event){
        setEditOpen(false);
    }

    function handleAfterOpen(event, data){
        console.log(event, data);
    }
    
    function handleAfterEdit(data){
        props.onUpdateDependent(data);
        setEditOpen(false);
    }

    return (
        <tr>
            <th scope="row">{props.data.id}</th>
            <td>{lastName}</td>
            <td>{firstName}</td>
            <td>{dateOfBirth}</td>
            <td>{relationship}</td>
            <td>
                <AddEditDependentModal 
                data={props.data}
                employeeId={props.data.employeeId}
                IsModalOpen={editOpen}
                onCloseModal={handleCloseModal}
                onAfterOpen={handleAfterOpen}
                onSave={handleAfterEdit}
                />
                <button onClick={openEditModal}>Edit</button>

            </td>
            <td><button className="btn btn-danger btn-sm" onClick={(e) => props.onDelete(e)}>Delete</button></td>
        </tr>
    );
};

export default Dependent;