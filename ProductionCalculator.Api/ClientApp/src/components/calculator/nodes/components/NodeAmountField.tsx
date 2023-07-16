const precision = 2;

type NodeAmountFieldProps = {
    amount: number;
}

export function NodeAmountField({amount} : NodeAmountFieldProps) {
    return <div>{+amount.toFixed(precision)}</div>;
}
