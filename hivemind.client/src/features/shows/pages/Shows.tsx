import Container from '@mui/material/Container';
import Header from '@/components/Header';
import CustomTable, { type CellData } from '@/components/CustomTable';

import type { Show } from '../types';

import { useQuery } from '@tanstack/react-query';
import { showsQueryOptions } from '../api/queryOptions';

const ShowsPage = () => {

    const { data: shows, refetch, isLoading } = useQuery(showsQueryOptions());

    const columns = [
        { key: 'showId', name: 'ID', align: 'left' },
        { key: 'showName', name: 'Show Name', align: 'left' },
    ] as CellData<Show>[];


    const handleRetry = () => {
        refetch(); 
    };

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Shows"/>
            <CustomTable data={shows} handleRetry={handleRetry} columns={columns} isLoading={isLoading} />
        </Container>
    )
}

export default ShowsPage;