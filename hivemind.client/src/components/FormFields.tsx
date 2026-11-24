import FormControl from '@mui/material/FormControl';
import TextField from '@mui/material/TextField';
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import InputLabel from '@mui/material/InputLabel';
// import type { SelectChangeEvent } from '@mui/material/Select';
import { TimePicker } from '@mui/x-date-pickers/TimePicker';

import { FormLabel, RadioGroup, FormControlLabel, Radio, Switch, Checkbox } from '@mui/material';
import type { JSX } from 'react';

import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers';
import dayjs, { Dayjs } from 'dayjs';


export interface EnumOptionsObj {
    [key: number]: string | number | boolean; // Keys are numbers, values are strings
}

export type CustomFormFieldTypes = 'Text' | 'Number' | 'Date' | 'Select' | 'Time' | 'Radio' | 'Switch' | 'Checkbox' | 'MultiSelect';

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
    disabled?: boolean;
}

const createSelectOptions = (options: EnumOptionsObj) => {
    return Object.keys(options).map((key: string) => (<MenuItem value={Number(key)}>{options && options[Number(key)]}</MenuItem>))
}

interface CreateFieldsProps {
    field: CustomFormField;
    index: number;
    disableLabels: boolean;
    variant: string;
    fullWidth: boolean;
    handle: (event: ChangeEvent) => void;
}

const CreateFields = ({ field, index, disableLabels, fullWidth, handle }: CreateFieldsProps)   => {
    switch (field.type) {
        case 'Text': return (
            <TextField sx={{ m: 0, p: 0 }} key={index}
                error={!field.valid}
                label={!disableLabels ? field.display ? field.display : field.name : ""}
                required={field.required}
                name={field.name}
                value={field.initialValue}
                onChange={(e) => handle({name: e.target.name, type: e.target.type, value: e.target.value})}
                fullWidth={fullWidth}
                disabled={ field.disabled }
                />
        )
        case 'Number': return (
                <TextField sx={{ m: 0, pb: 0 }} key={index}
                    error={!field.valid}
                    required={field.required}
                    type="number"
                    name={field.name}
                label={!disableLabels ? field.display ? field.display : field.name : ""}
                value={field.initialValue}
                    onChange={(e) => handle({name: e.target.name, type: e.target.type, value: e.target.value})}
                    fullWidth={fullWidth}
                    disabled={field.disabled}
                />
        )
        case 'Switch': return (
            <FormControlLabel control={
                <Switch  onChange={(e) => handle({name: e.target.name, type: e.target.type, value: e.target.value})} checked={field.initialValue as boolean} name={field.name} />
            }
                label={field.display ? field.display : field.name}  />
        )
        case 'Checkbox': return (
            <FormControlLabel control={
                <Checkbox onChange={(e) => handle({name: e.target.name, type: e.target.type, value: e.target.value})} checked={field.initialValue as boolean} name={field.name} />
            }
            label={field.display ? field.display : field.name} />
        )
        case 'MultiSelect': return (
            <FormControl sx={{ mr: 1, p: 0, minWidth: 100 }} key={index} fullWidth={fullWidth}>
                {!disableLabels && <FormLabel>{field.display ? field.display : field.name}</FormLabel>}
                <Select
                    required={field.required}
                   
                    name={field.name}
                    value={field.initialValue}
                    onChange={(e) => handle({name: e.target.name, type: "MultiSelect", value: e.target.value})}
                    disabled={field.disabled}
                    multiple
                >
                    {field.options && createSelectOptions(field.options)}
                </Select>
            </FormControl>
        )
        case 'Select': return (
            <FormControl sx={{ mr: 1, p: 0, minWidth: 100 }} key={index} fullWidth={fullWidth}>
                {!disableLabels && <FormLabel>{field.display ? field.display : field.name}</FormLabel>}
                <Select
                    required={field.required}
                   
                    name={field.name}
                    value={field.initialValue}
                    onChange={(e) => handle({name: e.target.name, type: "Select", value: e.target.value})}
                    disabled={field.disabled}
                >
                    {field.options && createSelectOptions(field.options)}
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
                    onChange={(e) => handle({name: e.target.name, type: e.target.type, value: e.target.value})}
                    row
                >
                    {field.options && Object.keys(field.options).map((key: string) => <FormControlLabel key={Number(key)} value={field.options && field.options[Number(key)]} control={<Radio />} label={field.options && field.options[Number(key)]} />) }
                </RadioGroup>

                <InputLabel></InputLabel>
            </FormControl>
        )
        case 'Date': return (
            <LocalizationProvider dateAdapter={AdapterDayjs}>
                <DatePicker 
                    sx={{width: '100%'}}
                    label={field.display}
                    value={dayjs(field.initialValue + '')}
                    name={field.name}
                    onChange={(e: Dayjs | null) => handle({name: field.name, type: 'Date', value: e?.toString() ?? "" })}
                />
            </LocalizationProvider>
        )
        case 'Time': return (
            <FormControl key={index} >
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                <TimePicker 
                    sx={{width: '100%'}}
                    ampm={false}
                    label={field.display}
                    value={dayjs(field.initialValue + '', "HH:mm:ss")}
                    name={field.name}
                    onChange={(e: Dayjs | null) => handle({name: field.name, type: 'Time', value: e?.format("HH:mm:ss") ?? "" })}
                />
                </LocalizationProvider>
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
    fullWidth?: boolean,
    disabled?: boolean
}

interface ChangeEvent {
    name: string,
    type: string,
    value: string | boolean | number
}

const CustomFormFields = ({ fields, handleInputChanges, wrapper, disableLabels, variant, fullWidth }: FormFieldsProps) => {

    const handleFieldChanges = (
        event: ChangeEvent
    ) => {
        const name = event.name;
        let value;
        
        

        if (['switch', 'checkbox'].includes(event.type)) {
            value = event.value;
        } else {
            value = event.value;
        }

        console.log(value)

        handleInputChanges({ name, value });
    };

    return fields.map((field, index) => {
        const value = field.initialValue ?? "";

        const valid = field.validator
            ? field.validator(value)
            : true;

        const fieldElement = CreateFields(
            {
                field: { ...field, initialValue: field.initialValue ?? "", valid },
                index: index,
                disableLabels: disableLabels ?? false,
                variant: variant ?? 'filled',
                fullWidth: fullWidth ?? false,
                handle: handleFieldChanges,
                disabled: field.disabled ?? false
            } as CreateFieldsProps
            );

            return wrapper ? wrapper(fieldElement, index) : fieldElement;
        })
}

export default CustomFormFields;