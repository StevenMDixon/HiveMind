import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import Button from '@mui/material/Button';
import Stack from '@mui/material/Stack';
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import type { EnumOptionsObj } from '../../../utilities/types';
import type { SelectChangeEvent } from '@mui/material/Select';
import TextField from '@mui/material/TextField';

import { useState } from 'react';
import type { Tag } from '@/features/media/types';

export interface NewTagModalProps {
    categories: EnumOptionsObj;
    existingTags: any[];
    modalOpen: boolean;
    modalSave: (tag: Tag) => void;
    modalClose: () => void;
}

export const NewTagModal = ({categories, existingTags, modalOpen, modalClose, modalSave } : NewTagModalProps) => {
    const [tagName, setTagName] = useState('');
    const [category, setCategory] = useState(0);
    const [hasError, setError] = useState(false);

    const handleSave = () => {
        const isDuped = existingTags.map(x => x.name).some(x => x.toLocaleLowerCase() === tagName.toLocaleLowerCase())

        if(isDuped || tagName.replace(" ", "") === "") 
        {
            setError(true);
            return;
        }

        const newTag = {
            id: -1 * existingTags.length,
            name: tagName,
            category: Number(category)
        }

        setError(false);
        setCategory(0);
        setTagName('');

        modalSave(newTag);
    }

    const handleCategoryUpdate = (e: SelectChangeEvent<number>) => {
        setCategory(e.target.value);
    }

    const handleNameUdpate = (e: React.ChangeEvent<HTMLInputElement>) => {
        setTagName(e.target.value);
    }

    const handleModalClose = () => {
        setError(false);
        setCategory(0);
        setTagName('');

        modalClose();
    }

    return (
        <Dialog open={modalOpen}>
             <DialogTitle>Add a New Tag</DialogTitle>
             <DialogContent>
                <Stack sx={{margin: "1em;"}} >
                    <TextField label="Tag Name" value={tagName} onChange={handleNameUdpate} error={hasError} sx={{marginBottom: '1em;'}}/>
                    <Select defaultValue={0} value={category} onChange={handleCategoryUpdate}>
                        {[...Object.keys(categories)].map((x) =>  <MenuItem value={x}>{categories[Number(x)]}</MenuItem>)}
                    </Select>
                </Stack>
             </DialogContent>
             <DialogActions>
                <Button onClick={handleModalClose}>Cancel</Button>
                <Button onClick={handleSave}>Save</Button>
            </DialogActions>
        </Dialog>
    );
}