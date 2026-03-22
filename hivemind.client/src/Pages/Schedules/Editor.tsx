import { Typography, TextField, FormControlLabel, Radio, RadioGroup, FormControl, FormLabel, Box, Button } from '@mui/material';
import type { ScheduleItem } from './types';
import { useState } from 'react'

interface ScheduleItemEditorProps {
    scheduleItem: ScheduleItem | null;
    save: () => void
}

const ScheduleItemEditor = ({ scheduleItem, save }: ScheduleItemEditorProps) => {

    const [inputs, setInputs] = useState({ name: scheduleItem?.name ?? "", type: scheduleItem?.type ?? "Generic" });

    const changeInputs = (event: React.ChangeEvent<HTMLInputElement> | React.ChangeEvent<HTMLTextAreaElement>) => {
        console.log(event.target.name)
        const name = event.target.name;
        const value = event.target.value;
        setInputs(values => ({ ...values, [name]: value }))
    }

    console.log(scheduleItem)
    return (
        <Box>
            <Typography sx={{mb: 5}}>Editing Schedule Item</Typography>
            <TextField label="Name" name="name" value={inputs.name} onChange={changeInputs} />
            <FormLabel id="demo-row-radio-buttons-group-label">Schedule Type</FormLabel>
        <FormControl>
            <RadioGroup
                row
                aria-labelledby="demo-row-radio-buttons-group-label"
                name="type"
                value={inputs.type}
                onChange={changeInputs}
            >
                <FormControlLabel value="Generic" control={<Radio />} label="Generic" />
                <FormControlLabel value="Block" control={<Radio />} label="Block" />
            </RadioGroup>
            </FormControl>
            <Button variant="contained" onClick={save} sx={{ mt: 2 }}>Save</Button>
        </Box>
    );
}

export default ScheduleItemEditor;