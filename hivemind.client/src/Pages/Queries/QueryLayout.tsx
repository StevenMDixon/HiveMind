import { IconButton, Stack } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import { useState, use } from 'react'

import CustomForm from '../Components/CustomForm';
import { type CustomFormField } from '../Components/FormFields';

import type { Query, Filter, QuerySetting, QuerySettingItem } from '../../Types/Query';
import type { EnumOptionsObj } from '../../Types/General';

import EditingGrid from '../Components/EditingGrid';
import type { EditingGridColumns } from '../Components/EditingGrid';

import { toEnumOptions } from '../../Utilities/FormOptionsMapper';

export const QueryLayout = ({ promise, options, settings, save }: { promise: Promise<Query>, options: Promise<QuerySettingItem[]>, settings: Promise<QuerySetting[]>, save: (c: Query) => Promise<void> }) => {
    const query = use(promise);
    const queryOptions = use(options);
    const querySettings = use(settings);

    const fieldSettings = querySettings.reduce((acc: EnumOptionsObj, curr, index) => { acc[index] = curr.name; return acc; }, {})

    const formatOptions = (item: number) => {
        return fieldSettings[item];
    }

    const operatorOptions = (filter: Filter) => {
        const targetQueryOption = querySettings.find(x => x.name == fieldSettings[Number(filter.field)])
        return targetQueryOption ? toEnumOptions(targetQueryOption.options, 'id', 'name') : {};
    }

    const formatOperator = (item: number) => {
        const options = toEnumOptions(queryOptions, 'id', 'name');
        return options[item];
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
                <EditingGrid gridItems={filterData} gridFieldColumns={gridColumns} deleteItem={removeFilter} saveItem={saveFilter} />
            </CustomForm>
        </Container>
    )
}