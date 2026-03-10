import { TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, Typography, Button } from "@mui/material";
import { use, Suspense } from "react";
import ErrorBoundary from "../../Components/ErrorBoundary";
import { /*useNavigate*/ } from "react-router-dom";

export interface CellData {
    key: string,
    name: string
    align?: "left" | "right" | "center";
    format?: <T,>(item: T) => string;
    action?: <T,>(item: T) => void;
}

interface CustomTableProps<T> {
    dataPromise: Promise<T[]>;
    columns: CellData[];
    actionColumns?: CellData[];
    handleRetry: () => void;
}

const CustomTableBody = <T,>({ dataPromise, columns, actionColumns}: CustomTableProps<T>) => {
    const dataItems = use(dataPromise);

    return (
        <>
            {
                dataItems.map((item, index) => <CustomTableRow key={index} item={item} columns={columns} actionColumns={actionColumns} />)
            }
        </>
    )
}

const CustomTableRow = <T,>({ item, columns, actionColumns }: { item: T, columns: CellData[], actionColumns?: CellData[]}) => {
    return (
        <TableRow>
            {columns.map(column => <CustomTableCell key={column.key} item={item} column={column} />)}
            {actionColumns?.map(ac => <TableCell key={ac.key}><Button fullWidth  onClick={() => ac.action?.(item)}>Test</Button></TableCell>) }
        </TableRow>
    )
}

const CustomTableCell = <T,>({ item, column }: { item: T, column: CellData }) => {
    const value = item[column.key as keyof T] as unknown as string;

    return (
        <TableCell align={column.align}>
            {
               column.format ? column.format(value) : value
            }
        </TableCell>
    );
}

const CustomTable = <T,>({ dataPromise, columns, actionColumns, handleRetry }: CustomTableProps<T>) => {
    

    return (
        <TableContainer component={Paper} >
            <Table sx={{ minWidth: 650 }} size="small" aria-label="a">
                <TableHead>
                    <TableRow>
                        {[...columns, ...actionColumns ?? []].map(column => (<TableCell key={column.key} align={column.align}>{column.name}</TableCell>))}
                       
                    </TableRow>
                </TableHead>
                <TableBody>
                    <ErrorBoundary
                        fallback={(error, reset) => (
                            <TableRow>
                                <TableCell colSpan={10} align="center">
                                    <Typography sx={{ mb: 1 }}>
                                        {error.message}
                                    </Typography>
                                    <button onClick={() => { reset(); handleRetry(); }}>
                                        Retry
                                    </button>
                                </TableCell>
                            </TableRow>
                        )}
                    >
                        <Suspense fallback={
                            <TableRow>
                                <TableCell colSpan={10} align="center">Loading...</TableCell>
                            </TableRow>
                        }>
                            <CustomTableBody dataPromise={dataPromise} columns={columns} actionColumns={actionColumns} handleRetry={handleRetry}/>
                        </Suspense>
                    </ErrorBoundary>
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default CustomTable;