import Paper from '@mui/material/Paper';
import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton'
import Typography from '@mui/material/Typography'
import Divider from '@mui/material/Divider'
import FormControl from '@mui/material/FormControl'
import TextField from '@mui/material/TextField'
import SaveIcon from '@mui/icons-material/Save';
import { type PropsWithChildren, useState } from 'react';


interface EditFormProps<T> {
    title: string;
    initialValue: T;
    fields: CustomFormField[];
    save: (item: T) => void | Promise<void>;
}

type CustomFormFieldTypes = 'Text' | 'Date' | 'Select';

export interface CustomFormField {
    name: string;
    initialValue: string | number | boolean;
    type: CustomFormFieldTypes;
    validator: () => void;
    options?: string[];
}

const CreateFields = (field: CustomFormField, index: number, handle: (e: React.ChangeEvent<HTMLInputElement>) => void) => {
    switch (field.type) {
        case 'Text': return (
            <FormControl sx={{ m: 2, pb: 2 }} key={index} >
                <TextField
                required
                variant="filled"
                name={field.name}
                label={field.name}
                defaultValue={field.initialValue}
                onChange={handle}
                />
            </FormControl>
                )
    }


    return (<p>{field.name}</p>)
}

const EditCustomForm = <T,>({ title, save, initialValue, fields, children }: PropsWithChildren<EditFormProps<T>>) => {

    const [data, setData] = useState<T>(initialValue);

    const handleInputChanges = (event: React.ChangeEvent<HTMLInputElement>) => {
        const name = event.target.name;
        const value = event.target.value;
        setData(values => ({ ...values, [name]: value }))
    };

    return (
        <Paper>
            <Box sx={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', mx: 1 }}>
                <Typography variant="h5">
                    {title}
                </Typography>
                <IconButton onClick={() => save(data)}>
                    <SaveIcon />
                </IconButton>
            </Box>
            <Divider />
            <Box component="form">
                {fields && fields.map((field, index) => CreateFields(field, index, handleInputChanges)) }
            </Box>
            <Box component="form">
                {children}
            </Box>
        </Paper>
    )
}

export default EditCustomForm;