import Paper from '@mui/material/Paper';
import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';

import SaveIcon from '@mui/icons-material/Save';
import { type PropsWithChildren, useState, useMemo } from 'react';

import { type CustomFormField } from './FormFields';
import CustomFormFields, { type FieldData } from './FormFields';

interface CustomFormProps<T> {
    title: string;
    initialValue: T;
    fields: CustomFormField[];
    save: (item: T) => void | Promise<void>;
}

function getProperty<Type, Key extends keyof Type>(obj: Type, key: Key | string) {  
    return obj[key as Key];
}


const CustomForm = <T,>({ title, save, initialValue, fields, children }: PropsWithChildren<CustomFormProps<T>>) => {

    const [data, setData] = useState<T>(initialValue);

    const formFields = useMemo(() => {
        return fields.map(field => ({
            ...field,
            initialValue: getProperty(initialValue, field.name) as string | number | boolean,
            valid: field.validator ? field.validator(getProperty(initialValue, field.name) as string | number | boolean) : true
        }));
    }, [initialValue, fields]);

    const handleInputChanges = (data: FieldData)  => {
        const { name, value } = data;

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
            <CustomFormFields fields={formFields} handleInputChanges={handleInputChanges} />
            <Box component="form">
                {children}
            </Box>
        </Paper>
    )
}

export default CustomForm;