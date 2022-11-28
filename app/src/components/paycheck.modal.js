import React from "react";
import Modal from 'react-modal'
import { getPaycheck } from "../services/employee.service";
import { currencyFormat } from "../helpers/constants";

function PaycheckModal(props){
    const [paycheck, setPaycheck] = React.useState(null); // todo add type
    function afterModalOpen(e){
        const _getPaycheck = async () => {
            const response = await getPaycheck(props.data.id);
            setPaycheck(response.data);
         }
         _getPaycheck();
        props.onAfterOpen(e, "opened!")
    }

    function onModalClose(e){
        props.onCloseModal(e);
    }

    return (
        <div>
            <Modal
            isOpen={props.IsModalOpen}
            onAfterOpen={e => afterModalOpen(e)}
            ariaHideApp={false}
            >
                <h2>Paycheck for {props.data.firstName} {props.data.lastName} </h2>            
                <div>Payment: {currencyFormat(paycheck?.payment)}</div>
                <div>Total Benefits Cost: {currencyFormat(paycheck?.totalBenefitsCost)}</div>
                <div>Employee Benefits Cost: {currencyFormat(paycheck?.employeeBenefitCost)}</div>
                <div>Dependents Benefits Cost: {currencyFormat(paycheck?.dependentsBenefitCost)}</div>
                <div>Salary Surcharge: {currencyFormat(paycheck?.salarySurchargeCost)}</div>
                <div>Dependents Over 50 Cost: {currencyFormat(paycheck?.dependentsOverFiftyCost)}</div>
                <button onClick={e=> onModalClose(e)}>Close</button>
            </Modal>
        </div>
    );
}

export default PaycheckModal;