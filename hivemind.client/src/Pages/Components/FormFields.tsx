import FormControl from '@mui/material/FormControl';
import TextField from '@mui/material/TextField';
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import InputLabel from '@mui/material/InputLabel';
import Box from '@mui/material/Box';
import type { SelectChangeEvent } from '@mui/material/Select';
import { useState } from 'react';


export type CustomFormFieldTypes = 'Text' | 'Number' | 'Date' | 'Select' | 'Time';

export interface CustomFormField {
    name: string;
    initialValue: string | number | boolean;
    type: CustomFormFieldTypes;
    validator?: (x: string | number | boolean) => boolean;
    valid?: boolean;
    options?: string[];
    required?: boolean;
}

const CreateFields = (field: CustomFormField, index: number, handle: (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement> | SelectChangeEvent<string> | SelectChangeEvent<number> | SelectChangeEvent<boolean>) => void) => {
    switch (field.type) {
        case 'Time': return (
            <FormControl sx={{ m: 2, pb: 2 }} key={index} >

            </FormControl>
        )
        case 'Text': return (
            <FormControl sx={{ m: 2, pb: 2 }} key={index} >
                <TextField
                    error={!field.valid}
                    required={field.required}
                    variant="filled"
                    name={field.name}
                    label={field.name}
                    defaultValue={field.initialValue}
                    onChange={handle}
                />
            </FormControl>
        )
        case 'Number': return (
            <FormControl sx={{ m: 2, pb: 2 }} key={index} >
                <TextField
                    error={!field.valid}
                    required={field.required}
                    type="number"
                    variant="filled"
                    name={field.name}
                    label={field.name}
                    defaultValue={field.initialValue}
                    onChange={handle}
                />
            </FormControl>
        )
        case 'Select': return (
            <FormControl sx={{ m: 2, pb: 2, minWidth: 200 }} key={index} >
                <InputLabel>{field.name}</InputLabel>
                <Select
                    required={field.required}

                    variant="filled"
                    name={field.name}
                    label={field.name}
                    defaultValue={field.initialValue}
                    onChange={handle}
                >
                    {field.options && field.options.map((type: string, index: number) => (<MenuItem value={index}>{type}</MenuItem>))}

                </Select>
            </FormControl>
        )
        default: return (<p>{field.name}</p>)
    }
}

export interface FieldData {
    name: string,
    value: string | number | boolean
}

interface FormFieldsProps {
    fields: CustomFormField[];
    handleInputChanges: (data: FieldData) => void;
}

const CustomFormFields = ({ fields, handleInputChanges }: FormFieldsProps) => {

    const [fieldsLocal, setFieldsLocal] = useState(fields);

    const handleFieldChanges = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement> | SelectChangeEvent<string> | SelectChangeEvent<number> | SelectChangeEvent<boolean>) =>
    {
        const name = event.target.name;
        const value = event.target.value;

        fieldsLocal.forEach(field => {
            if (field.name == name) field.valid = field.validator ? field.validator(value) : false
        });

        setFieldsLocal(fieldsLocal);

        handleInputChanges({name, value})
    }

    return (
        <Box component="form">
            {fieldsLocal && fieldsLocal.map((field, index) => CreateFields(field, index, handleFieldChanges))}
        </Box>
    )
}

export default CustomFormFields;