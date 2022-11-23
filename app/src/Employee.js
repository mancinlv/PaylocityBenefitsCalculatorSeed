import { currencyFormat } from "./Constants";

const Employee = (props) => {
    const firstName = props.firstName || '';
    const lastName = props.lastName || '';
    const salary = props.salary || 0;
    return (
        <tr>
            <th scope="row">{props.id}</th>
            <td>{lastName}</td>
            <td>{firstName}</td>
            <td>{props.dateOfBirth}</td>
            <td>{currencyFormat(salary)}</td>
            <td>{props.dependents?.length || 0}</td>
            <td>
                <a href="#" data-bs-toggle="modal" data-bs-target={`#${props.editModalId}`}>Edit</a>  <a href="#">Delete</a>
            </td>
        </tr>
    );
};

export default Employee;