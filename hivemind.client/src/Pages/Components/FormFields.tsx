import FormControl from '@mui/material/FormControl';
import TextField, { type TextFieldVariants } from '@mui/material/TextField';
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import InputLabel from '@mui/material/InputLabel';
import type { SelectChangeEvent } from '@mui/material/Select';
import { FormLabel, RadioGroup, FormControlLabel, Radio } from '@mui/material';
import type { JSX } from 'react';

import type { EnumOptionsObj } from '../../Types/General';

export type CustomFormFieldTypes = 'Text' | 'Number' | 'Date' | 'Select' | 'Time' | 'Radio';

export interface Option  {
    id: number,
    value: string | number | boolean
}

export interface CustomFormField {
    name: string;
    initialValue: string | number | boolean;
    type: CustomFormFieldTypes;
    validator?: (x: string | number | boolean) => boolean;
    valid?: boolean;
    options?: EnumOptionsObj;
    required?: boolean;
    display?: string;
}

const CreateFields = (field: CustomFormField, index: number, disableLabels: boolean, variant: string, fullWidth: boolean, handle: (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement> | SelectChangeEvent<string> | SelectChangeEvent<number> | SelectChangeEvent<boolean>) => void) => {
    switch (field.type) {
        case 'Time': return (
            <FormControl key={index} >

            </FormControl>
        )
        case 'Text': return (
            <TextField sx={{ m: 0, p: 0 }} key={index}
                error={!field.valid}
                label={!disableLabels ? field.display ? field.display : field.name  : ""}
                required={field.required}
                variant={variant as TextFieldVariants}
                name={field.name}
                value={field.initialValue}
                onChange={handle}
                fullWidth={fullWidth }
                />
        )
        case 'Number': return (
                <TextField sx={{ m: 0, pb: 0 }} key={index}
                    error={!field.valid}
                    required={field.required}
                    type="number"
                    variant={variant as TextFieldVariants}
                    name={field.name}
                    label={!disableLabels ? field.display ? field.display : field.name : ""}                    value={field.initialValue}
                    onChange={handle}
                    fullWidth={fullWidth}
                />
        )
        case 'Select': return (
            <FormControl sx={{ mr: 1, p: 0, minWidth: 100 }} key={index} fullWidth={fullWidth}>
                {!disableLabels && <FormLabel>{field.display ? field.display : field.name}</FormLabel>}
                <Select
                    required={field.required}
                    
                    variant={variant as TextFieldVariants}
                    name={field.name}
                    value={field.initialValue}
                    onChange={handle}
                >
                    {field.options && Object.keys(field.options).map((key: string) => (<MenuItem value={Number(key)}>{field.options && field.options[Number(key)]}</MenuItem>))}
                </Select>
            </FormControl>
        )
        case 'Radio': return (
            <FormControl sx={{ m: 0, p: 0, minWidth: 100 }} key={index} fullWidth={fullWidth}>
                {!disableLabels && <FormLabel>{field.display ? field.display : field.name}</FormLabel>}
                <RadioGroup
                    aria-labelledby="demo-radio-buttons-group-label"
                    value={field.initialValue}
                    name={field.name}
                    onChange={handle}
                    row
                >
                    {field.options && Object.keys(field.options).map((key: string) => <FormControlLabel key={Number(key)} value={field.options && field.options[Number(key)]} control={<Radio />} label={field.options && field.options[Number(key)]} />) }
                </RadioGroup>

                <InputLabel></InputLabel>
            </FormControl>
        )
        default: return (<p>{field.name}</p>)
    }
}

export { CreateFields };

export interface FieldData {
    name: string,
    value: string | number | boolean
}

interface FormFieldsProps {
    fields: CustomFormField[];
    handleInputChanges: (data: FieldData) => void;
    wrapper?: (field: JSX.Element, index: number) => JSX.Element;
    disableLabels?: boolean
    variant?: "standard" | "filled"
    fullWidth?: boolean
}

const CustomFormFields = ({ fields, handleInputChanges, wrapper, disableLabels, variant, fullWidth }: FormFieldsProps) => {

    const handleFieldChanges = (
        event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement> |
            SelectChangeEvent<string> |
            SelectChangeEvent<number> |
            SelectChangeEvent<boolean>
    ) => {
        const name = event.target.name;
        const value = event.target.value;

        handleInputChanges({ name, value });
    };

    return fields.map((field, index) => {
            const value = field.initialValue ?? "";

            const valid = field.validator
                ? field.validator(value)
            : true;

            const fieldElement = CreateFields(
                { ...field, initialValue: field.initialValue ?? "", valid },
                index,
                disableLabels ?? false,
                variant ?? 'filled',
                fullWidth ?? false,
                handleFieldChanges
            );

            return wrapper ? wrapper(fieldElement, index) : fieldElement;
        })
}

export default CustomFormFields;