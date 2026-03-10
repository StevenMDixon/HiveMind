import { IconButton, Stack } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';
import { CollectionFilter } from "./CollectionFilter";
import type { Filter, Option } from './types';
import Divider from '@mui/material/Divider';

interface CollectionFiltersLayoutProps { filters: Filter[] | undefined, options: Option[], filterChange: (id: number, field: string, value: string) => void, addFilter: () => void, removeFilter: (a: number) => void }

export const CollectionFiltersLayout = ({ filters, options, filterChange, addFilter, removeFilter }: CollectionFiltersLayoutProps) => {
    return (
        <>
        <Stack direction= "row" justifyContent = "space-between" >
                <p>Filters:</p>
                < IconButton onClick = { addFilter } >
                    <AddIcon/>
                    </IconButton>
            </Stack>
            <Divider sx={{mb: 3}} />
            {
                filters && <CollectionFilter filters={ filters } options = { options } change = { filterChange } remove = { removeFilter } />
            }

    </>
    )
};