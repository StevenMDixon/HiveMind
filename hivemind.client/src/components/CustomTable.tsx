import { TableContainer, Table, TableHead, TableRow, TableCell, TableBody, Button } from "@mui/material";
//import ErrorBoundary from "../components/ErrorBoundary";
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import IconButton from "@mui/material/IconButton";
import RefreshIcon from '@mui/icons-material/Refresh';

import RoundedLoadingFiller from './RoundedLoadingFiller';

export interface CellData<T> {
    key: string,
    name: string
    align?: "left" | "right" | "center";
    format?: (item: string) => string;
    action?: (item: T) => void;
    icon?: "Edit" | "Delete";
    disabled?: (i: T) => boolean
}

interface CustomTableProps<T> {
    data: T[] | undefined;
    columns: CellData<T>[];
    actionColumns?: CellData<T>[];
    handleRetry: () => void;
    isLoading: boolean;
}

const CustomIcon = (iconName: string) => {
    switch (iconName) {
        case 'Edit': return <EditIcon />
        case 'Delete': return <DeleteIcon />
        case 'Refresh': return <RefreshIcon />
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
                            <IconButton key={ac.key} onClick={() => ac.action?.(item)} disabled={ac.disabled ? ac.disabled(item): false}> 
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

export const CustomTable = <T,>({ data, columns, actionColumns, isLoading }: CustomTableProps<T>) => {
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
                    {isLoading ? <RoundedLoadingFiller size={10} width={columns.length + (actionColumns ? 1 : 0)}></RoundedLoadingFiller> :
                            data && data.map((item, index) => <CustomTableRow key={index} item={item} columns={columns} actionColumns={actionColumns} />)
                    }
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default CustomTable;