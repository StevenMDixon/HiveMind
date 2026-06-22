import { IconButton, Stack } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import { useState } from 'react'

import CustomForm from '@/components/CustomForm';
import { type CustomFormField } from '@/components/FormFields';

import type { Query, Filter} from '../types';
import type { EnumOptionsObj } from '@/utilities/types';

import EditingGrid, { type EditingGridColumns } from '@/components/EditingGrid';

import { toEnumOptions } from '@/utilities/FormOptionsMapper';

import { useQuery } from '@tanstack/react-query';
import { settingsQueryOptions, optionsQueryOptions } from '../api/queryOptions'

interface QueryLayoutProps {
    query: Query,
    save: (c: Query) => Promise<void>
}

export const QueryLayout = ({ query, save }: QueryLayoutProps) => {

    const { data: settings, isLoading: settingsLoad } = useQuery(settingsQueryOptions());
    const { data: options, isLoading: optionsLoad } = useQuery(optionsQueryOptions());

    const fieldSettings = settings ? settings.reduce((acc: EnumOptionsObj, curr, index) => { acc[index] = curr.name; return acc; }, {}) : {}

    const formatOptions = (item: number) => {
        return fieldSettings[item];
    }

    const operatorOptions = (filter: Filter) => {
        const targetQueryOption = settings ? settings.find(x => x.name == fieldSettings[Number(filter.field)]) : null;
        return targetQueryOption ? toEnumOptions(targetQueryOption.options, 'id', 'name') : {};
    }

    const formatOperator = (item: number) => {
        const o = options ? toEnumOptions(options, 'id', 'name') : {};
        return o[item];
    }

    const [filterData, setFilterData] = useState<Filter[]>(query.filters);

    const handleSave = (e: Query) => {
        const newData = {
            queryId: e.queryId,
            queryName: e.queryName,
            filters: filterData
        }
        save(newData);
    }

    const removeFilter = (item: Filter) => {
        const newFilterData = filterData.filter(x => x.queryFilterId != item.queryFilterId)
        setFilterData(newFilterData)
    }

    const addFilter = () => {
        const newFilterData = [...filterData, { queryFilterId: (filterData.length + 1) * -1, field: 0, operator: 0, value: "" } as Filter];
        setFilterData(newFilterData);
    }

    const saveFilter = (item: Filter) => {
        setFilterData(filterData.map(filter => filter.queryFilterId == item.queryFilterId ? item : filter));
    }

    const fields = [
        { name: 'queryName', display: "Name", type: "Text", initialValue: query.queryName },
    ] as CustomFormField[];

    const gridColumns = [
        { name: 'field', display: 'Field', initialValue: 0, type: 'Select', format: formatOptions ,options: fieldSettings},
        { name: 'operator', display: 'Operator', initialValue: 0, type: 'Select', format: formatOperator, options: operatorOptions },
        { name: 'value', display: 'Value', initialValue: 0, type: 'Text'}
    ] as EditingGridColumns<Filter>[]

    return (
        <Container sx={{ mt: 5 }}>
            <CustomForm title="Query Info:" fields={fields} initialValue={query} save={handleSave}>
                <Stack direction="row" justifyContent="space-between" sx={{m: 1, p:1}}>
                    <Typography variant="h6" >Query Filters:</Typography>
                    < IconButton onClick={addFilter} >
                        <AddIcon />
                    </IconButton>
                </Stack>
                {filterData && <EditingGrid gridItems={filterData} gridFieldColumns={gridColumns} deleteItem={removeFilter} saveItem={saveFilter} isLoading={settingsLoad || optionsLoad} />}
            </CustomForm>
        </Container>
    )
}