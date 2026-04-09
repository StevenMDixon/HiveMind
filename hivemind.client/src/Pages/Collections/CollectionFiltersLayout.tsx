import { IconButton, Stack } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';
import { CollectionFilter } from "./CollectionFilter";
import type { Filter, Option } from '../../Types/Collections'
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';

interface CollectionFiltersLayoutProps { filters: Filter[] | undefined, options: Option[], filterChange: (id: number, field: string, value: string) => void, addFilter: () => void, removeFilter: (a: number) => void }

export const CollectionFiltersLayout = ({ filters, options, filterChange, addFilter, removeFilter }: CollectionFiltersLayoutProps) => {
    return (
        <Box sx={{m: 1}}>
        <Stack direction= "row" justifyContent = "space-between" >
                <Typography variant="h6" >Collection Filters:</Typography>
                < IconButton onClick = { addFilter } >
                    <AddIcon/>
                </IconButton>
        </Stack>
        <Box sx={{ my: 1, ml: 2 }}>
            {
                filters && <CollectionFilter filters={ filters } options = { options } change = { filterChange } remove = { removeFilter } />
            }
        </Box>
    </Box>

    )
};