import { TableContainer, Table, TableHead, TableRow, TableCell, TableBody, Typography, Button } from "@mui/material";
import { use, Suspense } from "react";
import ErrorBoundary from "../../Components/ErrorBoundary";
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import IconButton from "@mui/material/IconButton";

import RoundedLoadingFiller from './RoundedLoadingFiller';

export interface CellData<T> {
    key: string,
    name: string
    align?: "left" | "right" | "center";
    format?: (item: string) => string;
    action?: (item: T) => void;
    icon?: "Edit" | "Delete";
}

interface CustomTableProps<T> {
    dataPromise: Promise<T[]>;
    columns: CellData<T>[];
    actionColumns?: CellData<T>[];
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

const CustomIcon = (iconName: string) => {
    switch (iconName) {
        case 'Edit': return <EditIcon />
        case 'Delete': return <DeleteIcon />
    }
}

const CustomTableRow = <T,>({ item, columns, actionColumns }: { item: T, columns: CellData<T>[], actionColumns?: CellData<T>[]}) => {
    return (
        <TableRow>
            {columns.map(column => <CustomTableCell key={column.key} item={item} column={column} />)}

            {actionColumns &&
                <TableCell align={'right'} >
                    {actionColumns?.map(ac =>
                        ac.icon ? 
                            <IconButton key={ac.key} onClick={() => ac.action?.(item)}>
                                {CustomIcon(ac.icon)}
                            </IconButton>
                        : <Button key={ac.key} onClick={() => ac.action?.(item)}>{ac.name}</Button>)
                    }
                </TableCell>
            }
        </TableRow>
    )
}

const CustomTableCell = <T,>({ item, column }: { item: T, column: CellData<T> }) => {
    const value = item[column.key as keyof T] as unknown as string;

    return (
        <TableCell align={column.align}>
            {
               column.format ? column.format(value) : value
            }
        </TableCell>
    );
}

export const CustomTable = <T,>({ dataPromise, columns, actionColumns, handleRetry }: CustomTableProps<T>) => {
    return (
        <TableContainer  sx={{mt: 2, p: 2}} >
            <Table>
                <TableHead>
                    <TableRow>
                        {[...columns].map(column => (<TableCell key={column.key} align={column.align}>{column.name}</TableCell>))}
                        {actionColumns && <TableCell key={'action-column'} align={'right'}>{'Action'}</TableCell>}
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
                            <RoundedLoadingFiller size={10} width={columns.length + (actionColumns ? 1 : 0)}></RoundedLoadingFiller>
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