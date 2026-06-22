import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import Stack from '@mui/material/Stack';

import IconButton from '@mui/material/IconButton';
import Delete from '@mui/icons-material/DeleteOutline';
import EditIcon from '@mui/icons-material/Edit';
import CheckIcon from '@mui/icons-material/Check';
import CloseIcon from '@mui/icons-material/Close';

import RoundedLoadingFiller from '@/components/RoundedLoadingFiller'

import { useState, useMemo } from 'react'

import type { EnumOptionsObj } from '@/utilities/types';


export type EditingGridFieldTypes = 'Text' | 'Number' | 'Date' | 'Select' | 'Time' | 'Radio';

import FormFields from './FormFields';
import { type FieldData } from './FormFields';

export interface EditingGridColumns<T> {
    name: string;
    display: string;
    initialValue: string | number | boolean;
    type: EditingGridFieldTypes;
    validator?: (x: string | number | boolean) => boolean;
    valid?: boolean;
    options?: EnumOptionsObj | ((x: T) => EnumOptionsObj );
    required?: boolean;
    format?: (item: string | boolean | number) => string | number | boolean;
    disabled?: boolean | ((field: string, x: T) => boolean)
}

interface EditingGridProps<T> {
    gridItems?: T[],
    gridFieldColumns: EditingGridColumns<T>[],
    deleteItem?: (i: T) => void;
    saveItem: (i: T) => void;
    isLoading: boolean;
}

const EditingGrid = <T,>({ gridItems, gridFieldColumns, deleteItem, saveItem, isLoading} : EditingGridProps<T>)  => {

    const [selectedEditItem, setSelectedEditItem] = useState<T | null>(null);

    const handleStopEditing = () => {
        setSelectedEditItem(null);
    }

    const handleSaveEdit = (item: T) => {
        saveItem(item);
        setSelectedEditItem(null);
    }

    const handleRemoval = (item: T) => {
        if (deleteItem) {
            deleteItem(item)
        }
    }

    const handleSetEdit = (item: T) => {
        setSelectedEditItem(item);
    }

  return (
      <TableContainer component={Paper}>
          <Table sx={{ minWidth: 650 }} aria-label="simple table">
              <TableHead>
                  <TableRow>
                      {gridFieldColumns.map((column, index) => <TableCell align="right" key={index}>{column.display}</TableCell>)}
                      <TableCell align="right">Actions</TableCell>
                  </TableRow>
              </TableHead>
              <TableBody>
                  {isLoading ?
                      <RoundedLoadingFiller size={10} width={gridFieldColumns.length + 1}></RoundedLoadingFiller>
                      :
                      gridItems && gridItems.map((item, index) =>
                          selectedEditItem && selectedEditItem == item ?
                              <EditTableRow key={index} item={selectedEditItem} columns={gridFieldColumns} stopEdit={handleStopEditing} saveEdit={handleSaveEdit} /> :
                              <ViewTableRow key={index} item={item} columns={gridFieldColumns} setEdit={handleSetEdit} remove={deleteItem ? handleRemoval : undefined} />)
                  }
              </TableBody>
          </Table>
      </TableContainer>
  );
}

interface RowProps<T>{
    item: T;
    columns: EditingGridColumns<T>[];
}

interface ViewActionProps<T> {
    setEdit: (i: T) => void;
    remove?: (i: T) => void;
}

interface EditActionProps<T> {
    stopEdit: () => void;
    saveEdit: (i: T) => void;
}

function getProperty<Type, Key extends keyof Type>(obj: Type, key: Key | string) {
    return obj[key as Key];
}

const ViewTableRow = <T,>({ item, columns, setEdit, remove} : RowProps<T> & ViewActionProps<T>) => {
    return (
        <TableRow
            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
        >
            {columns.map((column, index) => {
                const columnValue = getProperty(item, column.name) as string | number | boolean
                return (<TableCell align="right" key={index}>{column.format ? column.format(columnValue) : columnValue}</TableCell>)
            })
            }
            <TableCell align="right">
                <Stack direction="row" justifyContent="right">
                <IconButton onClick={() => setEdit(item)}>
                    <EditIcon />
                </IconButton>
                    {remove && <IconButton onClick={() => remove(item)}>
                        <Delete />
                    </IconButton>}
                </Stack>
            </TableCell>
        </TableRow>
    )
}

const EditTableRow = <T,>({ item, columns, stopEdit, saveEdit }: RowProps<T> & EditActionProps<T>) => {
    const [data, setData] = useState<T>(item);

    const formFields = useMemo(() => {
        return columns.map(column => ({
            ...column,
            initialValue: getProperty(data, column.name) as string | number | boolean,
            valid: column.validator ? column.validator(getProperty(data, column.name) as string | number | boolean) : true,
            options: column.options && typeof column.options === 'function' ? column.options(data) : column.options,
            disabled: column.disabled && typeof column.disabled === 'function' ? column.disabled(column.name, data) : column.disabled,
        }));
    }, [data, columns]);

    const handleInputChanges = (data: FieldData) => {
        const { name, value } = data;
        setData(values => ({ ...values, [name]: value }))
    };

    return (
            <TableRow
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
        >
                <FormFields 
                    fields={formFields} 
                    handleInputChanges={handleInputChanges}
                    variant="standard"
                    disableLabels={true}
                    wrapper={(field, index) => <TableCell sx={{ m: 0, p: 0 }} align="right" key={index}>{field}</TableCell>}
                />
            <TableCell align="center"> 
                <Stack direction="row" justifyContent="right">
                    <IconButton onClick={() => saveEdit(data)} >
                        <CheckIcon />
                    </IconButton>
                    <IconButton onClick={() => stopEdit()}>
                        <CloseIcon />
                    </IconButton>
                    </Stack>
                    </TableCell>
            </TableRow>
    )
}

export default EditingGrid;