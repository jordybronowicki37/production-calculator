const precision = 3;

type NodeAmountFieldProps = {
    amount: number;
}

export function NodeAmountField({amount} : NodeAmountFieldProps) {
    return <div>{+amount.toFixed(precision)}</div>;
}
