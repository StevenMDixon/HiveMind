import { useState, use, useEffect } from 'react'

import Container from '@mui/material/Container';
import CustomEditForm, { type CustomFormField } from '../Components/EditCustomForm';

import type { Collection, Filter, QueryOption } from './types';

import { CollectionFiltersLayout } from './CollectionFiltersLayout';

export const CollectionLayout = ({ promise, options, save }: { promise: Promise<Collection>, options: Promise<QueryOption[]>, save: (c: Collection) => Promise<void> }) => {
    const collection = use(promise);
    const queryOptions = use(options);

    const [filterData, setFilterData] = useState<Filter[]>([]);

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

    useEffect(() => {
        setFilterData(collection.filters);
    }, [collection])

    const fields = [
        { name: 'collectionName', type: "Text", initialValue: collection.collectionName },
    ] as CustomFormField[];

    return (
        <Container sx={{ mt: 5 }}>
            <CustomEditForm title="Collection Info:" fields={fields} initialValue={collection} save={handleSave}>
                <CollectionFiltersLayout filters={filterData} options={queryOptions} filterChange={handleFilterChange} addFilter={addFilter} removeFilter={removeFilter} />
            </CustomEditForm>
        </Container>
    )
}