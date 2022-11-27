import { useState, useEffect } from 'react';
import Employee from './components/Employee';
import { deleteEmployee, getAllEmployees }  from '../src/services/employee.service'
import AddEditEmployeeModal from './components/addeditEmployee.modal';

const EmployeeListing = () => {
    const [employees, setEmployees] = useState([]);
    const [editOpen, setAddOpen] = useState(false);
    //const [error, setError] = useState(null);

    useEffect(() => {
        const _getEmployees = async () => {
            const response = await getAllEmployees();
            setEmployees(response.data);
         }
         _getEmployees();
    }, []);

    function handleDeleteEmployee(id) {
        const _deleteEmployee = async () => {
            const response = await deleteEmployee(id);
            setEmployees(response.data);
         }
         _deleteEmployee();
    }

    function handleEmployeesUpdate(data){
        setEmployees(data);
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
        setEmployees(data);
        setAddOpen(false);
    }

    return (
    <div className="employee-listing">
        <table className="table caption-top">
            <caption>Employees</caption>
            <thead className="table-dark">
                <tr>
                    <th scope="col">Id</th>
                    <th scope="col">LastName</th>
                    <th scope="col">FirstName</th>
                    <th scope="col">DOB</th>
                    <th scope="col">Salary</th>
                    <th scope="col">Dependents</th>
                    <th scope="col">Actions</th>
                    <th scope="col"></th>
                    <th scope="col"></th>
                </tr>
            </thead>
            <tbody>
            {employees.map( e => (
                <Employee
                    key={e.id}
                    id={e.id}
                    firstName={e.firstName}
                    lastName={e.lastName}
                    dateOfBirth={e.dateOfBirth}
                    salary={e.salary}
                    dependents={e.dependents}
                    onUpdateEmployees={handleEmployeesUpdate}
                    onDelete={(ev) => handleDeleteEmployee(e.id)}
                />
            ))}
            </tbody>
        </table>

        <AddEditEmployeeModal
        data={null}
        IsModalOpen={editOpen}
        onCloseModal={handleCloseModal}
        onAfterOpen={handleAfterOpen}
        onSaveEmployee={handleAfterAdd}
        />
        <button type="button" className="btn btn-primary" onClick={openAddModal}>Add Employee</button>
    </div>
    );
};

export default EmployeeListing;