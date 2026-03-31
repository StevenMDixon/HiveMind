import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import Button from '@mui/material/Button';

import CustomFormFields, { type FieldData } from './FormFields';
import { type CustomFormField } from './FormFields';

import { useState } from 'react';

interface CustomDialogProps<T> {
    buttonText: string;
    title: string;
    fields: CustomFormField[];
    initialValue: T;
    save: (item: T) => void | Promise<void>;
};

const CustomDialog = <T,>({ buttonText, title, fields, initialValue, save }:CustomDialogProps<T>) => {
    const [createModalState, setCreateModalState] = useState<boolean>(false);

    const handleModalOpen = () => setCreateModalState(true);
    const handleModalClose = () => setCreateModalState(false);

    const [data, setData] = useState<T>(initialValue);

    const handleInputChanges = (data : FieldData) => {
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
                    <CustomFormFields fields={fields} handleInputChanges={handleInputChanges}/>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleAction}>Create</Button>
                </DialogActions>
            </Dialog>
        </>
        )
}

export default CustomDialog