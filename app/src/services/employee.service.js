import {baseUrl} from "../helpers/constants"

const employeeControllerBase = '/api/v1/Employees'

export async function getEmployee(id){
    const raw = await fetch(`${baseUrl}${employeeControllerBase}/${id}`,
    {
        method: 'GET'
    });
    const response = await raw.json();
    return response;
}

export async function deleteEmployee(id){
    const raw = await fetch(`${baseUrl}${employeeControllerBase}/${id}`,
        {
            headers: {
                'Access-Control-Allow-Methods': 'DELETE',
            },
            method: 'DELETE'
        }
    );
    const response = await raw.json();
    return response;

}

export async function updateEmployee(id, employee){
    const raw = await fetch(`${baseUrl}${employeeControllerBase}/${id}`,
        {
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(employee),
            method: 'PUT'
        }
    );
    const response = await raw.json();
    return response;
}

export async function addEmployee(employee){
    const raw = await fetch(`${baseUrl}${employeeControllerBase}`,
        {
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(employee),
            method: 'POST'
        }
    );
    const response = await raw.json();
    return response;
}

export async function getPaycheck(id){
    const raw = await fetch(`${baseUrl}${employeeControllerBase}/${id}/paycheck`,
        {
            method: 'GET'
        }
    );
    const response = await raw.json();
    return response;
}

export async function getEmployeeDependents(id){
    const raw = await fetch(`${baseUrl}${employeeControllerBase}/${id}/dependents`,
        {
            method: 'GET'
        }
    );
    const response = await raw.json();
    return response;
}

export async function getAllEmployees() {
    const raw = await fetch(`${baseUrl}${employeeControllerBase}`,
    {
        method: 'GET'
    });
    const response = await raw.json();
    return response;
    // if (response.success) {
    //     //setEmployees(response.data);
    //     //setError(null);
    // }
    // else {
    //     //setEmployees([]);
    //     //setError(response.error);
    // }
};