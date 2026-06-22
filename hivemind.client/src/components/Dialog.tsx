import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import Button from '@mui/material/Button';
import Stack from '@mui/material/Stack';

import CustomFormFields, { type FieldData } from './FormFields';
import { type CustomFormField } from './FormFields';

import { useState, useMemo } from 'react';

interface CustomDialogProps<T> {
    buttonText: string;
    title: string;
    fields: CustomFormField[];
    initialValue: T;
    save: (item: T) => void | Promise<void>;
};

function getProperty<Type, Key extends keyof Type>(obj: Type, key: Key | string) {
    return obj[key as Key];
}

const CustomDialog = <T,>({ buttonText, title, fields, initialValue, save }:CustomDialogProps<T>) => {
    const [createModalState, setCreateModalState] = useState<boolean>(false);

    const handleModalOpen = () => {
        setData(initialValue);
        setCreateModalState(true);
    }
    const handleModalClose = () => setCreateModalState(false);

    const [data, setData] = useState<T>(initialValue);

    const formFields = useMemo(() => {
        return fields.map(field => ({
            ...field,
            initialValue: getProperty(data, field.name) as string | number | boolean,
            valid: field.validator ? field.validator(getProperty(data, field.name) as string | number | boolean) : true
        }));
    }, [data, fields]);

    const handleInputChanges = (data: FieldData) => {
        const { name, value } = data;
        setData(values => ({ ...values, [name]: value }))
    };

    const handleAction = () => {
        save(data);
        handleModalClose();
    }

    return (
        <>
            <Button onClick={handleModalOpen}>
                {buttonText}
            </Button>
            <Dialog onClose={handleModalClose} open={createModalState}>
                <DialogTitle>{title}</DialogTitle>
                <DialogContent>
                    <Stack spacing={2}>
                        <CustomFormFields fields={formFields} handleInputChanges={handleInputChanges} />
                    </Stack>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleAction}>Create</Button>
                </DialogActions>
            </Dialog>
        </>
        )
}

export default CustomDialog