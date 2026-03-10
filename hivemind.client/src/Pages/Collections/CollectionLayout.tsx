import { useState, use, useEffect } from 'react'

import Container from '@mui/material/Container';
import Paper from '@mui/material/Paper';
import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton'
import SaveIcon from '@mui/icons-material/Save';

import type { Collection, Filter } from './types';

import { CollectionInfo } from './CollectionInfo';
import { CollectionFiltersLayout } from './CollectionFiltersLayout';

const dataPromise = fetch('/querysettings').then(res => res.json());

export const CollectionLayout = ({ promise, save }: { promise: Promise<Collection>, save: (c: Collection) => Promise<void> }) => {
    const collection = use(promise);
    const queryOptions = use(dataPromise);

    const [collectionData, setCollectionData] = useState<Collection>();
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

    const getUpdatedCollection = (): Collection => {
        return {
            collectionID: collectionData?.collectionID ?? 0,
            collectionName: collectionData?.collectionName ?? "",
            filters: filterData
        }
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
        setCollectionData(collection);
        setFilterData(collection.filters);
    }, [collection])

    return (
        <Container sx={{ mt: 5 }}>
            <Paper>
                <Box sx={{ display: 'flex', flexDirection: 'row', justifyContent: 'flex-end', mx: 1}}>
                    <IconButton onClick={() => save(getUpdatedCollection())}>
                        <SaveIcon  />
                    </IconButton>
                </Box>
                
                <Box sx={{m: 2, pb: 2} }>
                    <CollectionInfo collection={collectionData} />
                    <CollectionFiltersLayout filters={filterData} options={queryOptions.options} filterChange={handleFilterChange} addFilter={addFilter} removeFilter={removeFilter} />
                </Box>
            </Paper>
        </Container>
    )
}