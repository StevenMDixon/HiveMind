import { useState, use } from 'react'

import Container from '@mui/material/Container';
import CustomForm from '../Components/CustomForm';
import { type CustomFormField } from '../Components/FormFields';

import type { Collection, Filter, QueryOption } from '../../Types/Collections'

import { CollectionFiltersLayout } from './CollectionFiltersLayout';

export const CollectionLayout = ({ promise, options, save }: { promise: Promise<Collection>, options: Promise<QueryOption[]>, save: (c: Collection) => Promise<void> }) => {
    const collection = use(promise);
    const queryOptions = use(options);

    const [filterData, setFilterData] = useState<Filter[]>(collection.filters);

    const handleFilterChange = (id: number, field: string, value: string) => {
        if (filterData) {
            setFilterData(prevFilters =>
                prevFilters.map(filter => {
                    if (filter.queryFilterID === id) {
                        return { ...filter, [field]: value }
                    }

                    return filter;
                })
            );
        }
    }

    const handleSave = (e: Collection) => {
        const newData = {
            collectionID: e.collectionID,
            collectionName: e.collectionName,
            filters: filterData
        }

        save(newData);
    }

    const removeFilter = (id: number) => {
        const newFilterData = filterData.filter(x => x.queryFilterID != id);
        setFilterData(newFilterData)
    }

    const addFilter = () => {
        const newFilterData = [...filterData, { queryFilterID: (filterData.length + 1) * -1, field: "", operator: "", value: "" } as Filter];
        setFilterData(newFilterData);
    }

    const fields = [
        { name: 'collectionName', type: "Text", initialValue: collection.collectionName },
    ] as CustomFormField[];

    return (
        <Container sx={{ mt: 5 }}>
            <CustomForm title="Collection Info:" fields={fields} initialValue={collection} save={handleSave}>
                <CollectionFiltersLayout filters={filterData} options={queryOptions} filterChange={handleFilterChange} addFilter={addFilter} removeFilter={removeFilter} />
            </CustomForm>
        </Container>
    )
}