import { useState, useEffect } from 'react';
import { deleteDependent } from '../services/dependent.service';
import AddEditDependentModal from './add-edit-dependent.modal';
import Dependent from './dependent';

const DependentListing = (props) => {
    const [dependents, setDependents] = useState([]);
    const [editOpen, setAddOpen] = useState(false);
    const [currentEmployeeId, setEmployeeId] = useState(0);

    //known issue: React Hook useEffect has missing dependencies: 'props.currentEmployeeId' and 'props.dependents'. Either include them or remove the dependency array. If 'setDependents' needs the current value of 'props.dependents', you can also switch to useReducer instead of useState and read 'props.dependents' in the reducer
    useEffect(() => {
        setDependents(props.dependents);
        setEmployeeId(props.currentEmployeeId);
    }, []);

    function handleDeleteDependent(id) {
        const _deleteDependent = async () => {
            const response = await deleteDependent(id);
            const filtered = response.data.filter(x => x.employeeId === currentEmployeeId);
            setDependents(filtered);
         }
         _deleteDependent();
    }

    function HandleDependentsUpdate(data){
        setDependents(data);
    }

    function openAddModal(){
        setAddOpen(true);
    }

    function handleAfterOpen(event, data){
        console.log(event, data);
    }

    function handleCloseModal(event){
        setAddOpen(false);
    }

    function handleAfterAdd(data){
        setDependents(data);
        setAddOpen(false);
    }

    function closeDependents(){
        props.onHideDependents();
    }

    return (
    <div className="dependent-listing">
        <button type="button" className="btn btn-secondary btn-sm" onClick={closeDependents}>Back</button>
        <table className="table caption-top">
            <caption>Dependents</caption>
            <thead className="table-dark">
                <tr>
                    <th scope="col">Id</th>
                    <th scope="col">LastName</th>
                    <th scope="col">FirstName</th>
                    <th scope="col">DOB</th>
                    <th scope="col">Relationship</th>
                    <th scope="col">Actions</th>
                    <th scope="col"></th>
                </tr>
            </thead>
            <tbody>
            {dependents && dependents.map( e => (
                <Dependent
                    key={e.id}
                    data={e}
                    onUpdateDependent={HandleDependentsUpdate}
                    onDelete={(ev) => handleDeleteDependent(e.id)}
                />
            ))}
            </tbody>
        </table>
        {dependents?.length === 0 && <h2>Dependentless!</h2>}
        
        <AddEditDependentModal
        data={null}
        employeeId={currentEmployeeId}
        IsModalOpen={editOpen}
        onCloseModal={handleCloseModal}
        onAfterOpen={handleAfterOpen}
        onSave={handleAfterAdd}
        />
        <button type="button" className="btn btn-primary" onClick={openAddModal}>Add Dependent</button>
    </div>
    );
};

export default DependentListing;