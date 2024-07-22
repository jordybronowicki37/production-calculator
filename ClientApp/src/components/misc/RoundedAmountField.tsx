import React from "react";

const precision = 2;

type NodeAmountFieldProps = {
    amount: number;
}

export function RoundedAmountField({amount} : NodeAmountFieldProps): React.JSX.Element {
    return <div>{+amount.toFixed(precision)}</div>;
}
