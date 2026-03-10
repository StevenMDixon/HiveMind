import TextField from '@mui/material/TextField';
import Stack from '@mui/material/Stack';
import FormControl from '@mui/material/FormControl';
import InputLabel from '@mui/material/InputLabel';
import Select, { type SelectChangeEvent }  from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem'; 
import IconButton from '@mui/material/IconButton';
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';

import type { Filter, Option } from './types';

interface CollectionFilterProps { filters: Filter[], options: Option[], change: (id: number, field: string, value: string) => void, remove: (a: number) => void }

const getSelectableOptions = (field: string, options: Option[]): string[] => {
    let validOps: string[] = ["-"];
    const selectedOption = options.find(x => x.name == field);

    if (selectedOption) {
        validOps = validOps.concat(selectedOption.options.split(", "))
    }

    return validOps;
}

export const CollectionFilter = ({ filters, options, change, remove }: CollectionFilterProps) => {
    return (
        <Stack>
            {
                filters.map((x: Filter) => (
                    <Stack direction="row" key={x.queryFilterID} alignItems="center" spacing={2} sx={{pb: 1} }>
                        <FormControl sx={{ minWidth: 200, mr: 1, mb: 1 }}>
                            <InputLabel>Field</InputLabel>
                            <Select
                                name={x.queryFilterID + " field"}
                                value={x.field}
                                onChange={(e: SelectChangeEvent) => change(x.queryFilterID, "field", e.target.value)}
                            >
                                {
                                    options.map((option, idx) => (
                                        <MenuItem key={`field-${option.name}-${idx}`} value={option.name}>
                                            {option.name}
                                        </MenuItem>
                                    ))
                                }
                            </Select>
                        </FormControl>
                        <FormControl sx={{ minWidth: 200, mr: 1 }}>
                            <InputLabel>Operator</InputLabel>
                            <Select
                                name={x.queryFilterID + " operator"}
                                value={x.operator}
                                onChange={(e: SelectChangeEvent) => change(x.queryFilterID, "operator", e.target.value)}
                            >
                                {
                                    getSelectableOptions(x.field, options).map(op => (<MenuItem value={op}>{op}</MenuItem>))
                                }
                            </Select>
                        </FormControl>
                        <FormControl sx={{ minWidth: 350 }} >
                            <TextField name={x.queryFilterID + " value"} label="Value" variant="outlined" value={x.value} onChange={(e) => change(x.queryFilterID, "value", e.target.value)} />
                        </FormControl>
                        <FormControl >
                            <IconButton aria-label="delete" onClick={() => remove(x.queryFilterID)}>
                                <DeleteOutlineIcon color="error" />
                            </IconButton>
                        </FormControl>
                    </Stack>
                ))
            }
        </Stack>
    );
}