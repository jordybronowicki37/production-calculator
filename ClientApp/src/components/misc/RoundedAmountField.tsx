const precision = 2;

type NodeAmountFieldProps = {
    amount: number;
}

export function RoundedAmountField({amount} : NodeAmountFieldProps) {
    return <div>{+amount.toFixed(precision)}</div>;
}
