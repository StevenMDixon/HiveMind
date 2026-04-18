import Paper from '@mui/material/Paper';
import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import Stack from '@mui/material/Stack';

import SaveIcon from '@mui/icons-material/Save';
import { type PropsWithChildren, useState, useMemo } from 'react';

import { type CustomFormField } from './FormFields';
import CustomFormFields, { type FieldData } from './FormFields';

import { getProperty } from '../../Utilities/FormOptionsMapper';

interface CustomFormProps<T> {
    title: string;
    initialValue: T;
    fields: CustomFormField[];
    save?: (item: T) => void | Promise<void>;
}


const CustomForm = <T,>({ title, save, initialValue, fields, children }: PropsWithChildren<CustomFormProps<T>>) => {
    const [data, setData] = useState<T>(initialValue);

    const formFields = useMemo(() => {
        return fields.map(field => ({
            ...field,
            initialValue: getProperty(data, field.name) as string | number | boolean,
            valid: field.validator ? field.validator(getProperty(data, field.name) as string | number | boolean) : true
        }));
    }, [data, fields]);

    const handleInputChanges = (data: FieldData)  => {
        const { name, value } = data;
        setData(values => ({ ...values, [name]: value }))
    };

    return (
        <Paper>
            <Box sx={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', mx: 1, p: ".5em" }}>
                <Typography variant="h5">
                    {title}
                </Typography>
                {save &&
                    <IconButton onClick={() => save(data)}>
                        <SaveIcon />
                    </IconButton>
                }
            </Box>
            <Divider />
            <Stack spacing={2} direction="column" useFlexGap
                sx={{
                    flexWrap: 'wrap', m: 2, p: 2, alignItems: "stretch"
                    }}
                >
                <CustomFormFields fields={formFields} handleInputChanges={handleInputChanges} fullWidth
                    wrapper={(component, index) => <Box key={index}  sx={{maxWidth: '100%'} }>{component}</Box>} />
            </Stack>
            <Box component="form">
                {children}
            </Box>
        </Paper>
    )
}

export default CustomForm;