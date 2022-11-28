import {baseUrl} from "../helpers/constants"

const dependentControllerBase = '/api/v1/Dependents'

export async function deleteDependent(id){
    const raw = await fetch(`${baseUrl}${dependentControllerBase}/${id}`,
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

export async function updateDependent(id, dependent){
    const raw = await fetch(`${baseUrl}${dependentControllerBase}/${id}`,
        {
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(dependent),
            method: 'PUT'
        }
    );
    const response = await raw.json();
    return response;
}

export async function addDependent(dependent){
    const raw = await fetch(`${baseUrl}${dependentControllerBase}`,
        {
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(dependent),
            method: 'POST'
        }
    );
    const response = await raw.json();
    return response;
}